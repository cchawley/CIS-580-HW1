using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MonoGameWindowsStarter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D YouWin;     //variable for the you win text
        Rectangle win;
        Texture2D YouLose;     //variable for the you lose text
        Rectangle lose;
        public Vector2 ballPosition = Vector2.Zero; 
        Paddle paddle;  //player paddle
        PaddleAI AIpaddle; //enemy paddle
        public int GameState = 0;   //used to track if the player has won or lost. Will be changed to a 1 when the ball hits the right boundary (win) and a 2 if it hits the left boundary (loss)
        public Random Random = new Random();
        Ball ball;  //used to create a ball using the Ball class
        public int BounceCounter = 0;

        KeyboardState oldKeyboardState;
        KeyboardState newKeyboardState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            paddle = new Paddle(this);
            AIpaddle = new PaddleAI(this);
            ball = new Ball(this);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 1000;
            graphics.ApplyChanges();

            ball.Initialize();
            paddle.Initialize();
            AIpaddle.Initialize();

            win.X = 400;        //sizing and screen positioning for the win text
            win.Y = 250;
            win.Width = 800;
            win.Height = 500;

            lose.X = 400;          //sizing and screen positioning for the lose text
            lose.Y = 250;
            lose.Width = 800;
            lose.Height = 500;


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ball.LoadContent(Content);    // load in green ball
            paddle.LoadContent(Content);                   //load in the pixel paddle
            AIpaddle.LoadContent(Content);                //load in the enemy pixel paddle
            YouWin = Content.Load<Texture2D>("You_win");     //load in the you win texture
            YouLose = Content.Load<Texture2D>("game_over");  //load in the you lose texture
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            newKeyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            ball.Update(gameTime);
            paddle.Update(gameTime);
            AIpaddle.Update(gameTime);

            if (paddle.bounds.CollidesWith(ball.Bounds)) //if the player paddle collides with the ball, the ball bounces off
            {
                ball.Velocity.X *= -1;
                var delta = (paddle.bounds.X + paddle.bounds.Width) - (ball.Bounds.X - ball.Bounds.Radius);
                ball.Bounds.X += 2 * delta;
                BounceCounter++;
            }

            if (AIpaddle.bounds.CollidesWith(ball.Bounds))
            {
                ball.Velocity.X *= -1;
                var delta = (AIpaddle.bounds.X - AIpaddle.bounds.Width) - (ball.Bounds.X - ball.Bounds.Radius);
                ball.Bounds.X += 2 * delta;
                BounceCounter++;
            }

            if (GameState == 0)  //if the game is still going, keeps moving. Stops moving if game is over
            {
                if (ball.Bounds.Y < AIpaddle.bounds.Y)           //if the balls Y position is less than the paddles Y, then move paddle up
                {
                    AIpaddle.bounds.Y -= (float)gameTime.ElapsedGameTime.TotalMilliseconds * (float)1;
                    
                }

                if (ball.Bounds.Y > AIpaddle.bounds.Y)           //if the balls Y position is greater than the paddles Y, then move paddle down
                {
                    AIpaddle.bounds.Y += (float)gameTime.ElapsedGameTime.TotalMilliseconds * (float)1;
                    
                }
            }




            oldKeyboardState = newKeyboardState;   

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.MintCream);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            ball.Draw(spriteBatch);   //draw green ball 
            paddle.Draw(spriteBatch);
            AIpaddle.Draw(spriteBatch);
            if(GameState == 1)  //if you have won, draw the you win
            {
                spriteBatch.Draw(YouWin, win, Color.White);
            }

            if (GameState == 2) //if you have lost, draw the you lose
            {
                spriteBatch.Draw(YouLose, lose, Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
