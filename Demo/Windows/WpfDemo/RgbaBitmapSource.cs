using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfDemo
{
    public class RgbaBitmapSource : BitmapSource
    {
        private byte[] rgbaBuffer;
        private int pixelWidth;
        private int pixelHeight;

        public RgbaBitmapSource(byte[] rgbaBuffer, int pixelWidth)
        {
            this.rgbaBuffer = rgbaBuffer;
            this.pixelWidth = pixelWidth;
            this.pixelHeight = rgbaBuffer.Length / (4 * pixelWidth);
        }

        public override void CopyPixels(
            Int32Rect sourceRect, Array pixels, int stride, int offset)
        {
            for (int y = sourceRect.Y; y < sourceRect.Y + sourceRect.Height; y++)
            {
                for (int x = sourceRect.X; x < sourceRect.X + sourceRect.Width; x++)
                {
                    int i = stride * y + 4 * x;
                    byte a = rgbaBuffer[i + 3];
                    byte r = (byte)(rgbaBuffer[i] * a / 256); // pre-multiplied R
                    byte g = (byte)(rgbaBuffer[i + 1] * a / 256); // pre-multiplied G
                    byte b = (byte)(rgbaBuffer[i + 2] * a / 256); // pre-multiplied B

                    pixels.SetValue(b, i + offset);
                    pixels.SetValue(g, i + offset + 1);
                    pixels.SetValue(r, i + offset + 2);
                    pixels.SetValue(a, i + offset + 3);
                }
            }
        }

        protected override Freezable CreateInstanceCore()
        {
            return new RgbaBitmapSource(rgbaBuffer, pixelWidth);
        }

        public override event EventHandler<DownloadProgressEventArgs> DownloadProgress;
        public override event EventHandler DownloadCompleted;
        public override event EventHandler<ExceptionEventArgs> DownloadFailed;
        public override event EventHandler<ExceptionEventArgs> DecodeFailed;

        public override double DpiX
        {
            get { return 96; }
        }

        public override double DpiY
        {
            get { return 96; }
        }

        public override PixelFormat Format
        {
            get { return PixelFormats.Pbgra32; }
        }

        public override int PixelWidth
        {
            get { return pixelWidth; }
        }

        public override int PixelHeight
        {
            get { return pixelHeight; }
        }

        public override double Width
        {
            get { return pixelWidth; }
        }

        public override double Height
        {
            get { return pixelHeight; }
        }
    }
}
