using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.ComponentModel;
using System.Data;
using Emgu.CV.IntensityTransform;
using Emgu.CV.OCR;


namespace WinFormsCropApp
{
    public partial class FormCrop : Form
    {

        int counter = 1;
        string croppedFileName = string.Empty;
        public FormCrop()
        {
            InitializeComponent();
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            var result = openFileDialog.ShowDialog(this);
            lblFileName.Text = openFileDialog.FileName;
            pictureBox.Image = Image.FromFile(lblFileName.Text);
        }

        private void btnCropImage_Click(object sender, EventArgs e)
        {
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            btnCropImage.Enabled = false;
            btnLoadImage.Enabled = false;
            timer.Enabled = true;
            backgroundWorker.RunWorkerAsync();          
        }

        private void BackgroundWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
           
        }

        private void BackgroundWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            btnCropImage.Enabled = true;
            btnLoadImage.Enabled = true;
            counter = 1;
            timer.Enabled = false;
            labelProgress.Text = "0";
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {        
            ProcessImage(lblFileName.Text);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            labelProgress.Text = counter.ToString();
            counter++;
        }

        private void btnLoadImage_EnabledChanged(object sender, EventArgs e)
        {
            
        }

        private void btnCropImage_EnabledChanged(object sender, EventArgs e)
        {
            if (!btnCropImage.Enabled)
                btnCropImage.Text = "Crop Image...";
            else
                btnCropImage.Text = "Crop Image";
        }

        private void lblFileName_TextChanged(object sender, EventArgs e)
        {
            btnCropImage.Enabled = true;
            btnOcr.Enabled = true;
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            
        }

        void ProcessImage(string imagePath)
        {
            var fileInfo = new FileInfo(imagePath);
            var imagesPath = fileInfo.DirectoryName;
            var filename = fileInfo.Name.Replace(fileInfo.Extension, "");
            var extention = fileInfo.Extension;
            var imagepath = imagesPath + @"\" + filename + extention;

            Mat img = new Mat(imagepath);
            Mat gama = new Mat();

            if (checkBoxGama.Checked)
                IntensityTransformInvoke.GammaCorrection(img, img, 0.5F);

          
            using (UMat gray = new UMat())
            using (UMat cannyEdges = new UMat())
            using (Mat lineImage = new Mat(img.Size, DepthType.Cv8U, 3)) //image to drtaw lines on
            {
                //Convert the image to grayscale and filter out the noise
                
                UMat threshold = new UMat();
                CvInvoke.CvtColor(img, gray, ColorConversion.Bgr2Gray);
                
                double cannyThreshold = 180.0;
                
                CvInvoke.GaussianBlur(gray, gray, new Size(3, 3), 1);

                CvInvoke.Threshold(gray, gray, 210, 100, ThresholdType.Binary);

                #region Canny and edge detection
                double cannyThresholdLinking = 120.0;
                CvInvoke.Canny(gray, cannyEdges, cannyThreshold, cannyThresholdLinking);

                LineSegment2D[] lines = CvInvoke.HoughLinesP(
                    cannyEdges,
                    1, //Distance resolution in pixel-related units
                    Math.PI / 45.0, //Angle resolution measured in radians.
                    20, //threshold
                    50, //min Line width
                    10); //gap between lines

                List<DestPoint> dests00 = new List<DestPoint>();
                List<DestPoint> dests0x = new List<DestPoint>();

                Image image = Bitmap.FromFile(imagepath);

                foreach (var line in lines)
                {
                    
                    var dest1 = Math.Sqrt((line.P1.Y * line.P1.Y));
                    dests00.Add(new DestPoint() { point = new Point() { X = (int)line.P1.X, Y = (int)line.P1.Y }, dest = dest1, Ditection = line.Direction });

                    var dest2 = Math.Sqrt((line.P2.Y * line.P2.Y));
                    dests00.Add(new DestPoint() { point = new Point() { X = (int)line.P1.X, Y = (int)line.P1.Y }, dest = dest2, Ditection = line.Direction });

                    dest1 = Math.Sqrt(((image.Width - line.P1.X) * (image.Width - line.P1.X)));
                    dests0x.Add(new DestPoint() { point = new Point() { X = (int)line.P1.X, Y = (int)line.P1.Y }, dest = dest1, Ditection = line.Direction });

                    dest2 = Math.Sqrt(((image.Width - line.P2.X) * (image.Width - line.P2.X)));
                    dests0x.Add(new DestPoint() { point = new Point() { X = (int)line.P1.X, Y = (int)line.P1.Y }, dest = dest2, Ditection = line.Direction });

                }

                var closePoint = dests00.OrderBy(x => x.dest).First();
                var farPoint = dests00.OrderByDescending(x => x.dest).First();

                var closePointOX = dests0x.OrderBy(x => x.dest).First();
                var farPoint0X = dests0x.OrderByDescending(x => x.dest).First();

                var degClose = Math.Atan2((closePointOX.point.Y - closePoint.point.Y), (closePointOX.point.X - closePoint.point.X)) * (180 / Math.PI);

                var degFar = Math.Atan2((farPoint0X.point.Y - farPoint.point.Y), (farPoint0X.point.X - farPoint.point.X)) * (180 / Math.PI);

                double destWidth = 0;
                double destHeight = 0;

                if ((degClose > 0 & (degFar < 0 || degFar == 0)))
                {
                    destWidth = Math.Sqrt(Math.Pow((closePointOX.point.X - farPoint0X.point.X), 2) + Math.Pow((closePoint.point.Y - closePoint.point.Y), 2));

                    destHeight = Math.Sqrt(Math.Pow((farPoint0X.point.X - farPoint0X.point.X), 2) + Math.Pow((farPoint.point.Y - closePoint.point.Y), 2));
                }
                else

                if (degClose < 0 & degFar > 0)
                {
                    destWidth = Math.Sqrt(Math.Pow((farPoint.point.X - closePoint.point.X), 2) + Math.Pow((closePointOX.point.Y - closePointOX.point.Y), 2));

                    destHeight = Math.Sqrt(Math.Pow((closePoint.point.X - closePoint.point.X), 2) + Math.Pow((farPoint0X.point.Y - closePoint.point.Y), 2));
                }

                else

                if (degClose < 0 & (degFar < 0 || degFar == 0))
                {
                    destWidth = Math.Sqrt(Math.Pow((closePointOX.point.X - closePoint.point.X), 2) + Math.Pow((closePointOX.point.Y - closePointOX.point.Y), 2));

                    destHeight = Math.Sqrt(Math.Pow((closePoint.point.X - closePoint.point.X), 2) + Math.Pow((farPoint0X.point.Y - closePoint.point.Y), 2));
                }

                else

                if (degClose > 0 & degFar > 0)
                {
                    destWidth = Math.Sqrt(Math.Pow((closePointOX.point.X - closePoint.point.X), 2) + Math.Pow((closePointOX.point.Y - closePointOX.point.Y), 2));

                    destHeight = Math.Sqrt(Math.Pow((closePoint.point.X - closePoint.point.X), 2) + Math.Pow((farPoint0X.point.Y - closePoint.point.Y), 2));
                }

                destHeight = destHeight + 100;
                destWidth = destWidth + 100;
                closePoint.point = new Point() { X = closePoint.point.X - 50, Y = closePoint.point.Y - 50 };
                closePointOX.point = new Point() { X = closePointOX.point.X - 50, Y = closePointOX.point.Y - 50 };
                farPoint.point = new Point() { X = farPoint.point.X - 50, Y = farPoint.point.Y - 50 };
                farPoint0X.point = new Point() { X = farPoint0X.point.X - 50, Y = farPoint0X.point.Y - 50 };
                
                Image destinationImage = new Bitmap((int)destWidth, (int)destHeight);

                using (Graphics g = Graphics.FromImage(destinationImage))
                {
                    float x = 0.0F;
                    float y = 0.0F;

                    RectangleF srcRect;
                    if (degClose > 0 & degFar < 0)
                    {
                        srcRect = new RectangleF(farPoint0X.point.X, closePoint.point.Y, (float)destWidth, (float)destHeight);
                        g.DrawImage(image, x, y, srcRect, GraphicsUnit.Pixel);
                    }
                    else
                    if (degClose > 0 & degFar > 0)
                    {
                        srcRect = new RectangleF(closePoint.point.X, closePointOX.point.Y, (float)destWidth, (float)destHeight);
                        g.DrawImage(image, x, y, srcRect, GraphicsUnit.Pixel);
                    }
                    else
                     if (degClose < 0 & degFar > 0)
                    {
                        srcRect = new RectangleF(closePoint.point.X, closePointOX.point.Y, (float)destWidth, (float)destHeight);
                        g.DrawImage(image, x, y, srcRect, GraphicsUnit.Pixel);
                    }
                    else
                     if (degClose < 0 & degFar < 0)
                    {                       
                        srcRect = new RectangleF(closePoint.point.X, closePointOX.point.Y, (float)destWidth, (float)destHeight);
                        g.DrawImage(image, x, y, srcRect, GraphicsUnit.Pixel);
                    }
                    else
                    if (degClose < 0 & degFar == 0)
                    {
                        srcRect = new RectangleF(closePoint.point.X, closePointOX.point.Y, (float)destWidth, (float)destHeight);
                        g.DrawImage(image, x, y, srcRect, GraphicsUnit.Pixel);
                    }
                    else
                    if (degClose > 0 & degFar == 0)
                    {
                        srcRect = new RectangleF(farPoint0X.point.X, closePoint.point.Y, (float)destWidth, (float)destHeight);
                        g.DrawImage(image, x, y, srcRect, GraphicsUnit.Pixel);
                    }

                    if (checkBoxRotate.Checked)
                        destinationImage.RotateFlip(RotateFlipType.RotateNoneFlipY);
                                       
                    g.DrawString("@Cropped Image", new Font(new FontFamily("Tahoma"), 50, FontStyle.Bold), Brushes.Red, new PointF() { X = 0, Y = 0 });

                   
                    croppedFileName = imagesPath + @"\" + filename + "_cropped" + extention;
                    destinationImage.Save(croppedFileName);
                    pictureBoxCrop.Image = destinationImage;

                  
                }
                #endregion

            }
        }

     
      
        private void btnOcr_Click(object sender, EventArgs e)
        {
            string result = string.Empty;
            Task.Run(() =>
            {
               
                using (var img =  new Mat(croppedFileName))
                {                 
                    string tessdata = @"C:\.....\tessdata";
                    using (var ocrProvider = new Tesseract(tessdata, "eng", OcrEngineMode.TesseractOnly))  //TesseractCubeCombined
                    {
                        ocrProvider.SetImage(img); //Recognize
                        result = ocrProvider.GetUTF8Text().TrimEnd(); //GetText
                       
                        textOcr.Invoke((MethodInvoker)delegate
                        {
                            textOcr.Text = result;
                        });
                    }
                }
            });

            
        }
    }

  
    public class DestPoint
    {
        public Point point { get; set; }

        public double dest { get; set; }

        public PointF Ditection { get; set; }
    }
}