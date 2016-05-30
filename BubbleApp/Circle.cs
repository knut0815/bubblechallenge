using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Thanks to Matthias Shipiro for his work on Circle Packing
// http://matthiasshapiro.com/2015/02/09/circle-packing-algorithm-in-c-xaml/

namespace BubbleApp
{
    class Circle
    {
        public float x, y, radius;
        public Color circleColor;
        public float Width = 100.0f;
        public float Height = 100.0f;
        public string CircleName;

        public Circle(float x, float y, float radius)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
            this.circleColor = Color.FromArgb(255, 156, 156, 156);
        }

        public Circle(float x, float y, float radius, string name)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
            this.circleColor = Color.FromArgb(255, 156, 156, 156);
            this.CircleName = name;
        }

        public bool Contains(float x, float y)
        {
            float dx = this.x - x;
            float dy = this.y - y;
            return Math.Sqrt(dx * dx + dy * dy) <= radius;
        }

        public float DistanceToCenter
        {
            get
            {
                float dx = x - Width / 2;
                float dy = y - Height / 2;
                return (float)Math.Sqrt(dx * dx + dy * dy);
            }
        }

        public bool Intersects(Circle c)
        {
            float dx = c.x - x;
            float dy = c.y - y;
            float d = (float)Math.Sqrt(dx * dx + dy * dy);
            return d < radius || d < c.radius;
        }

        public void SetCanvasDimensions(double width, double height)
        {
            Width = (float)width;
            Height = (float)height;
        }
    }
}
