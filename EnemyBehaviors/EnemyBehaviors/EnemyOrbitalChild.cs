//2022 - Levi D. Smith
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace EnemyBehaviors {
    class EnemyOrbitalChild : Enemy {

        public EnemyOrbitalChild(Texture2D in_spr, GameManager in_gamemanager) : base(in_spr, in_gamemanager) {
            w = 16;
            h = 16;
            gamemanager = in_gamemanager;
        }


        public override void Update(float deltaTime) {
        }

    }

}
