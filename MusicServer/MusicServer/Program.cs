using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

IPAddress ip = IPAddress.Parse("192.168.182.1");
TcpListener listener = new TcpListener(ip, 9999);
List<Socket> clientList = new List<Socket>();

listener.Start();
Console.WriteLine($"Server started on {listener.LocalEndpoint}");
Console.WriteLine("Waiting for a connection...");

void SendData(Socket client)
{
    if (client != null)
    {

    }
}

void ReceiveAndReply(object obj)
{
    Socket? client = obj as Socket;
    try
    {
        while (true)
        {
            byte[] data = new byte[1024 * 5120];
            client.Receive(data);

            string message = (string)Deserialize(data);
            if (message.Equals("GetData"))
            {
                foreach (Socket item in clientList)
                {
                    if (item != null && item == client)
                        SendData(client);
                }
            }
        }
    }
    catch
    {
        clientList.Remove(client);
        client.Close();
    }
}

Thread listen = new Thread(() =>
{
    while (true)
    {
        Socket socket = listener.AcceptSocket();
        clientList.Add(socket);
        Console.WriteLine("Connection received from " + socket.RemoteEndPoint);
        Thread receive = new Thread(ReceiveAndReply);
        receive.IsBackground = true;
        receive.Start(socket);
    }
});
listen.IsBackground = true;
listen.Start();

Console.Read();
foreach (Socket socket in clientList)
{
    if (socket != null)
        socket.Close();
}
listener.Stop();

#pragma warning disable SYSLIB0011
//phân mảnh dữ liệu
byte[] Serialize(object obj)
{
    MemoryStream stream = new MemoryStream();
    BinaryFormatter formatter = new BinaryFormatter();

    formatter.Serialize(stream, obj);

    return stream.ToArray();
}

//gom mảnh dữ liệu đã phân mảnh
object Deserialize(byte[] data)
{
    MemoryStream stream = new MemoryStream(data);
    BinaryFormatter formatter = new BinaryFormatter();

    return formatter.Deserialize(stream);
}
#pragma warning restore SYSLIB0011