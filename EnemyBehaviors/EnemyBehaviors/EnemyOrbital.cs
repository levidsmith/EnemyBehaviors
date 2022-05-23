//2022 - Levi D. Smith
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EnemyBehaviors {
    class EnemyOrbital : Enemy {
        float fLifetime;
        float fOrbitRadius;  
        float fOrbitSpeed; //1f = 1 orbit every second
        List<Enemy> children;

        public EnemyOrbital(Texture2D in_spr, GameManager in_gamemanager) : base(in_spr, in_gamemanager) {
            gamemanager = in_gamemanager;
            children = new List<Enemy>();
            fLifetime = 0f;
        }

        public override void Update(float deltaTime) {
            fLifetime += deltaTime * MathF.PI * 2f;
            //fOrbitRadius = 2f * GameManager.UNIT_SIZE;
            int i;
            for (i = 0; i < children.Count; i++) {
                float fAngleOffset = ((float)i / (float)children.Count) * MathF.PI * 2f;
                int parent_center_x = (int) (x + (w / 2f));
                int parent_center_y = (int) (y + (h / 2f));
                int child_x = (int) (parent_center_x + fOrbitRadius * MathF.Cos((fLifetime * fOrbitSpeed) + fAngleOffset));
                int child_y = (int) (parent_center_y + fOrbitRadius * MathF.Sin((fLifetime * fOrbitSpeed) + fAngleOffset));

                //subtract off half of child's width and height to center
                child_x -= (children[i].w / 2);
                child_y -= (children[i].h / 2);
                children[i].setPosition(child_x, child_y);
            }
        }


        public override void Draw(SpriteBatch in_spritebatch) {
            in_spritebatch.Draw(spr, new Rectangle((int)x, (int)y, w, h), Color.White);

            in_spritebatch.DrawString(gamemanager.myfontsmall,
                string.Format("children: {0}\norbit radius: {1}\norbit speed: {2}",
                children.Count, fOrbitRadius / GameManager.UNIT_SIZE, fOrbitSpeed),
                new Vector2(x + w, y),
                Color.White);


        }


        public void setChildren(int in_iNumber, float in_fOrbitRadius, float in_fOrbitSpeed) {
            fOrbitRadius = in_fOrbitRadius * GameManager.UNIT_SIZE;
            fOrbitSpeed = in_fOrbitSpeed;

            int i;
            for (i = 0; i < in_iNumber; i++) {
                Enemy enemyChild = new EnemyOrbitalChild(gamemanager.sprEnemyChild, gamemanager);
                children.Add(enemyChild);
                gamemanager.enemies.Add(enemyChild);
            }

        }

    }
}
