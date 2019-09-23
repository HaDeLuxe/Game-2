using Game_2.Snake;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace Game_2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private List<PlayerComponent> _player1List;

        private List<Food> _foodList;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1500;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 1000;   // set this value to the desired height of your window
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

            _player1List = new List<PlayerComponent>();
            _foodList = new List<Food>();

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

            _player1List.Add(new Head(Content.Load<Texture2D>("Snake_Head"), new Vector2(50,50),Enums.directions.Right));
            _foodList.Add(new Food(Content.Load<Texture2D>("Food"), new Vector2(200, 200)));


            // TODO: use this.Content to load your game content here
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            if (Keyboard.GetState().IsKeyDown(Keys.W)) _player1List[0].changeDirection(Enums.directions.Up);
            if (Keyboard.GetState().IsKeyDown(Keys.D)) _player1List[0].changeDirection(Enums.directions.Right);
            if (Keyboard.GetState().IsKeyDown(Keys.S)) _player1List[0].changeDirection(Enums.directions.Down);
            if (Keyboard.GetState().IsKeyDown(Keys.A)) _player1List[0].changeDirection(Enums.directions.Left);

            if(Keyboard.GetState().IsKeyDown(Keys.F)) _player1List.Add(new Body(Content.Load<Texture2D>("Snake_Body"), new Vector2(_player1List.Last().previousPosition.X, _player1List.Last().previousPosition.Y)));

            for (int i = 1; i <= _player1List.Count-1; i++)
            {
                if (_player1List[i - 1].previousPosition != null || _player1List[i - 1].previousPosition == _player1List[i - 1].CurrentPosition)
                    _player1List[i].NewPosition = _player1List[i - 1].previousPosition;
            }

            foreach (PlayerComponent playerComponent in _player1List)
            {
                playerComponent.Update(gameTime);
            }

            if(_foodList.Count > 0 && _player1List[0].CheckFood(_foodList[0]))
            {
                _foodList.RemoveAt(0);
                for (int i = 0; i < 30; i++)
                    _player1List.Add(new Body(Content.Load<Texture2D>("Snake_Body"), _player1List[(_player1List.Count - 1)].previousPosition));

            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            _player1List.Reverse();
            foreach (PlayerComponent playerComponent in _player1List)
            {
                playerComponent.Draw(gameTime, spriteBatch);
            }
            _player1List.Reverse();



            foreach (Food food in _foodList)
            {
                food.Draw(gameTime, spriteBatch);
            }
      

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
