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

        private List<PlayerComponent> _player1List;

        private readonly List<Food> _foodList;

        private Texture2D _snake_Head_Pl1_Texture;

        private Texture2D _snake_Head_Pl2_Texture;

        private Texture2D _snake_Body_Pl1_Texture;

        private Texture2D _snake_Body_Pl2_Texture;

        private Texture2D _food_Texture;


        #endregion


        #region properties

        #endregion


        #region methods

        public GameManager()
        {
            _player1List = new List<PlayerComponent>();
            _foodList = new List<Food>();
        }

        public void LoadContent(ContentManager Content)
        {

            _snake_Head_Pl1_Texture = Content.Load<Texture2D>("snakeRobot_head_purple");
            _snake_Head_Pl2_Texture = Content.Load<Texture2D>("snakeRobot_head_red");
            _snake_Body_Pl1_Texture = Content.Load<Texture2D>("snakeRobot_link_purple");
            _snake_Body_Pl2_Texture = Content.Load<Texture2D>("snakeRobot_link_red");

            _food_Texture = Content.Load<Texture2D>("Food");

            _player1List.Add(new Head(_snake_Head_Pl1_Texture, new Vector2(50, 50), (float)Math.PI/2));

            _foodList.Add(new Food(_food_Texture, new Vector2(200, 200)));
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D)) _player1List[0].Rotation += (float)Math.PI / 48;
            if (Keyboard.GetState().IsKeyDown(Keys.A)) _player1List[0].Rotation -= (float)Math.PI / 48;

            _updatePlayer(_player1List, gameTime, 1);
            

           
        }


        public void _updatePlayer(List<PlayerComponent> pPlayerList, GameTime gameTime, short pPlayer)
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
                    if (pPlayer == 1)
                        pPlayerList.Add(new Body(_snake_Body_Pl1_Texture, pPlayerList[(pPlayerList.Count - 1)].PreviousPosition, (float)Math.PI));
                    else if (pPlayer == 2)
                        pPlayerList.Add(new Body(_snake_Head_Pl2_Texture, pPlayerList[(pPlayerList.Count - 1)].PreviousPosition, (float)Math.PI));
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
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
        }

        
        #endregion

    }
}
