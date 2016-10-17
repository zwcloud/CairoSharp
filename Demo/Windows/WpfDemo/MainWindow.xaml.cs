using Cairo;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WpfDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            Image image = (Image)sender;
            using (ImageSurface surface = new ImageSurface(Format.Argb32, (int)image.Width, (int)image.Height))
            {
                using (Context context = new Context(surface))
                {
                    PointD p = new PointD(10.0, 10.0);
                    PointD p2 = new PointD(100.0, 10.0);
                    PointD p3 = new PointD(100.0, 100.0);
                    PointD p4 = new PointD(10.0, 100.0);
                    context.MoveTo(p);
                    context.LineTo(p2);
                    context.LineTo(p3);
                    context.LineTo(p4);
                    context.LineTo(p);
                    context.ClosePath();
                    context.Fill();
                    context.MoveTo(140.0, 110.0);
                    context.SetFontSize(32.0);
                    context.SetSourceColor(new Color(0.0, 0.0, 0.8, 1.0));
                    context.ShowText("Hello Cairo!");
                    surface.Flush();
                    RgbaBitmapSource source = new RgbaBitmapSource(surface.Data, surface.Width);
                    image.Source = source;
                }
            }
        }
    }
}
