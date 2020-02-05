using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MonoGameWindowsStarter
{
    public class PaddleAI
    {
        Game1 game;
        

        Texture2D texture;

        BoundingRectangle bounds;

        /// <summary>
        /// Creates a paddle
        /// </summary>
        /// <param name="game">Reference to the game the paddle belongs to</param>
        public PaddleAI(Game1 game)
        {
            this.game = game;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("pixel");
            bounds.Width = 50;
            bounds.Height = 200;
            bounds.X = 1550;
            bounds.Y = game.GraphicsDevice.Viewport.Height / 2 - bounds.Height / 2;
        }

        public void Update(GameTime gameTime)
        {
            var newKeyboardState = Keyboard.GetState();

            if (game.GameState == 0)  //if the game is still going, keeps moving. Stops moving if game is over
            {
                if (game.ballPosition.Y < bounds.Y)           //if the balls Y position is less than the paddles Y, then move paddle up
                {
                    bounds.Y -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * (float)0.75;
                }

                if (game.ballPosition.Y > bounds.Y)           //if the balls Y position is greater than the paddles Y, then move paddle down
                {
                    bounds.Y += (float)gameTime.ElapsedGameTime.TotalMilliseconds * (float)0.5;
                }
            }

            if (bounds.Y < 0)
            {
                bounds.Y = 0;
            }

            if (bounds.Y > game.GraphicsDevice.Viewport.Height - bounds.Height)
            {
                bounds.Y = game.GraphicsDevice.Viewport.Height - bounds.Height;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.DarkRed);
        }
    }
}

