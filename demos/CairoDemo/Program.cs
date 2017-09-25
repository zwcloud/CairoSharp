using System;
using System.Diagnostics;
using System.IO;
using Cairo;
using IOPath = System.IO.Path;

namespace CairoDemo
{
    static class Program
    {
        static void Main()
        {
            if (Directory.Exists("output")) Directory.Delete("output", true);
            Directory.CreateDirectory("output");
            Environment.CurrentDirectory = IOPath.Combine(Environment.CurrentDirectory, "output");

            try
            {
                Primitives();
                AntiAlias();
                Mask();
                Demo01();
                Demo02();
                Arrow();
                Hexagon();
            }
            catch (Exception ex) when (!Debugger.IsAttached)
            {
                Console.WriteLine(ex);
            }

            if (Debugger.IsAttached)
            {
                Console.WriteLine("\nEnd.");
                Console.ReadKey();
            }
        }
        //---------------------------------------------------------------------
        private static void Primitives()
        {
            Action<Surface> draw = surface =>
            {
                using (var c = new Context(surface))
                {
                    c.Scale(4, 4);

                    // Stroke:
                    c.LineWidth = 0.1;
                    c.Color = new Color(0, 0, 0);
                    c.Rectangle(10, 10, 10, 10);
                    c.Stroke();

                    c.Save();
                    {
                        c.Color = new Color(0, 0, 0);
                        c.Translate(20, 5);
                        c.MoveTo(0, 0);
                        c.LineTo(10, 5);
                        c.Stroke();
                    }
                    c.Restore();

                    // Fill:
                    c.Color = new Color(0, 0, 0);
                    c.SetSourceRGB(0, 0, 0);
                    c.Rectangle(10, 30, 10, 10);
                    c.Fill();

                    // Text:
                    c.Color = new Color(0, 0, 0);
                    c.SelectFontFace("Georgia", FontSlant.Normal, FontWeight.Bold);
                    c.SetFontSize(10);
                    TextExtents te = c.TextExtents("a");
                    c.MoveTo(
                        0.5 - te.Width / 2 - te.XBearing + 10,
                        0.5 - te.Height / 2 - te.YBearing + 50);
                    c.ShowText("a");

                    c.Color = new Color(0, 0, 0);
                    c.SelectFontFace("Arial", FontSlant.Normal, FontWeight.Bold);
                    c.SetFontSize(10);
                    te = c.TextExtents("a");
                    c.MoveTo(
                        0.5 - te.Width / 2 - te.XBearing + 10,
                        0.5 - te.Height / 2 - te.YBearing + 60);
                    c.ShowText("a");
                }
            };

            using (Surface surface = new ImageSurface(Format.Argb32, 200, 3200))
            {
                draw(surface);
                surface.WriteToPng("primitives.png");
            }

            using (Surface surface = new PdfSurface("primitives.pdf", 200, 3200))
                draw(surface);

            using (Surface surface = new SvgSurface("primitives.svg", 200, 3200))
                draw(surface);
        }
        //---------------------------------------------------------------------
        private static void AntiAlias()
        {
            Action<Surface> draw = surface =>
            {
                using (var ctx = new Context(surface))
                {
                    // Sets the anti-aliasing method:
                    ctx.Antialias = Antialias.Subpixel;

                    // Sets the line width:
                    ctx.LineWidth = 9;

                    // red, green, blue, alpha:
                    ctx.Color = new Color(0, 0, 0, 1);

                    // Sets the Context's start point:
                    ctx.MoveTo(10, 10);

                    // Draws a "virtual" line:
                    ctx.LineTo(40, 60);

                    // Stroke the line to the image surface:
                    ctx.Stroke();

                    ctx.Antialias = Antialias.Gray;
                    ctx.LineWidth = 8;
                    ctx.Color = new Color(1, 0, 0, 1);
                    ctx.LineCap = LineCap.Round;
                    ctx.MoveTo(10, 50);
                    ctx.LineTo(40, 100);
                    ctx.Stroke();

                    // Fastest method but low quality:
                    ctx.Antialias = Antialias.None;
                    ctx.LineWidth = 7;
                    ctx.MoveTo(10, 90);
                    ctx.LineTo(40, 140);
                    ctx.Stroke();
                }
            };

            using (Surface surface = new ImageSurface(Format.Argb32, 70, 150))
            {
                draw(surface);

                // Save the image as a png image:
                surface.WriteToPng("antialias.png");
            }

            using (Surface surface = new PdfSurface("antialias.pdf", 70, 150))
                draw(surface);

            using (Surface surface = new SvgSurface("antialias.svg", 70, 150))
                draw(surface);
        }
        //---------------------------------------------------------------------
        private static void Mask()
        {
            Action<Surface> draw = surface =>
            {
                using (var ctx = new Context(surface))
                {
                    ctx.Scale(500, 500);

                    Gradient linpat = new LinearGradient(0, 0, 1, 1);
                    linpat.AddColorStop(0, new Color(0, 0.3, 0.8));
                    linpat.AddColorStop(1, new Color(1, 0.8, 0.3));

                    Gradient radpat = new RadialGradient(0.5, 0.5, 0.25, 0.5, 0.5, 0.6);
                    radpat.AddColorStop(0, new Color(0, 0, 0, 1));
                    radpat.AddColorStop(1, new Color(0, 0, 0, 0));

                    ctx.Source = linpat;
                    //ctx.Paint();
                    ctx.Mask(radpat);
                }
            };

            using (Surface surface = new ImageSurface(Format.Argb32, 500, 500))
            {
                draw(surface);
                surface.WriteToPng("mask.png");
            }

            using (Surface surface = new PdfSurface("mask.pdf", 500, 500))
                draw(surface);

            using (Surface surface = new SvgSurface("mask.svg", 500, 500))
                draw(surface);
        }
        //---------------------------------------------------------------------
        private static void Demo01()
        {
            Action<Surface> draw = surface =>
            {
                using (var c = new Context(surface))
                {
                    c.Scale(500, 500);

                    c.Color = new Color(0, 0, 0);
                    c.MoveTo(0, 0);         // absetzen und neu beginnen
                    c.LineTo(1, 1);
                    c.MoveTo(1, 0);
                    c.LineTo(0, 1);
                    c.LineWidth = 0.2;
                    c.Stroke();             // Lininen zeichnen

                    c.Rectangle(0, 0, 0.5, 0.5);
                    c.Color = new Color(1, 0, 0, 0.8);
                    c.Fill();

                    c.Rectangle(0, 0.5, 0.5, 0.5);
                    c.Color = new Color(0, 1, 0, 0.6);
                    c.Fill();

                    c.Rectangle(0.5, 0, 0.5, 0.5);
                    c.Color = new Color(0, 0, 0, 0.4);
                    c.Fill();
                }
            };

            using (Surface surface = new ImageSurface(Format.Argb32, 500, 500))
            {
                draw(surface);
                surface.WriteToPng("demo01.png");
            }

            using (Surface surface = new PdfSurface("demo01.pdf", 500, 500))
                draw(surface);

            using (Surface surface = new SvgSurface("demo01.svg", 500, 500))
                draw(surface);
        }
        //---------------------------------------------------------------------
        private static void Demo02()
        {
            Action<Surface> draw = surface =>
            {
                using (var c = new Context(surface))
                {
                    c.Scale(500, 500);

                    Gradient radpat = new RadialGradient(0.25, 0.25, 0.1, 0.5, 0.5, 0.5);
                    radpat.AddColorStop(0, new Color(1.0, 0.8, 0.8));
                    radpat.AddColorStop(1, new Color(0.9, 0.0, 0.0));

                    for (int i = 1; i < 10; i++)
                        for (int j = 1; j < 10; j++)
                            c.Rectangle(i / 10d - 0.04, j / 10d - 0.04, 0.08, 0.08);
                    c.Source = radpat;
                    c.Fill();

                    Gradient linpat = new LinearGradient(0.25, 0.35, 0.75, 0.65);
                    linpat.AddColorStop(0.00, new Color(1, 1, 1, 0));
                    linpat.AddColorStop(0.25, new Color(0, 1, 0, 0.5));
                    linpat.AddColorStop(0.50, new Color(1, 1, 1, 0));
                    linpat.AddColorStop(0.75, new Color(0, 0, 1, 0.5));
                    linpat.AddColorStop(1.00, new Color(1, 1, 1, 0));

                    c.Rectangle(0, 0, 1, 1);
                    c.Source = linpat;
                    c.Fill();
                }
            };

            using (Surface surface = new ImageSurface(Format.Argb32, 500, 500))
            {
                draw(surface);
                surface.WriteToPng("demo02.png");
            }

            using (Surface surface = new PdfSurface("demo02.pdf", 500, 500))
                draw(surface);

            using (Surface surface = new SvgSurface("demo02.svg", 500, 500))
                draw(surface);
        }
        //---------------------------------------------------------------------
        private static void Arrow()
        {
            Action<Surface> draw = surface =>
            {
                using (var c = new Context(surface))
                {
                    c.Scale(500, 500);

                    // Hat nur für PNG Relevanz:
                    c.Antialias = Antialias.Subpixel;

                    // Linienweite, wegen Maßstab so:
                    double ux = 1, uy = 1;
                    c.InverseTransformDistance(ref ux, ref uy);
                    c.LineWidth = Math.Max(ux, uy);

                    c.Color = new Color(0, 0, 1);
                    c.MoveTo(0.1, 0.10);
                    c.LineTo(0.9, 0.45);
                    c.Stroke();

                    c.Arrow(0.1, 0.50, 0.9, 0.95, 0.05, 10);
                    c.Stroke();
                }
            };

            using (Surface surface = new ImageSurface(Format.Argb32, 500, 500))
            {
                draw(surface);
                surface.WriteToPng("arrow.png");
            }

            using (Surface surface = new PdfSurface("arrow.pdf", 500, 500))
                draw(surface);

            using (Surface surface = new SvgSurface("arrow.svg", 500, 500))
                draw(surface);
        }
        //---------------------------------------------------------------------
        private static void Hexagon()
        {
            Func<double, PointD[]> getHexagonPoints = cellSize =>
            {
                double ri = cellSize / 2;
                double r = 2 * ri / Math.Sqrt(3);

                var p1 = new PointD(0, r);
                var p2 = new PointD(ri, r / 2);
                var p3 = new PointD(ri, -r / 2);
                var p4 = new PointD(0, -r);
                var p5 = new PointD(-ri, -r / 2);
                var p6 = new PointD(-ri, r / 2);
                PointD[] hexagon = { p1, p2, p3, p4, p5, p6 };

                return hexagon;
            };

            Action<Surface> draw = surface =>
            {
                using (var c = new Context(surface))
                {
                    c.Scale(500, 500);

                    // Hat nur für PNG Relevanz:
                    c.Antialias = Antialias.Subpixel;

                    // Linienweite, wegen Maßstab so:
                    double ux = 1, uy = 1;
                    c.InverseTransformDistance(ref ux, ref uy);
                    c.LineWidth = Math.Max(ux, uy);

                    PointD[] hexagon = getHexagonPoints(0.5);
                    c.Save();
                    {
                        c.Translate(0.5, 0.5);
                        c.MoveTo(hexagon[0]);
                        c.LineTo(hexagon[1]);
                        c.LineTo(hexagon[2]);
                        c.LineTo(hexagon[3]);
                        c.LineTo(hexagon[4]);
                        c.LineTo(hexagon[5]);
                        c.ClosePath();
                        c.Stroke();
                    }
                    c.Restore();

                    c.Color = new Color(0, 0, 1);
                    ux = 0.1; uy = 0.1;
                    c.InverseTransformDistance(ref ux, ref uy);
                    c.LineWidth = Math.Max(ux, uy);

                    c.MoveTo(0.5, 0);
                    c.LineTo(0.5, 1);
                    c.MoveTo(0, 0.5);
                    c.LineTo(1, 0.5);
                    c.Stroke();
                }
            };

            using (Surface surface = new ImageSurface(Format.Argb32, 500, 500))
            {
                draw(surface);
                surface.WriteToPng("hexagon.png");
            }

            using (Surface surface = new PdfSurface("hexagon.pdf", 500, 500))
            {
                draw(surface);
                surface.WriteToPng("hexagon1.png");
            }

            using (Surface surface = new SvgSurface("hexagon.svg", 500, 500))
            {
                draw(surface);
                surface.WriteToPng("hexagon2.png");
            }
        }
    }
    //-------------------------------------------------------------------------
    public static class ContextExtensions
    {
        public static void Arrow(
            this Context c,
            double x0, double y0,
            double x1, double y1,
            double length, double angle)
        {
            // Linie zeichnen:
            c.MoveTo(x0, y0);
            c.LineTo(x1, y1);
            c.Stroke();

            // Pfeil zeichnen:
            angle *= Math.PI / 180d;
            double phi = Math.Atan2(y1 - y0, x0 - x1);
            double xx1 = x1 + length * Math.Cos(phi - angle);
            double yy1 = y1 + length * Math.Sin(phi - angle);
            double xx2 = x1 + length * Math.Cos(phi + angle);
            double yy2 = y1 + length * Math.Sin(phi + angle);

            c.MoveTo(x1, y1);
            c.LineTo(xx1, yy1);
            c.LineTo(xx2, yy2);
            c.ClosePath();
            c.Fill();
        }
    }
}