﻿using Game_2.Snake;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game_2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        
        SpriteBatch spriteBatch;

        private List<PlayerComponent> _player1List;
        private List<PlayerComponent> _player2List;

        private List<Food> _foodList;

        public Game1()
        {
            GraphicsDeviceManager graphics;
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
            _player1List = new List<PlayerComponent>();
            _player2List = new List<PlayerComponent>();
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

            _player1List.Add(new Head(Content.Load<Texture2D>("snakeRobot_head_purple"), new Vector2(50, 50), 0));
            _player2List.Add(new Head(Content.Load<Texture2D>("snakeRobot_head_red"), new Vector2(1000, 150), 0));

            _foodList.Add(new Food(Content.Load<Texture2D>("Food"), new Vector2(200, 200)));
            _foodList.Add(new Food(Content.Load<Texture2D>("Food"), new Vector2(200, 200)));
            _foodList.Add(new Food(Content.Load<Texture2D>("Food"), new Vector2(200, 200)));
            _foodList.Add(new Food(Content.Load<Texture2D>("Food"), new Vector2(200, 200)));
            _foodList.Add(new Food(Content.Load<Texture2D>("Food"), new Vector2(200, 200)));

            


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

            #region player1
            
            if (Keyboard.GetState().IsKeyDown(Keys.D)) _player1List[0].Rotation += (float)Math.PI / 48;
            if (Keyboard.GetState().IsKeyDown(Keys.A)) _player1List[0].Rotation -= (float)Math.PI / 48;

            if(Keyboard.GetState().IsKeyDown(Keys.F)) _player1List.Add(new Body(Content.Load<Texture2D>("snakeRobot_link_purple"), _player1List[(_player1List.Count - 1)].PreviousPosition, (float)Math.PI));

            _updatePlayer(_player1List, gameTime, 1);

            #endregion

            #region player2

            if (Keyboard.GetState().IsKeyDown(Keys.Right)) _player2List[0].Rotation += (float)Math.PI / 48;
                if (Keyboard.GetState().IsKeyDown(Keys.Left)) _player2List[0].Rotation -= (float)Math.PI / 48;

            _updatePlayer(_player2List, gameTime, 2);

            #endregion


            base.Update(gameTime);
        }

        private void _updatePlayer(List<PlayerComponent> pPlayerList, GameTime gameTime, short pPlayer)
        {
            for (int i = 1; i <= pPlayerList.Count - 1; i++)
            {
                if (pPlayerList[i - 1].PreviousPosition != null || pPlayerList[i - 1].PreviousPosition == pPlayerList[i - 1].CurrentPosition)
                {
                    pPlayerList[i].NewPosition = pPlayerList[i - 1].PreviousPosition;
                    pPlayerList[i].Rotation = pPlayerList[i - 1].PreviousRotation;
                }
            }

            foreach (PlayerComponent playerComponent in pPlayerList)
            {
                playerComponent.Update(gameTime);
            }

            if (_foodList.Count > 0 && pPlayerList[0].CheckCollision(_foodList[0].Rectangle))
            {
                _foodList.RemoveAt(0);
                for (int i = 0; i < 1; i++)
                {
                    if(pPlayer == 1)
                        pPlayerList.Add(new Body(Content.Load<Texture2D>("snakeRobot_link_purple"), pPlayerList[(pPlayerList.Count - 1)].PreviousPosition,(float)Math.PI));
                    else if(pPlayer == 2)
                        pPlayerList.Add(new Body(Content.Load<Texture2D>("snakeRobot_link_red"), pPlayerList[(pPlayerList.Count - 1)].PreviousPosition, (float)Math.PI));
                }
            }
            
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            spriteBatch.Begin();

            _player1List.Reverse();
            foreach (PlayerComponent playerComponent in _player1List)
            {
                playerComponent.Draw(gameTime, spriteBatch);
            }
            _player1List.Reverse();

            _player2List.Reverse();
            foreach(PlayerComponent playerComponent in _player2List)
            {
                playerComponent.Draw(gameTime, spriteBatch);
            }
            _player2List.Reverse();



            foreach (Food food in _foodList)
            {
                food.Draw(gameTime, spriteBatch);
            }
      

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
