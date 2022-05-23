//2022 - Levi D. Smith
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EnemyBehaviors {
    public abstract class Enemy {
        public Texture2D spr;
        public float x;
        public float y;
        public int w;
        public int h;

        public float vel_x;
        public float vel_y;
        public GameManager gamemanager;

        public Enemy(Texture2D in_spr, GameManager in_gamemanager) {
            w = 64;
            h = 64;
            spr = in_spr;
            gamemanager = in_gamemanager;
        }

        public abstract void Update(float deltaTime);

        public virtual void Draw(SpriteBatch in_spritebatch) {
            in_spritebatch.Draw(spr, new Rectangle((int) x, (int) y, w, h), Color.White);

        }

        public void setPosition(int in_x, int in_y) {
            x = in_x;
            y = in_y;
        }
    }
}
