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
    }
}
