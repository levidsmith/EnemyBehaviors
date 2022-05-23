//2022 - Levi D. Smith
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EnemyBehaviors {
    public class Player {
        public float x, y;
        public float vel_x, vel_y;
        public int w, h;
        public Texture2D spr;
        GameManager gamemanager;

        public Player(Texture2D in_spr, GameManager in_gamemanager) {
            w = 64;
            h = 64;
            spr = in_spr;
            gamemanager = in_gamemanager;
        }

        public void Update(float deltaTime) {
            x += vel_x * deltaTime;
            y += vel_y * deltaTime;

        }


        public void Draw(SpriteBatch in_spritebatch) {
            in_spritebatch.Draw(spr, new Rectangle((int)x, (int)y, w, h), Color.White);

        }

        public void setPosition(int in_x, int in_y) {
            x = in_x;
            y = in_y;
        }

    }
}
