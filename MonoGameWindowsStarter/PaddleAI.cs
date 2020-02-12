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
    public enum AIPaddleState
    {
        up = 1,
        down = 0,
        Idle = 2
    }

    public class PaddleAI
    {
        Game1 game;
        //Ball ball;

        /// <summary>
        /// How quickly the animation should advance frames (1/8 second as milliseconds)
        /// </summary>
        const int ANIMATION_FRAME_RATE = 124;


        /// <summary>
        /// The width of the animation frames
        /// </summary>
        const int FRAME_WIDTH = 15;

        /// <summary>
        /// The hieght of the animation frames
        /// </summary>
        const int FRAME_HEIGHT = 149;

        Texture2D texture;

        public BoundingRectangle bounds;
        public AIPaddleState AIpstate;
        TimeSpan timer;
        int frame;

        /// <summary>
        /// Creates a paddle
        /// </summary>
        /// <param name="game">Reference to the game the paddle belongs to</param>
        public PaddleAI(Game1 game)
        {
            this.game = game;
            AIpstate = AIPaddleState.Idle;
            timer = new TimeSpan(0);
        }

        /// <summary>
        /// initializes paddles, sets it size and its screen positioning
        /// </summary>
        public void Initialize()
        {
            bounds.Width = 50;
            bounds.Height = 200;
            bounds.X = 1550;
            bounds.Y = game.GraphicsDevice.Viewport.Height / 2 - bounds.Height / 2;
        }

        /// <summary>
        /// Load in the paddles texture
        /// </summary>
        /// <param name="content">content manager to use</param>
        public void LoadContent(ContentManager content)
        {
            //texture = content.Load<Texture2D>("pixel");
            texture = game.Content.Load<Texture2D>("Right_paddle");
        }

        /// <summary>
        /// gives the paddle movement based off the balls Y position
        /// </summary>
        /// <param name="gameTime">current gametime</param>
        public void Update(GameTime gameTime)
        {
            var newKeyboardState = Keyboard.GetState();

            /*
            if (game.GameState == 0)  //if the game is still going, keeps moving. Stops moving if game is over
            {
                if (ball.Bounds.Center.Y < bounds.Y)           //if the balls Y position is less than the paddles Y, then move paddle up
                {
                    bounds.Y -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * (float)0.75;
                }

                if (ball.Bounds.Center.Y > bounds.Y)           //if the balls Y position is greater than the paddles Y, then move paddle down
                {
                    bounds.Y += (float)gameTime.ElapsedGameTime.TotalMilliseconds * (float)0.75;
                }
            }
            */ //was getting an error so moved it to Game1.cs and it worked, not sure why but it works

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
            spriteBatch.Draw(texture, bounds, Color.DarkRed);
        }
    }
}

