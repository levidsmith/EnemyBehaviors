//2022 - Levi D. Smith
using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace EnemyBehaviors {
    public class Block {
        public int x, y, w, h;
        Texture2D spr;
        public Block(Texture2D in_spr,  int in_x, int  in_y) {
            x = in_x;
            y = in_y;
            w = 64;
            h = 64;
            spr = in_spr;

        }

        public void Draw(SpriteBatch in_spritebatch) {
            in_spritebatch.Draw(spr, new Rectangle(x, y, w, h), Color.White);

        }
    }
}
