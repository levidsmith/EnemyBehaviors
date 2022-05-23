//2022 - Levi D. Smith
using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EnemyBehaviors {
    class EnemyBackAndForthTimer : Enemy {
        public float fMaxCountdown;
        float fVelChangeCountdown;

        public EnemyBackAndForthTimer(Texture2D in_spr, GameManager in_gamemanager) : base(in_spr, in_gamemanager) {
        }

        public void setCountdown(float in_fCountdown) {
            fMaxCountdown = in_fCountdown;
            fVelChangeCountdown = fMaxCountdown;
        }



        public override void Update(float deltaTime) {
            fVelChangeCountdown -= deltaTime;
            if (fVelChangeCountdown <= 0f) {
                vel_x *= -1f;
                fVelChangeCountdown += fMaxCountdown;
            }

            x += vel_x * deltaTime;
        }


        public override void Draw(SpriteBatch in_spritebatch) {
            in_spritebatch.Draw(spr, new Rectangle((int)x, (int)y, w, h), Color.White);

            in_spritebatch.DrawString(gamemanager.myfontsmall,
                string.Format("vel_x: {0}\ncountdown: {1:0.00}\nmax_countdown: {2:0}",
                (vel_x / GameManager.UNIT_SIZE),
                fVelChangeCountdown, fMaxCountdown),
                new Vector2(x + w, y),
                Color.White);


        }
    }
}
