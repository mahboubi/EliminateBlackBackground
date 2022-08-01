namespace WinFormsCropApp
{
    partial class FormCrop
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
            this.components = new System.ComponentModel.Container();
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.btnCropImage = new System.Windows.Forms.Button();
            this.lblFileName = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.pictureBoxCrop = new System.Windows.Forms.PictureBox();
            this.checkBoxGama = new System.Windows.Forms.CheckBox();
            this.checkBoxRotate = new System.Windows.Forms.CheckBox();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.labelProgress = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.btnOcr = new System.Windows.Forms.Button();
            this.textOcr = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCrop)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.Location = new System.Drawing.Point(395, 22);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(179, 59);
            this.btnLoadImage.TabIndex = 0;
            this.btnLoadImage.Text = "Load Image";
            this.btnLoadImage.UseVisualStyleBackColor = true;
            this.btnLoadImage.EnabledChanged += new System.EventHandler(this.btnLoadImage_EnabledChanged);
            this.btnLoadImage.Click += new System.EventHandler(this.btnLoadImage_Click);
            // 
            // btnCropImage
            // 
            this.btnCropImage.Enabled = false;
            this.btnCropImage.Location = new System.Drawing.Point(753, 22);
            this.btnCropImage.Name = "btnCropImage";
            this.btnCropImage.Size = new System.Drawing.Size(174, 59);
            this.btnCropImage.TabIndex = 1;
            this.btnCropImage.Text = "Crop Image";
            this.btnCropImage.UseVisualStyleBackColor = true;
            this.btnCropImage.EnabledChanged += new System.EventHandler(this.btnCropImage_EnabledChanged);
            this.btnCropImage.Click += new System.EventHandler(this.btnCropImage_Click);
            // 
            // lblFileName
            // 
            this.lblFileName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFileName.Location = new System.Drawing.Point(29, 22);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(360, 58);
            this.lblFileName.TabIndex = 2;
            this.lblFileName.Text = "...";
            this.lblFileName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFileName.TextChanged += new System.EventHandler(this.lblFileName_TextChanged);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox.Location = new System.Drawing.Point(27, 105);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(595, 540);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 3;
            this.pictureBox.TabStop = false;
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            // 
            // pictureBoxCrop
            // 
            this.pictureBoxCrop.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxCrop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxCrop.Location = new System.Drawing.Point(628, 105);
            this.pictureBoxCrop.Name = "pictureBoxCrop";
            this.pictureBoxCrop.Size = new System.Drawing.Size(631, 540);
            this.pictureBoxCrop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxCrop.TabIndex = 4;
            this.pictureBoxCrop.TabStop = false;
            // 
            // checkBoxGama
            // 
            this.checkBoxGama.AutoSize = true;
            this.checkBoxGama.Location = new System.Drawing.Point(604, 3);
            this.checkBoxGama.Name = "checkBoxGama";
            this.checkBoxGama.Size = new System.Drawing.Size(143, 29);
            this.checkBoxGama.TabIndex = 5;
            this.checkBoxGama.Text = "Gamma Filter";
            this.checkBoxGama.UseVisualStyleBackColor = true;
            // 
            // checkBoxRotate
            // 
            this.checkBoxRotate.AutoSize = true;
            this.checkBoxRotate.Location = new System.Drawing.Point(604, 38);
            this.checkBoxRotate.Name = "checkBoxRotate";
            this.checkBoxRotate.Size = new System.Drawing.Size(89, 29);
            this.checkBoxRotate.TabIndex = 9;
            this.checkBoxRotate.Text = "Rotate";
            this.checkBoxRotate.UseVisualStyleBackColor = true;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.Location = new System.Drawing.Point(1237, 39);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(22, 25);
            this.labelProgress.TabIndex = 10;
            this.labelProgress.Text = "0";
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // btnOcr
            // 
            this.btnOcr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOcr.Enabled = false;
            this.btnOcr.Location = new System.Drawing.Point(1552, 22);
            this.btnOcr.Name = "btnOcr";
            this.btnOcr.Size = new System.Drawing.Size(177, 59);
            this.btnOcr.TabIndex = 11;
            this.btnOcr.Text = "OCR Image";
            this.btnOcr.UseVisualStyleBackColor = true;
            this.btnOcr.Click += new System.EventHandler(this.btnOcr_Click);
            // 
            // textOcr
            // 
            this.textOcr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textOcr.Location = new System.Drawing.Point(1265, 105);
            this.textOcr.Name = "textOcr";
            this.textOcr.ReadOnly = true;
            this.textOcr.Size = new System.Drawing.Size(464, 540);
            this.textOcr.TabIndex = 12;
            this.textOcr.Text = "";
            // 
            // FormCrop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1741, 657);
            this.Controls.Add(this.textOcr);
            this.Controls.Add(this.btnOcr);
            this.Controls.Add(this.labelProgress);
            this.Controls.Add(this.checkBoxRotate);
            this.Controls.Add(this.checkBoxGama);
            this.Controls.Add(this.pictureBoxCrop);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.btnCropImage);
            this.Controls.Add(this.btnLoadImage);
            this.Name = "FormCrop";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Crop Image...";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCrop)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnLoadImage;
        private Button btnCropImage;
        private Label lblFileName;
        private OpenFileDialog openFileDialog;
        private PictureBox pictureBox;
        private PictureBox pictureBoxCrop;
        private CheckBox checkBoxGama;
        private CheckBox checkBoxRotate;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private Label labelProgress;
        private System.Windows.Forms.Timer timer;
        private Button btnOcr;
        private RichTextBox textOcr;
    }
}