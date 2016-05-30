using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Thanks to Matthias Shipiro for his work on Circle Packing
// http://matthiasshapiro.com/2015/02/09/circle-packing-algorithm-in-c-xaml/

namespace BubbleApp
{
    class CirclePacker
    {

        private Random ranNumGen;
        public List<Circle> AllCircles { get; set; }
        long Iterations { get; set; }
        Panel HostCanvas { get; set; }

        public CirclePacker()
        {
        }

        public CirclePacker(Panel hostCanvas)
        {
            HostCanvas = hostCanvas;
            ranNumGen = new Random();
        }

        public void GenerateSampleCircles(int circleCount, int circleMin, int circleMax)
        {
            if (HostCanvas != null)
            {
                AllCircles = CreateRandomCircles(circleCount, circleMin, circleMax, (int)HostCanvas.Width, (int)HostCanvas.Height);
            }
        }

        List<Circle> CreateRandomCircles(int circleCount, int circleMin, int circleMax, int canvasWidth, int canvasHeight)
        {
            List<Circle> result = new List<Circle>();
            Random r = new Random();
            for (int i = 0; i < circleCount; i++)
            {
                Circle c = new Circle(r.Next(canvasWidth), r.Next(canvasHeight), r.Next(circleMin, circleMax));
                c.circleColor = Color.FromArgb(128, (byte)r.Next(255), 128, 200);
                c.SetCanvasDimensions(HostCanvas.Width, HostCanvas.Height);
                result.Add(c);
            }

            return result;
        }

        public void AddCircle(int radius, string name)
        {

            if (AllCircles == null)
            {
                AllCircles = new List<Circle>();
            }

            Circle c = new Circle(radius, radius, radius, name);
            c.circleColor = Color.FromArgb(ranNumGen.Next(256), ranNumGen.Next(256), ranNumGen.Next(256));
            c.SetCanvasDimensions(HostCanvas.Width, HostCanvas.Height);
            AllCircles.Add(c);
        }


        public void CreateTestCircles()
        {
            List<Circle> tempCircles = new List<Circle>();

            Circle c = new Circle(130, 130, 130);
            c.circleColor = Color.Red;
            c.SetCanvasDimensions(HostCanvas.Width, HostCanvas.Height);
            tempCircles.Add(c);

            c = new Circle(200, 200, 200);
            c.circleColor = Color.Blue;
            c.SetCanvasDimensions(HostCanvas.Width, HostCanvas.Height);
            tempCircles.Add(c);

            c = new Circle(310, 310, 310);
            c.circleColor = Color.Green;
            c.SetCanvasDimensions(HostCanvas.Width, HostCanvas.Height);
            tempCircles.Add(c);

            c = new Circle(40, 40, 40);
            c.circleColor = Color.Purple;
            c.SetCanvasDimensions(HostCanvas.Width, HostCanvas.Height);
            tempCircles.Add(c);

            AllCircles = tempCircles;
        }


        public void Iterate(int iterationCount)
        {
            var sortedCircles = from c in AllCircles
                                orderby c.DistanceToCenter descending
                                select c;

            var sCircles = sortedCircles.ToList();
            Vector2 v = new Vector2();

            // Cycle through circles for collision detection
            foreach (Circle c1 in sCircles)
            {
                foreach (Circle c2 in sCircles)
                {
                    if (c1 != c2)
                    {
                        float dx = c2.x - c1.x;
                        float dy = c2.y - c1.y;
                        float r = c1.radius + c2.radius;
                        float d = (dx * dx) + (dy * dy);
                        if (d < (r * r) - 0.01)
                        {
                            v = new Vector2(dx, dy);
                            v.Normalize();
                            v = v * (float)((r - Math.Sqrt(d)) * .5);

                            c2.x += v.X;
                            c2.y += v.Y;
                            c1.x -= v.X;
                            c1.y -= v.Y;
                        }
                    }
                }

            }

            // Contract all circles into the center 
            float dampening = 0.1f / (float)iterationCount;
            foreach (Circle c in sCircles)
            {
                v = new Vector2(c.x - (float)(HostCanvas.Width / 2), c.y - (float)(HostCanvas.Height / 2));
                v *= dampening;
                c.x -= v.X;
                c.y -= v.Y;
            }
        }

        public void Render(Graphics g)
        {
            g.Clear(Color.White);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            for (int i = 0; i < AllCircles.Count; i++)
            {
                Circle c = AllCircles[i];

                // Just in case there are some NaN values out there
                if (c.x.ToString() != "NaN" && c.y.ToString() != "NaN")
                {

                    Brush brush = new SolidBrush(c.circleColor);
                    float dim = (c.radius * 2);
                    g.FillEllipse(brush, c.x - c.radius, c.y - c.radius, dim, dim);

                    Font drawFont = new Font("Arial", 10);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    StringFormat drawFormat = new StringFormat();
                    g.DrawString(c.CircleName, drawFont, drawBrush, c.x, c.y, drawFormat);
                }
            }

        }
    }
}
