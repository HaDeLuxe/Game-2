using Game_2.Network;
using Game_2.Snake;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_2
{
    class GameManager
    {
        #region fields
        

        private readonly List<Food> _foodList;

        private Texture2D _snake_Head_Pl1_Texture = null;

        private Texture2D _snake_Head_Pl2_Texture = null;

        private Texture2D _snake_Body_Pl1_Texture = null;

        private Texture2D _snake_Body_Pl2_Texture = null;

        private Texture2D _food_Texture = null;

        private Client _client = null;

        private bool _headAdded = false;

        #endregion


        #region properties

        public List<PlayerComponent> MainPlayer { get; set; }

        public List<PlayerComponent> EnemyPlayer { get; set; }

        #endregion


        #region methods

        public GameManager(Client pClient)
        {
            MainPlayer = new List<PlayerComponent>();
            EnemyPlayer = new List<PlayerComponent>();
            _foodList = new List<Food>();
            _client = pClient;

            MainPlayer.Add(new Head(null, new Vector2(0, 0), 0));
        }

        public void initGame()
        {
            if (_client.PlayerNumber == 1 && !_headAdded)
            {
                MainPlayer.RemoveAt(0);
                MainPlayer.Add(new Head(_snake_Head_Pl1_Texture, new Vector2(0, 0), 0));
                EnemyPlayer.Add(new Head(_snake_Head_Pl2_Texture, new Vector2(0, 0), 0));
                _headAdded = true;
            }
            else if (_client.PlayerNumber == 2 && !_headAdded)
            {
                MainPlayer.RemoveAt(0);
                MainPlayer.Add(new Head(_snake_Head_Pl2_Texture, new Vector2(0, 0), 0));
                EnemyPlayer.Add(new Head(_snake_Head_Pl1_Texture, new Vector2(0, 0), 0));

                _headAdded = true;
            }
        }

        public void LoadContent(ContentManager Content)
        {

            _snake_Head_Pl1_Texture = Content.Load<Texture2D>("snakeRobot_head_purple");
            _snake_Head_Pl2_Texture = Content.Load<Texture2D>("snakeRobot_head_red");
            _snake_Body_Pl1_Texture = Content.Load<Texture2D>("snakeRobot_link_purple");
            _snake_Body_Pl2_Texture = Content.Load<Texture2D>("snakeRobot_link_red");

            _food_Texture = Content.Load<Texture2D>("Food");
            

        }

        public void Update(GameTime gameTime)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.D))
                _client.SendMainGameMsg(Client.SendMessageType.INPUT_RIGHT);
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                _client.SendMainGameMsg(Client.SendMessageType.INPUT_LEFT);

            _client.CheckForMessagesGameManager(MainPlayer, EnemyPlayer);

            
        }
        

        //public void _updatePlayer(List<PlayerComponent> pPlayerList, GameTime gameTime, short pPlayer)
        //{
        //    for (int i = 1; i <= pPlayerList.Count - 1; i++)
        //    {
        //        if (pPlayerList[i - 1].PreviousPosition != null || pPlayerList[i - 1].PreviousPosition == pPlayerList[i - 1].CurrentPosition)
        //        {
        //            pPlayerList[i].NewPosition = pPlayerList[i - 1].PreviousPosition;
        //            pPlayerList[i].Rotation = pPlayerList[i - 1].PreviousRotation;
        //        }
        //    }

        //    foreach (PlayerComponent playerComponent in pPlayerList)
        //    {
        //        playerComponent.Update(gameTime);
        //    }

        //    if (_foodList.Count > 0 && pPlayerList[0].CheckCollision(_foodList[0].Rectangle))
        //    {
        //        _foodList.RemoveAt(0);
        //        for (int i = 0; i < 1; i++)
        //        {
        //            if (pPlayer == 1)
        //                pPlayerList.Add(new Body(_snake_Body_Pl1_Texture, pPlayerList[(pPlayerList.Count - 1)].PreviousPosition, (float)Math.PI));
        //            else if (pPlayer == 2)
        //                pPlayerList.Add(new Body(_snake_Head_Pl2_Texture, pPlayerList[(pPlayerList.Count - 1)].PreviousPosition, (float)Math.PI));
        //        }
        //    }
        //}

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_client.PlayerNumber != 0)
            {
                foreach (PlayerComponent playerComponent in MainPlayer)
                {
                    playerComponent.Draw(gameTime, spriteBatch);
                }
                foreach(PlayerComponent enemyComponent in EnemyPlayer)
                {
                    enemyComponent.Draw(gameTime, spriteBatch);
                }


                //foreach (Food food in _foodList)
                //{
                //    food.Draw(gameTime, spriteBatch);
                //}
            }
            
        }

        
        #endregion

    }
}
