//2022 - Levi D. Smith
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EnemyBehaviors {

    class EnemyChaseEndDistance : Enemy {

        public float fAlertDistance = 2f;
        public bool isChasing = false;
        public float fChaseEndDistance = 3f;



        public EnemyChaseEndDistance(Texture2D in_spr, GameManager in_gamemanager) : base(in_spr, in_gamemanager) {
            gamemanager = in_gamemanager;
        }


        public override void Update(float deltaTime) {
            if (!isChasing) {
                if (getDistance() <= fAlertDistance) {
                    isChasing = true;
                }
            } else {

                //simple chase direction, should normalize instead
                if (gamemanager.players.Count > 0) {
                    Player player = gamemanager.players[0];
                    float fSpeed = 0.5f;
                    if (x < player.x) {
                        vel_x = fSpeed;
                    } else if (x > player.x) {
                        vel_x = -fSpeed;
                    }

                    if (y < player.y) {
                        vel_y = fSpeed;
                    } else if (y > player.y) {
                        vel_y = -fSpeed;
                    }
                }

                x += vel_x;
                y += vel_y;


                    if (getDistance() >= fChaseEndDistance) {
                    isChasing = false;
                }
            }
         

        }


        public override void Draw(SpriteBatch in_spritebatch) {
            in_spritebatch.Draw(spr, new Rectangle((int)x, (int)y, w, h), Color.White);

            in_spritebatch.DrawString(gamemanager.myfontsmall,
                string.Format("distance: {0:0.00}\nisChasing: {1}\nalert distance: {2}\nend chase distance: {3}",
                getDistance(), isChasing, fAlertDistance, fChaseEndDistance),
                new Vector2(x + w, y),
                Color.White);


        }

        private float getDistance() {
            float fDistance = 0f;
            if (gamemanager.players.Count > 0) {
                Player player = gamemanager.players[0];
                fDistance = (float) Math.Sqrt(
                    Math.Pow((player.x - x), 2) +
                    Math.Pow((player.y - y), 2)
                     );
                fDistance = fDistance / GameManager.UNIT_SIZE;
            }
            return fDistance;

        }

    }
}
