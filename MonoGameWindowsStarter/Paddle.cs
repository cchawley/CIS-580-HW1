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

    enum PaddleState
    {
        up = 1,
        down = 0,
        Idle = 2
    }

    public class Paddle
    {

        /// <summary>
        /// How quickly the animation should advance frames (1/8 second as milliseconds)
        /// </summary>
        const int ANIMATION_FRAME_RATE = 124;

        /// <summary>
        /// How quickly the player should move
        /// </summary>
        const float PLAYER_SPEED = 1;

        /// <summary>
        /// The width of the animation frames
        /// </summary>
        const int FRAME_WIDTH = 15;

        /// <summary>
        /// The hieght of the animation frames
        /// </summary>
        const int FRAME_HEIGHT = 149;

        Game1 game;

        Texture2D texture;
        PaddleState pstate;
        TimeSpan timer;
        int frame;
        public BoundingRectangle bounds;

        /// <summary>
        /// Creates a paddle
        /// </summary>
        /// <param name="game">Reference to the game the paddle belongs to</param>
        public Paddle(Game1 game)
        {
            this.game = game;
            pstate = PaddleState.Idle;
            timer = new TimeSpan(0);
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
            //texture = content.Load<Texture2D>("pixel");
            texture = game.Content.Load<Texture2D>("left_paddle5");
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
                    pstate = PaddleState.up;
                    bounds.Y -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * PLAYER_SPEED;
                }
                else if (newKeyboardState.IsKeyDown(Keys.Down))
                {
                    pstate = PaddleState.down;
                    bounds.Y += (float)gameTime.ElapsedGameTime.TotalMilliseconds * PLAYER_SPEED;
                }
                else pstate = PaddleState.Idle;
            }

            if (bounds.Y < 0)
            {
                bounds.Y = 0;
            }

            if (bounds.Y > game.GraphicsDevice.Viewport.Height - bounds.Height)
            {
                bounds.Y = game.GraphicsDevice.Viewport.Height - bounds.Height;
            }

            // Update the player animation timer when the player is moving
            if (pstate != PaddleState.Idle) timer += gameTime.ElapsedGameTime;

            // Determine the frame should increase.  Using a while 
            // loop will accomodate the possiblity the animation should 
            // advance more than one frame.
            while (timer.TotalMilliseconds > ANIMATION_FRAME_RATE)
            {
                // increase by one frame
                frame++;
                // reduce the timer by one frame duration
                timer -= new TimeSpan(0, 0, 0, 0, ANIMATION_FRAME_RATE);
            }

            // Keep the frame within bounds (there are four frames)
            frame %= 4;
        }

        /// <summary>
        /// draws the paddle into the game
        /// </summary>
        /// <param name="spriteBatch">the spritebatch to use</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // determine the source rectagle of the sprite's current frame
            var source = new Rectangle(
                frame * FRAME_WIDTH, // X value 
                (int)pstate % 4 * FRAME_HEIGHT, // Y value
                FRAME_WIDTH, // Width 
                FRAME_HEIGHT // Height
                );

            var source2 = new Rectangle(
                0, // X value 
                0, // Y value
                FRAME_WIDTH, // Width 
                FRAME_HEIGHT // Height
                );

            // render the sprite
            spriteBatch.Draw(texture, bounds, source, Color.White);

            if (pstate == PaddleState.Idle)
            {
                spriteBatch.Draw(texture, bounds, source2, Color.White);
            }
        }
    }
}

