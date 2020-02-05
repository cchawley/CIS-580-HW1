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
        Texture2D GreenBall;
        Texture2D YouWin;     //variable for the you win text
        Rectangle win;
        Texture2D YouLose;     //variable for the you lose text
        Rectangle lose;
        public Vector2 ballPosition = Vector2.Zero; 
        Vector2 ballVelocity;
        Paddle paddle;  //player paddle
        PaddleAI AIpaddle; //enemy paddle
        public int GameState = 0;   //used to track if the player has won or lost. Will be changed to a 1 when the ball hits the right boundary (win) and a 2 if it hits the left boundary (loss)

        KeyboardState oldKeyboardState;
        KeyboardState newKeyboardState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            paddle = new Paddle(this);
            AIpaddle = new PaddleAI(this);
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

            Random random = new Random();
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 1000;
            graphics.ApplyChanges();

            ballVelocity = new Vector2(     //gives velocity to the ball
                (float)random.NextDouble(),
                (float)random.NextDouble()
            );
            ballVelocity.Normalize();

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
            GreenBall = Content.Load<Texture2D>("green ball");    // load in green ball
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

            paddle.Update(gameTime);
            AIpaddle.Update(gameTime);
            ballPosition += (float)gameTime.ElapsedGameTime.TotalMilliseconds * ballVelocity * (float)1.5;

            if (GameState == 1 || GameState == 2)
            {
                ballVelocity.X = 0;
                ballVelocity.Y = 0;
            }

            //wall Collision checks as follows for ball
            if (ballPosition.Y < 0)
            {
                ballVelocity.Y *= -1;
                float delta = 0 - ballPosition.Y;
                ballPosition.Y += 2 * delta;
            }

            if (ballPosition.Y > graphics.PreferredBackBufferHeight - 100)
            {
                ballVelocity.Y *= -1;
                float delta = graphics.PreferredBackBufferHeight - 100 - ballPosition.Y;
                ballPosition.Y += 2 * delta;
            }

            if (ballPosition.X < 0)
            {
                ballVelocity.X *= -1;
                float delta = 0 - ballPosition.X;
                ballPosition.X += 2 * delta;
                GameState = 2;          //the ball hit the left side of the boundary, meaning the player losses
            }

            if (ballPosition.X > graphics.PreferredBackBufferWidth - 100)
            {
                ballVelocity.X *= -1;
                float delta = graphics.PreferredBackBufferWidth - 100 - ballPosition.X;
                ballPosition.X += 2 * delta;
                GameState = 1;         //the ball hit the right side of the boundary, meaning the player wins
            }

            //if statements for checking gamestate
            if(GameState == 1)
            {
                //logic for printing YOU WIN followed by either closing or restarting the game
            }

            if(GameState == 2)
            {
                //logic for printing YOU LOSE followed by closing or restarting the game
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
            spriteBatch.Draw(GreenBall, new Rectangle((int)ballPosition.X, (int)ballPosition.Y, 100, 100), Color.White);   //draw green ball 
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
