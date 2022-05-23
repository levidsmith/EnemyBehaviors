//2022 - Levi D. Smith
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EnemyBehaviors {
    public class EnemyBackAndForthCollision : Enemy {

        public EnemyBackAndForthCollision(Texture2D in_spr, GameManager in_gamemanager) : base (in_spr, in_gamemanager) {

        }

        public override void Update(float deltaTime) {
            bool hasCollided = false;

            x += vel_x * deltaTime;

            foreach (Block block in gamemanager.blocks) {
                if (gamemanager.checkCollision(this, block)) {
                    hasCollided = true;
                }
            }

            if (hasCollided) {
                //keep from getting stuck inside block
                x -= vel_x * deltaTime; 
                //reverse velocity
                vel_x *= -1f;
            }

        }


        public override void Draw(SpriteBatch in_spritebatch) {
            in_spritebatch.Draw(spr, new Rectangle((int)x, (int)y, w, h), Color.White);

            in_spritebatch.DrawString(gamemanager.myfontsmall,
                string.Format("vel_x: {0}\n",
                (vel_x / GameManager.UNIT_SIZE)
                ),
                new Vector2(x + w, y),
                Color.White);


        }
    }
}
