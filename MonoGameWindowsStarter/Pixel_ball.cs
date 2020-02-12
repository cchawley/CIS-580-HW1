using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

//couldn't figure out how to get the ball to correctly animate when hitting walls and paddle, so scratched the idea for the time being. May come back to in the future
namespace MonoGameWindowsStarter
{

    
    /// <summary>
    /// enum for telling what state the ball is in
    /// </summary>
    public enum State
    {
        right = 0,
        up = 1,
        down = 2,
        left = 3,
        Idle = 4
        
    }

    public class Pixel_ball
    {
        
        /// <summary>
        /// animation frame rate
        /// </summary>
        const int Animation_Frame_Rate = 90;

        const float Ball_Speed = 100;

        const int Frame_Width = 29;

        const int Frame_Height = 31;

        Game1 game;

        Texture2D texture;
        public State state;
        TimeSpan timer;
        int frame;
        public BoundingCircle Bounds;
        public Vector2 Velocity;
        SoundEffect explosion;
        SoundEffect norm_bounce;


        /// <summary>
        /// creates a new ball
        /// </summary>
        /// <param name="game">the game which this ball belongs to</param>
        public Pixel_ball(Game1 game)
        {
            this.game = game;
            timer = new TimeSpan(0);
            state = State.Idle;
        }

        public void Initialize()
        {
            // Set the ball's radius for some reason, Game calculations start getting janky when I increase past 25, not sure why
            Bounds.Radius = 25;

            // position the ball in the center of the screen
            Bounds.X = game.GraphicsDevice.Viewport.Width / 2;
            Bounds.Y = game.GraphicsDevice.Viewport.Height / 2;

            // give the ball a random velocity

            Velocity = new Vector2(
                (float)game.Random.NextDouble() * (float)0.005,
                (float)game.Random.NextDouble() * (float)0.005
            );


            Velocity.Normalize();
        }

        /// <summary>
        /// Loads the sprite's content
        /// </summary>
        public void LoadContent(ContentManager content)
        {
            texture = game.Content.Load<Texture2D>("Ball");
            explosion = content.Load<SoundEffect>("Explosion");  //load in explosion sound
            norm_bounce = content.Load<SoundEffect>("Norm_Bounce"); //load in normal bounce sound
        }

        public void Update(GameTime gameTime)
        {
            var viewport = game.GraphicsDevice.Viewport;

            Bounds.Center += 1.1f * (float)gameTime.ElapsedGameTime.TotalMilliseconds * Velocity;
            float charlie = (float)gameTime.ElapsedGameTime.TotalSeconds;


            if (game.BounceCounter == 9 || game.BounceCounter == 15)  //ball will speed up as more paddle bounces occur
            {
                Velocity = Velocity * (float)1.005;
            }
            if (game.BounceCounter == 26)
            {
                Velocity = Velocity * (float)2;

            }



            if (game.GameState == 1 || game.GameState == 2)
            {
                Velocity.X = 0;
                Velocity.Y = 0;
            }

            // check for wall collision
            if (Bounds.Center.Y < Bounds.Radius)
            {
                state = State.down; //can add in a state for top and bottom bounce animations
                Velocity.Y *= -1;
                float delta = Bounds.Radius - Bounds.Y;  
                Bounds.Y += 2 * delta * charlie;
                norm_bounce.Play();
            }
            else if (Bounds.Center.Y > viewport.Height - Bounds.Radius)
            {
                state = State.up;
                Velocity.Y *= -1;
                float delta = viewport.Height - Bounds.Radius - Bounds.Y;
                Bounds.Y += 2 * delta * charlie;
                norm_bounce.Play();
            }
            else if (Bounds.X < 0)
            {
                Velocity.X *= -1;
                float delta = Bounds.Radius - Bounds.X;  // can add in an explosion animation
                Bounds.X += 2 * delta * charlie;
                //Velocity = Vector2.Zero;
                game.GameState = 2;          //the ball hit the left side of the boundary, meaning the player losses;
                explosion.Play();
            }
            else if (Bounds.X > viewport.Width - Bounds.Radius)
            {
                Velocity.X *= -1;
                float delta = viewport.Width - Bounds.Radius - Bounds.X;
                Bounds.X += 2 * delta * charlie;
                game.GameState = 1;         //the ball hit the right side of the boundary, meaning the player wins
                explosion.Play();
            }
            else state = State.Idle;

            // Update the player animation timer when the ball bounces
            if (state != State.Idle) timer += gameTime.ElapsedGameTime;

            // Determine the frame should increase.  Using a while 
            // loop will accomodate the possiblity the animation should 
            // advance more than one frame.
            while (timer.TotalMilliseconds > Animation_Frame_Rate)
            {
                // increase by one frame
                frame++;
                // reduce the timer by one frame duration
                timer -= new TimeSpan(0, 0, 0, 0, Animation_Frame_Rate);
            }

            // Keep the frame within bounds (there are four frames)
            frame %= 4;
        }

        /// <summary>
        /// Renders the sprite on-screen
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // determine the source rectagle of the sprite's current frame
            var source = new Rectangle(
                frame * Frame_Width, // X value 
                (int)state % 4 * Frame_Height, // Y value
                Frame_Width, // Width 
                Frame_Height// Height
                );

            // render the sprite
            spriteBatch.Draw(texture, Bounds, source, Color.White);

            
        }
    }
   
}
