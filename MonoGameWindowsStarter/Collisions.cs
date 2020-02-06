using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGameWindowsStarter
{
    public static class Collisions
    {
        /// <summary>
        /// Will detect if rectangle a collides with another rectangle
        /// </summary>
        /// <param name="a">this boundingrectangle</param>
        /// <param name="b">other boundingrectangle</param>
        /// <returns>true if collision, flase otherwise</returns>
        public static bool CollidesWith(this BoundingRectangle a, BoundingRectangle b)
        {
            return !(a.X > a.X + b.Width
                  || a.X + a.Width < b.X
                  || a.Y > b.Y + b.Height
                  || a.Y + a.Height < b.Y);
        }

        /// <summary>
        /// Will detect if circle a collides with another cirlce
        /// </summary>
        /// <param name="a">this boundingcircle</param>
        /// <param name="b">other boundingcircle</param>
        /// <returns>true if collision, flase otherwise</returns>
        public static bool CollidesWith(this BoundingCircle a, BoundingCircle b)
        {
            return Math.Pow(a.Radius + b.Radius, 2) >= Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2);
        }

        /// <summary>
        /// Will detect if circle c collides with a rectangle
        /// </summary>
        /// <param name="a">this boundingcircle</param>
        /// <param name="b">other boundingrectangle</param>
        /// <returns>true if collision, flase otherwise</returns>
        public static bool CollidesWith(this BoundingCircle c, BoundingRectangle r)
        {
            var closestX = Math.Max(Math.Min(c.X, r.X + r.Width), r.X);
            var closestY = Math.Max(Math.Min(c.Y, r.Y + r.Height), r.Y);
            return (Math.Pow(c.Radius, 2) >= Math.Pow(closestX - c.X, 2) + Math.Pow(closestY - c.Y, 2));
        }

        /// <summary>
        /// Will detect if rectangle a collides with a circle
        /// </summary>
        /// <param name="a">this boundingrectangle</param>
        /// <param name="b">other boundingCircle</param>
        /// <returns>true if collision, flase otherwise</returns>
        public static bool CollidesWith(this BoundingRectangle r, BoundingCircle c)
        {
            return c.CollidesWith(r);
        }

        
        

        

       
    }
}
