using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using DevExpress.Xpf.Core;
using MahApps.Metro.Controls;
using WebCam_Capture;
using Image = System.Windows.Controls.Image;

namespace whostpos.Windows
{
    /// <summary>
    /// Interaction logic for WebCamWindow.xaml
    /// </summary>
    public partial class WebCamWindow : MetroWindow
    {
        public static BitmapSource Bs;
        public static IntPtr Ip;
        private readonly Image _frameImage;
        private readonly WebCamCapture _webCam;
        private byte[] _imageInBytes;
        private const int FrameNumber = 30;
        private readonly string _applicationName;
        public WebCamWindow(string applicationName)
        {
            InitializeComponent();
            _webCam = new WebCamCapture { FrameNumber = ((0ul)), TimeToCapture_milliseconds = FrameNumber };
            _webCam.ImageCaptured += webCam_ImageCaptured;
            _frameImage = SourceImage;
            _applicationName = applicationName;
        }

        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr handel);

        private void webCam_ImageCaptured(object source, WebcamEventArgs e)
        {
            try
            {
                Ip = ((Bitmap)e.WebCamImage).GetHbitmap();
                Bs = Imaging.CreateBitmapSourceFromHBitmap(Ip, IntPtr.Zero, Int32Rect.Empty,
                                                           BitmapSizeOptions.FromEmptyOptions());
                DeleteObject(Ip);
                _frameImage.Source = Bs;
            }
            catch (Exception errorException)
            {
                DXMessageBox.Show(errorException.Message, _applicationName, MessageBoxButton.OK,
                                  MessageBoxImage.Error);
            }
        }

        public void CamDone(BitmapSource bitmapSource)
        {
            if (bitmapSource != null)
            {
                using (var memStrem = new MemoryStream())
                {
                    var jpgeEncoder = new JpegBitmapEncoder();
                    jpgeEncoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                    jpgeEncoder.Save(memStrem);
                    _imageInBytes = memStrem.GetBuffer();
                }
                _webCam.Stop();
                Close();
            }
            else
            {
                DXMessageBox.Show("Please capture image first.", _applicationName, MessageBoxButton.OK,
                                  MessageBoxImage.Error);
            }
        }

        public static byte[] CaptureImage(string applicationName)
        {
            var camCapture = new WebCamWindow(applicationName);
            camCapture.ShowDialog();
            if (camCapture._imageInBytes != null)
            {
                return camCapture._imageInBytes;
            }
            return default(byte[]);
        }

        private void Strat_Click(object sender, RoutedEventArgs e)
        {
            _webCam.TimeToCapture_milliseconds = FrameNumber;
            _webCam.Start(0);
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            _webCam.Stop();
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            _webCam.TimeToCapture_milliseconds = FrameNumber;
            _webCam.Start(_webCam.FrameNumber);
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            CamDone(GetImage.Source as BitmapSource);
        }

        private void Capture_Click(object sender, RoutedEventArgs e)
        {
            GetImage.Source = SourceImage.Source;
        }

        private void VideoFormate_Click(object sender, RoutedEventArgs e)
        {
            _webCam.Config();
        }

        private void VideoSource_Click(object sender, RoutedEventArgs e)
        {
            _webCam.Config2();
        }
    }
}