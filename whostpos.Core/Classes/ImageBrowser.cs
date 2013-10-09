using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using whostpos.Core.BaseClass;
using whostpos.Core.Interface;
using whostpos.Windows;

namespace whostpos.Core.Classes
{
    public class ImageBrowser : IImageBrowser
    {
        public byte[] GetFromFile()
        {
            using (var imageOpenBox = new OpenFileDialog())
            {
                imageOpenBox.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                imageOpenBox.Filter = "JPGE (*.jpg)|*.jpg|PNG (*.png)|*.png|BMP (*.bmp)|*.bmp|All (*.*)|*.*";
                imageOpenBox.FilterIndex = 0;
                imageOpenBox.RestoreDirectory = true;
                if (imageOpenBox.ShowDialog().Equals(DialogResult.OK))
                {
                    var bitMapImage = new Bitmap(imageOpenBox.FileName);
                    var bmpFormate = bitMapImage.RawFormat;
                    var imagetoConvert = Image.FromFile(imageOpenBox.FileName);
                    using (var ms = new MemoryStream())
                    {
                        imagetoConvert.Save(ms, bmpFormate);
                        return ms.ToArray();
                    }
                }
                return null;
            }
        }

        public byte[] GetFromWebCam()
        {
            return WebCamWindow.CaptureImage(new BaseViewModel("").ApplicationName);
        }
    }
}