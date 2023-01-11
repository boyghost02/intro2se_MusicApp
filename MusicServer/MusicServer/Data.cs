using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicAppClass;

namespace MusicServer
{
    public class ServerData
    {
        public ServerData()
        {
            musicList = SongList();
            accountList = AccList();
        }
        private ObservableCollection<Song> musicList;
        private ObservableCollection<Account> accountList;

        public ObservableCollection<Song> MusicList() { return musicList; }
        public ObservableCollection<Account> AccountList() { return accountList; }

        ObservableCollection<Song> SongList()
        {
            ObservableCollection<Song> res = new ObservableCollection<Song>();
            res.Add(DoAnhDoanDuoc());
            res.Add(HayTraoChoAnh());
            res.Add(TetDongDay());
            res.Add(DoiItThoiOmDe());
            res.Add(RoiToiLuon());
            res.Add(VeTinh());
            res.Add(BaiNayKhongDeDiDien());
            res.Add(HangXom());
            res.Add(LacTroi());
            res.Add(ThichEmNoiNhieu());
            res.Add(ChungTaKhongThuocVeNhau());
            res.Add(ChuyenDoiTa());
            res.Add(DiDeTroVe());
            res.Add(EmChaoTet());
            res.Add(GieoQue());
            res.Add(HonEmChoNao());
            res.Add(Lalisa());
            res.Add(LayChongSomLamGi());
            res.Add(NhuNgayHomQua());
            res.Add(NoiNayCoAnh());
            res.Add(OnTheGround());
            res.Add(Solo());
            res.Add(ViYeuCuDamDau());
            return res;
        }
        ObservableCollection<Account> AccList()
        {
            ObservableCollection<Account> res = new ObservableCollection<Account>();
            res.Add(account1());
            res.Add(account2());
            return res;
        }
        #region song
        Song DoAnhDoanDuoc()
        {
            List<Comment> listComment = new List<Comment>();
            listComment.Add(new Comment("MusicLover123","Bình luận: Tôi rất thích sản xuất âm thanh trong bài hát này, rất sắc nét và tinh tế."));
            listComment.Add(new Comment("PopFanatic","Bình luận: Ca khúc này có lối nhạc rất thú vị và bài hát của nghệ sĩ đã được thể hiện rất tốt."));
            listComment.Add(new Comment("Harmony_Hunter","Bình luận: Tôi thích cách mà các nhạc cụ được sắp xếp trong bài hát này, đặc biệt là cách mà hợp âm được sử dụng"));
            listComment.Add(new Comment("RapLover","Lời bài hát rất sâu sắc và sự flow của nghệ sĩ rất tuyệt vời"));

            return new Song
            {
                Name = "Đố anh đoán được",
                Singer = "Bích Phương",
                Url = "http://192.168.8.1:8082/music/DoAnhDoanDuoc-BichPhuong.mp3",
                CoverImage = "http://192.168.8.1:8082/music/DoAnhDoanDuoc-BichPhuong.jpg",
                Like = 142,
                Comments = listComment
            };
        }
        Song HayTraoChoAnh()
        {
            List<Comment> listComment = new List<Comment>();
            listComment.Add(new Comment("Pianist_Dreamer","Tôi rất thích cách mà sự trình diễn piano được sử dụng trong bài hát này, cảm xúc được thể hiện rất tốt"));
            listComment.Add(new Comment("BluesFan","Tôi thích cách mà giai điệu blues được sử dụng trong bài hát này, đó là sự kết hợp hoàn hảo giữa cảm xúc và âm nhạc"));
            listComment.Add(new Comment("CountryLover","Lời bài hát rất sâu sắc và cảm xúc được thể hiện rất tốt, tôi yêu thích âm nhạc country"));
            listComment.Add(new Comment("JazzEnthusiast","Tôi thích cách mà các nhạc cụ được sắp xếp trong bài hát này, đặc biệt là cách mà sự tự do trong jazz được thể hiện"));
            listComment.Add(new Comment("ClassicalMusicFan","Tôi rất thích cách mà các nhạc cụ được sắp xếp trong bài hát này, đó là một tác phẩm âm nhạc quái đẹp"));

            return new Song
            {
                Name = "Hãy trao cho anh",
                Singer = "Sơn Tùng",
                Url = "http://192.168.8.1:8082/music/HayTraoChoAnh-SonTung.mp3",
                CoverImage = "http://192.168.8.1:8082/music/HayTraoChoAnh-SonTung.jpg",
                Like = 147,
                Comments = listComment
            };
        }        
        Song TetDongDay()
        {
            List<Comment> listComment = new List<Comment>();
            listComment.Add(new Comment("IndieRockFan","Tôi thích cách mà nghệ sĩ đã sử dụng các tiếng nói khác nhau trong bài hát này, đó là sự kết hợp hoàn hảo giữa âm nhạc và lời bài hát"));
            listComment.Add(new Comment("ElectronicMusicFan","Tôi thích cách mà các tiếng điện tử được sử dụng trong bài hát này, cảm giác rất mới lạ."));
            listComment.Add(new Comment("RockMusicFan","Tôi thích sự chạy nhanh và sức mạnh của bài hát này, điều đó giúp tôi cảm thấy sức mạnh và cuốn hút"));
            listComment.Add(new Comment("FolkMusicFan","Tôi thích lối trình bày nổi bật của các nhạc cụ trong bài hát này, tôi cảm thấy rất quen thuộc với cách mà nó được trình bày"));
            listComment.Add(new Comment("ReggaeMusicFan","Tôi thích sự dễ dàng và vui tươi của bài hát này, điều đó giúp tôi cảm thấy tự do và hạnh phúc"));

            return new Song
            {
                Name = "Tết đong đầy",
                Singer = "Kay Trần",
                Url = "http://192.168.8.1:8082/music/TetDongDay-KayTran.mp3",
                CoverImage = "http://192.168.8.1:8082/music/TetDongDay-KayTran.jpg",
                Like = 135,
                Comments = listComment
            };
        }        
        Song DoiItThoiOmDe()
        {
            return new Song
            {
                Name = "Dỗi ít thôi ôm đê",
                Singer = "Phúc Du - AMEE",
                Url = "http://192.168.8.1:8082/music/DoiItThoiOmDe-PhucDuAMEE.mp3",
                CoverImage = "http://192.168.8.1:8082/music/DoiItThoiOmDe-PhucDuAMEE.jpg",
                Like = 95
            };
        }
        Song RoiToiLuon()
        {
            return new Song
            {
                Name = "Rồi tới luôn",
                Singer = "Nal",
                Url = "http://192.168.8.1:8082/music/RoiToiLuon-Nal.mp3",
                CoverImage = "http://192.168.8.1:8082/music/RoiToiLuon-Nal.jpg",
                Like = 46
            };
        }        
        Song VeTinh()
        {
            return new Song
            {
                Name = "Vệ tinh",
                Singer = "HIEUTHUHAI",
                Url = "http://192.168.8.1:8082/music/VeTinh-HIEUTHUHAI.mp3",
                CoverImage = "http://192.168.8.1:8082/music/VeTinh-HIEUTHUHAI.jpg",
                Like = 107
            };
        }
        Song BaiNayKhongDeDiDien()
        {
            return new Song
            {
                Name = "Bài này không để đi diễn",
                Singer = "Anh Tú",
                Url = "http://192.168.8.1:8082/music/Bainaykhongdedidien_AnhTu.mp3",
                CoverImage = "http://192.168.8.1:8082/music/Bainaykhongdedidien_AnhTu.jpg",
                Like = 103
            };
        }
        Song HangXom()
        {
            return new Song
            {
                Name = "Hàng xóm",
                Singer = "Anh Tú - Ly Ly",
                Url = "http://192.168.8.1:8082/music/HangXom-AnhTuLyly.mp3",
                CoverImage = "http://192.168.8.1:8082/music/HangXom-AnhTuLyly.jpg",
                Like = 75
            };
        }
        Song LacTroi()
        {
            return new Song
            {
                Name = "Lạc trôi",
                Singer = "Sơn Tùng",
                Url = "http://192.168.8.1:8082/music/LacTroi-SonTung.mp3",
                CoverImage = "http://192.168.8.1:8082/music/LacTroi-SonTung.jpg",
                Like = 112
            };
        }
        Song ThichEmNoiNhieu()
        {
            return new Song
            {
                Name = "Thích em nói nhiều",
                Singer = "Hoàng Dũng",
                Url = "http://192.168.8.1:8082/music/ThichEmHoiNhieu-HoangDung.mp3",
                CoverImage = "http://192.168.8.1:8082/music/ThichEmHoiNhieu-HoangDung.jpg",
                Like = 78
            };
        }
        Song ChungTaKhongThuocVeNhau()
        {
            return new Song
            {
                Name = "Chúng ta không thuộc về nhau",
                Singer = "Sơn Tùng",
                Url = "http://192.168.8.1:8082/music/ChungTaKhongThuocVeNhau-SonTung.mp3",
                CoverImage = "http://192.168.8.1:8082/music/ChungTaKhongThuocVeNhau-SonTung.jpg",
                Like = 78
            };
        }       
        Song ChuyenDoiTa()
        {
            return new Song
            {
                Name = "Chuyện đôi ta",
                Singer = "Emceel",
                Url = "http://192.168.8.1:8082/music/ChuyenDoiTa-EmceeL.mp3",
                CoverImage = "http://192.168.8.1:8082/music/ChuyenDoiTa-EmceeL.jpg",
                Like = 78
            };
        }
        Song DiDeTroVe()
        {
            return new Song
            {
                Name = "Đi để trở về",
                Singer = "Soobin Hoàng Sơn",
                Url = "http://192.168.8.1:8082/music/DiDeTroVe-SoobinHoangSon.mp3",
                CoverImage = "http://192.168.8.1:8082/music/DiDeTroVe-SoobinHoangSon.jpg",
                Like = 78
            };
        }
        Song EmChaoTet()
        {
            return new Song
            {
                Name = "Em chào tết",
                Singer = "Bích Phương",
                Url = "http://192.168.8.1:8082/music/EmChaoTet-BichPhuong.mp3",
                CoverImage = "http://192.168.8.1:8082/music/EmChaoTet-BichPhuong.jpg",
                Like = 14
            };
        }
        Song GieoQue()
        {
            return new Song
            {
                Name = "Gieo quẻ",
                Singer = "Hoàng Thùy Linh",
                Url = "http://192.168.8.1:8082/music/GieoQue-HoangThuyLinh.mp3",
                CoverImage = "http://192.168.8.1:8082/music/GieoQue-HoangThuyLinh.jpg",
                Like = 102
            };
        }
        Song HonEmChoNao()
        {
            return new Song
            {
                Name = "Hôn em chỗ nào",
                Singer = "Thùy Chi",
                Url = "http://192.168.8.1:8082/music/HonEmChoNao-ThuyChi.mp3",
                CoverImage = "http://192.168.8.1:8082/music/HonEmChoNao-ThuyChi.jpg",
                Like = 78
            };
        }
        Song Lalisa()
        {
            return new Song
            {
                Name = "Lalisa",
                Singer = "LISA",
                Url = "http://192.168.8.1:8082/music/Lalisa-LISA.mp3",
                CoverImage = "http://192.168.8.1:8082/music/Lalisa-LISA.jpg",
                Like = 80
            };
        }
        Song LayChongSomLamGi()
        {
            return new Song
            {
                Name = "Lấy chồng sớm làm gì",
                Singer = "HuyR",
                Url = "http://192.168.8.1:8082/music/LayChongSomLamGi-HuyR.mp3",
                CoverImage = "http://192.168.8.1:8082/music/LayChongSomLamGi-HuyR.jpg",
                Like = 79
            };
        }
        Song NhuNgayHomQua()
        {
            return new Song
            {
                Name = "Như ngày hôm qua",
                Singer = "Sơn Tùng",
                Url = "http://192.168.8.1:8082/music/NhuNgayHomQua-SonTung.mp3",
                CoverImage = "http://192.168.8.1:8082/music/NhuNgayHomQua-SonTung.jpg",
                Like = 60
            };
        }
        Song NoiNayCoAnh()
        {
            return new Song
            {
                Name = "Nơi này có anh",
                Singer = "Sơn Tùng",
                Url = "http://192.168.8.1:8082/music/NoiNayCoAnh-SonTung.mp3",
                CoverImage = "http://192.168.8.1:8082/music/NoiNayCoAnh-SonTung.jpg",
                Like = 72
            };
        }
        Song OnTheGround()
        {
            return new Song
            {
                Name = "On the ground",
                Singer = "ROSE",
                Url = "http://192.168.8.1:8082/music/OnTheGround-ROSE.mp3",
                CoverImage = "http://192.168.8.1:8082/music/OnTheGround-ROSE.jpg",
                Like = 78
            };
        }
        Song Solo()
        {
            return new Song
            {
                Name = "Solo",
                Singer = "JENNY",
                Url = "http://192.168.8.1:8082/music/Solo-Jennie.mp3",
                CoverImage = "http://192.168.8.1:8082/music/Solo-Jennie.jpg",
                Like = 78
            };
        }
        Song ViYeuCuDamDau()
        {
            return new Song
            {
                Name = "Vì Yêu Cứ Đâm Đầu",
                Singer = "MIN",
                Url = "http://192.168.8.1:8082/music/ViYeuCuDamDau-MIN.mp3",
                CoverImage = "http://192.168.8.1:8082/music/ViYeuCuDamDau-MIN.jpg",
                Like = 78
            };
        }
        #endregion
        #region account
        Account account1()
        {
            ObservableCollection<Song> l = new ObservableCollection<Song>();
            l.Add(DoAnhDoanDuoc());
            l.Add(HayTraoChoAnh());
            l.Add(DiDeTroVe());
            l.Add(EmChaoTet());
            l[1].IsRecent = true;
            return new Account
            {
                Email = "normalaccount@gmail.com",
                Password = "123456",
                Type = TypeOfAccount.NormalUser,
                Playlist = l,
                FirstName = "Normal",
                LastName = "Account"
            };
        }
        Account account2()
        {
            return new Account
            {
                Email = "banaccount@gmail.com",
                Password = "123456",
                Type = TypeOfAccount.Banned,
                FirstName = "Banned",
                LastName = "Account"
            };
        }
        #endregion
    }
}
