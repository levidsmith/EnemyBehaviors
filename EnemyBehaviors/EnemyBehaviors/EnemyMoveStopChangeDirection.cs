//2022 Levi D. Smith
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EnemyBehaviors {
    class EnemyMoveStopChangeDirection : Enemy {

        public float fMoveCountdown;
        public float fMaxMoveCountdown;
        public float fStopCountdown;
        public float fMaxStopCountdown;

        Random r;

        public EnemyMoveStopChangeDirection(Texture2D in_spr, GameManager in_gamemanager) : base(in_spr, in_gamemanager) {
            gamemanager = in_gamemanager;
            r = new Random();
            changeDirection();
        }

        public void setCountdown(float in_fMoveCountdown, float in_fStopCountdown) {
            fMaxMoveCountdown = in_fMoveCountdown;
            fMoveCountdown = fMaxMoveCountdown;
            fMaxStopCountdown = in_fStopCountdown;
            fStopCountdown = 0f;
        }



        public override void Update(float deltaTime) {
            if (fMoveCountdown > 0f) {
                x += vel_x * deltaTime;
                y += vel_y * deltaTime;
                fMoveCountdown -= deltaTime;

                if (fMoveCountdown <= 0f) {
                    fStopCountdown = fMaxStopCountdown;
                }
            } else if (fStopCountdown > 0f) {
                fStopCountdown -= deltaTime;
                if (fStopCountdown <= 0f) {
                    fMoveCountdown = fMaxMoveCountdown;
                    changeDirection();
                }

            }


            //keep enemies from leaving the screen
            //int screen_w = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            //int screen_h = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            int screen_w = gamemanager.Window.ClientBounds.Width;
            int screen_h = gamemanager.Window.ClientBounds.Height;

            if (x < 0f) {
                x = 0f;
                changeDirection();
            } else if (x + w > screen_w) {
                x = screen_w - w;
                changeDirection();
            }

            if (y < 0f) {
                y = 0f;
                changeDirection();
            } else if (y + h > screen_h) {
                y = screen_h - h;
                changeDirection();
            }
        }

        private void changeDirection() {
            float fRandAngle = r.Next(0, 4) * MathF.PI / 2f;
            float fSpeed = 1f * GameManager.UNIT_SIZE;

            vel_x = fSpeed * MathF.Cos(fRandAngle);
            vel_y = fSpeed * MathF.Sin(fRandAngle);

        }


        public override void Draw(SpriteBatch in_spritebatch) {
            in_spritebatch.Draw(spr, new Rectangle((int)x, (int)y, w, h), Color.White);

            in_spritebatch.DrawString(gamemanager.myfontsmall,
                string.Format("move countdown: {0:0.00}\nmax move countdown: {1:0.##}\nstop countdown: {2:0.00}\nmax stop countdown: {3:0.##}",
                fMoveCountdown, fMaxMoveCountdown,
                fStopCountdown, fMaxStopCountdown),
                new Vector2(x + w, y),
                Color.White);


        }
    }
}
