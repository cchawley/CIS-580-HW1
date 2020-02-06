using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MonoGameWindowsStarter
{
    public class Ball
    {
        Game1 game;
        Texture2D texture;
        public BoundingCircle Bounds;
        public Vector2 Velocity;

        /// <summary>
        /// creates a new ball
        /// </summary>
        /// <param name="game">the game which this ball belongs to</param>
        public Ball(Game1 game)
        {
            this.game = game;
        }

        /// <summary>
        /// initialize the ball and its size/velocity
        /// </summary>
        public void Initialize()
        {
            // Set the ball's radius
            Bounds.Radius = 25;

            // position the ball in the center of the screen
            Bounds.X = game.GraphicsDevice.Viewport.Width / 2;
            Bounds.Y = game.GraphicsDevice.Viewport.Height / 2;

            // give the ball a random velocity
            Velocity = new Vector2(
                (float)game.Random.NextDouble(),
                (float)game.Random.NextDouble()
            );
            Velocity.Normalize();
        }

        /// <summary>
        /// load in the image of the ball
        /// </summary>
        /// <param name="content">which contentmanager to use</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("green ball");
        }

        /// <summary>
        /// Will update the balls position and will detect wall collisions to create bounces
        /// </summary>
        /// <param name="gameTime">Current game time</param>
        public void Update(GameTime gameTime)
        {
            var viewport = game.GraphicsDevice.Viewport;

            Bounds.Center += 1.5f * (float)gameTime.ElapsedGameTime.TotalMilliseconds * Velocity;

            if (game.BounceCounter == 10 || game.BounceCounter == 15 || game.BounceCounter == 25)  //ball will speed up as more paddle bounces occur
            {
                Velocity = Velocity * (float)1.005;
            }
            if (game.BounceCounter == 30)
            {
                Velocity = Velocity * (float)10;

            }
            


            if (game.GameState == 1 || game.GameState == 2)
            {
                Velocity.X = 0;
                Velocity.Y = 0;
            }

            // Check for wall collisions
            if (Bounds.Center.Y < Bounds.Radius)
            {
                Velocity.Y *= -1;
                float delta = Bounds.Radius - Bounds.Y;
                Bounds.Y += 2 * delta;
            }

            if (Bounds.Center.Y > viewport.Height - Bounds.Radius)
            {
                Velocity.Y *= -1;
                float delta = viewport.Height - Bounds.Radius - Bounds.Y;
                Bounds.Y += 2 * delta;
            }

            if (Bounds.X < 0)
            {
                Velocity.X *= -1;
                float delta = Bounds.Radius - Bounds.X;
                Bounds.X += 2 * delta;
                //Velocity = Vector2.Zero;
                game.GameState = 2;          //the ball hit the left side of the boundary, meaning the player losses
            }

            if (Bounds.X > viewport.Width - Bounds.Radius)
            {
                Velocity.X *= -1;
                float delta = viewport.Width - Bounds.Radius - Bounds.X;
                Bounds.X += 2 * delta;
                game.GameState = 1;         //the ball hit the right side of the boundary, meaning the player wins
            }


        }

        /// <summary>
        /// Draws the ball onto the screen
        /// Will need to be used in Game1.cs
        /// </summary>
        /// <param name="spriteBatch">the spritebatch to use</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Bounds, Color.White);
        }
    }
}
