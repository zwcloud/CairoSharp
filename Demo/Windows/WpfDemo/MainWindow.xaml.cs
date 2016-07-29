using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Cairo;

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
            var image = (Image)sender;
            
            using (var surface = new ImageSurface(Format.Argb32, (int)image.Width, (int)image.Height))
            using (var context = new Context(surface))
            {
                var p1 = new PointD(10, 10);
                var p2 = new PointD(100, 10);
                var p3 = new PointD(100, 100);
                var p4 = new PointD(10, 100);

                context.MoveTo(p1);
                context.LineTo(p2);
                context.LineTo(p3);
                context.LineTo(p4);
                context.LineTo(p1);
                context.ClosePath();
                context.Fill();
                context.MoveTo(140, 110);
                context.SetFontSize(32);
                context.SetSourceColor(new Cairo.Color(0, 0, 0.8, 1));
                context.ShowText("Hello Cairo!");

                surface.Flush();

                RgbaBitmapSource source = new RgbaBitmapSource(surface.Data, surface.Width);
                image.Source = source;
            }

        }

    }
}
