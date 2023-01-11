namespace MusicServer
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listClient = new System.Windows.Forms.ListView();
            this.EndPoint = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LastMsg = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RecTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listClient
            // 
            this.listClient.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.EndPoint,
            this.ID,
            this.LastMsg,
            this.RecTime});
            this.listClient.HideSelection = false;
            this.listClient.Location = new System.Drawing.Point(12, 12);
            this.listClient.Name = "listClient";
            this.listClient.Size = new System.Drawing.Size(776, 426);
            this.listClient.TabIndex = 0;
            this.listClient.UseCompatibleStateImageBehavior = false;
            this.listClient.View = System.Windows.Forms.View.Details;
            // 
            // EndPoint
            // 
            this.EndPoint.Text = "EndPoint";
            this.EndPoint.Width = 125;
            // 
            // ID
            // 
            this.ID.Text = "ID";
            this.ID.Width = 147;
            // 
            // LastMsg
            // 
            this.LastMsg.Text = "Last Msg";
            this.LastMsg.Width = 157;
            // 
            // RecTime
            // 
            this.RecTime.Text = "Last Received Time";
            this.RecTime.Width = 146;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listClient);
            this.Name = "Main";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listClient;
        private System.Windows.Forms.ColumnHeader EndPoint;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader LastMsg;
        private System.Windows.Forms.ColumnHeader RecTime;
    }
}

