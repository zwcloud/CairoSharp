using Cairo;

namespace DrawToPngDemo
{
    class Program
    {
        static void Main()
        {
            // The using statement ensures that potentially heavy objects
            // are disposed immediately.
            using (ImageSurface draw = new ImageSurface(Format.Argb32, 70, 150))
            {
                using (Context gr = new Context(draw))
                {
                    gr.Antialias = Antialias.Subpixel;    // sets the anti-aliasing method
                    gr.LineWidth = 9;          // sets the line width
                    gr.SetSourceColor(new Color(0, 0, 0, 1));   // red, green, blue, alpha
                    gr.MoveTo(10, 10);          // sets the Context's start point.
                    gr.LineTo(40, 60);          // draws a "virtual" line from 5,5 to 20,30
                    gr.Stroke();          //stroke the line to the image surface

                    gr.Antialias = Antialias.Gray;
                    gr.LineWidth = 8;
                    gr.SetSourceColor(new Color(1, 0, 0, 1));
                    gr.LineCap = LineCap.Round;
                    gr.MoveTo(10, 50);
                    gr.LineTo(40, 100);
                    gr.Stroke();

                    gr.Antialias = Antialias.None;    //fastest method but low quality
                    gr.LineWidth = 7;
                    gr.MoveTo(10, 90);
                    gr.LineTo(40, 140);
                    gr.Stroke();

                    draw.WriteToPng("antialias.png");  //save the image as a png image.
                }
            }
        }
    }
}
