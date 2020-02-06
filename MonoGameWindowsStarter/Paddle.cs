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
    public class Paddle
    {
        Game1 game;

        Texture2D texture;

        public BoundingRectangle bounds;

        /// <summary>
        /// Creates a paddle
        /// </summary>
        /// <param name="game">Reference to the game the paddle belongs to</param>
        public Paddle(Game1 game)
        {
            this.game = game;
        }

        /// <summary>
        /// initializes paddles, sets it size and its screen positioning
        /// </summary>
        public void Initialize()
        {
            bounds.Width = 50;
            bounds.Height = 200;
            bounds.X = 0;
            bounds.Y = game.GraphicsDevice.Viewport.Height / 2 - bounds.Height / 2;
        }

        /// <summary>
        /// Load in the paddles texture
        /// </summary>
        /// <param name="content">content manager to use</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("pixel");
            
        }

        /// <summary>
        /// gives the paddle keyboard input movement
        /// </summary>
        /// <param name="gameTime">current gametime</param>
        public void Update(GameTime gameTime)
        {
            var newKeyboardState = Keyboard.GetState();

            if (game.GameState == 0)     //if the game is still going, movement is still enabled. Stops enabling movement if game is over
            {  
                if (newKeyboardState.IsKeyDown(Keys.Up))
                {
                    bounds.Y -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                }

                if (newKeyboardState.IsKeyDown(Keys.Down))
                {
                    bounds.Y += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
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

        /// <summary>
        /// draws the paddle into the game
        /// </summary>
        /// <param name="spriteBatch">the spritebatch to use</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.Blue);
        }
    }
}

