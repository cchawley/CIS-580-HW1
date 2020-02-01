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
    
    public struct BoundingRectangle
    {
        public float X;
        public float Y;
        public float Width;
        public float Height;

        public bool CollidesWith(BoundingRectangle other)  //gives the rectangle the property to check and see if it is interacting with any other object
        {
            return !(this.X < other.X + other.Width
                || this.X + this.Width < other.X
                || this.Y > other.Y + other.Height
                || this.Y + this.Height < other.Y);
        }
    }

    /*
    public BoundingRectangle(float x, float y, float width, float height)
    {
        this.X = x;
        this.Y = y;
        this.Width = width;
        this.Height = height;
    }
    */
}
