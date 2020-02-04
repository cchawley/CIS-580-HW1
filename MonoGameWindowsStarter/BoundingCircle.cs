using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameWindowsStarter
{
    public struct BoundingCircle                    //creates a circle which will be able to tell us when it is interacting with other objects
    {
        public float X;
        public float Y;
        public float Radius;
        public Vector2 Center
        {
            get => new Vector2(X, Y);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public bool CollidesWith(BoundingCircle other)
        {
            return (Math.Pow(this.Radius + other.Radius, 2) <= Math.Pow(this.X - other.X, 2) + Math.Pow(this.Y - other.Y, 2));
        }

        public bool CollidesWith(BoundingRectangle other)
        {
            float nearestX = Clamp(this.X, other.X, other.X + other.Width);
            float nearestY = Clamp(this.Y, other.Y, other.Y + other.Height);
            return (this.Radius >= Math.Pow(this.X - nearestX, 2) + Math.Pow(this.Y - nearestY, 2));
            //r >= (r.x-p.x)^2 + (r.y - p.y)^2
        }

        public float Clamp(float value, float min, float max)
        {
            if (value < min)
            {
                return min;
            }
            if (value > max)
            {
                return max;
            }
            return value;
        }
    }
}
