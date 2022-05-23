using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EnemyBehaviors {
    class EnemyOrbitalVariableRadiusPlusMoving : Enemy {
        float fLifetime;
        float fMinOrbitRadius;
        float fMaxOrbitRadius;
        float fCurrentOrbitRadius;
        float fOrbitChangeVel;
        float fOrbitSpeed; //1f = 1 orbit every second
        float fOrbitChangeSpeed = 1f * GameManager.UNIT_SIZE;
        List<Enemy> children;

        float fVelChangeCountdown;
        float fMaxCountdown;

        public EnemyOrbitalVariableRadiusPlusMoving(Texture2D in_spr, GameManager in_gamemanager) : base(in_spr, in_gamemanager) {
            gamemanager = in_gamemanager;
            children = new List<Enemy>();
            fLifetime = 0f;

            fMaxCountdown = 3f;
            fVelChangeCountdown = fMaxCountdown;
            vel_x = 1f * GameManager.UNIT_SIZE;
           
        }

        public override void Update(float deltaTime) {


            UpdateBackAndForthTimer(deltaTime);
            UpdateOrbitalChildren(deltaTime);
            UpdateOrbitRadius(deltaTime);


        }

        private void UpdateBackAndForthTimer(float deltaTime) {
            fVelChangeCountdown -= deltaTime;
            if (fVelChangeCountdown <= 0f) {
                vel_x *= -1f;
                fVelChangeCountdown += fMaxCountdown;
            }

            x += vel_x * deltaTime;

        }

        private void UpdateOrbitalChildren(float deltaTime) {
            fLifetime += deltaTime * MathF.PI * 2f;
            int i;
            for (i = 0; i < children.Count; i++) {
                float fAngleOffset = ((float)i / (float)children.Count) * MathF.PI * 2f;
                int parent_center_x = (int)(x + (w / 2f));
                int parent_center_y = (int)(y + (h / 2f));
                int child_x = (int)(parent_center_x + fCurrentOrbitRadius * MathF.Cos((fLifetime * fOrbitSpeed) + fAngleOffset));
                int child_y = (int)(parent_center_y + fCurrentOrbitRadius * MathF.Sin((fLifetime * fOrbitSpeed) + fAngleOffset));

                //subtract off half of child's width and height to center
                child_x -= (children[i].w / 2);
                child_y -= (children[i].h / 2);
                children[i].setPosition(child_x, child_y);
            }

        }

        private void UpdateOrbitRadius(float deltaTime) {
            //change the orbit radius
            fCurrentOrbitRadius += fOrbitChangeVel * deltaTime;
            if (fCurrentOrbitRadius >= fMaxOrbitRadius) {
                fOrbitChangeVel = fOrbitChangeSpeed * -1f;
            } else if (fCurrentOrbitRadius <= fMinOrbitRadius) {
                fOrbitChangeVel = fOrbitChangeSpeed;

            }

        }


        public override void Draw(SpriteBatch in_spritebatch) {
            in_spritebatch.Draw(spr, new Rectangle((int)x, (int)y, w, h), Color.White);

            in_spritebatch.DrawString(gamemanager.myfontsmall,
                string.Format("children: {0}\norbit radius: {1:0.00}\nmin orbit radius: {2}\nmax orbit radius: {3}\norbit speed: {4}",
                children.Count, fCurrentOrbitRadius / GameManager.UNIT_SIZE, fMinOrbitRadius / GameManager.UNIT_SIZE, fMaxOrbitRadius / GameManager.UNIT_SIZE, fOrbitSpeed),
                new Vector2(x + w, y),
                Color.White);


        }


        public void setChildren(int in_iNumber, float in_fMinOrbitRadius, float in_fMaxOrbitRadius, float in_fOrbitSpeed) {
            fMinOrbitRadius = in_fMinOrbitRadius * GameManager.UNIT_SIZE;
            fMaxOrbitRadius = in_fMaxOrbitRadius * GameManager.UNIT_SIZE;
            fCurrentOrbitRadius = fMinOrbitRadius;
            fOrbitChangeVel = fOrbitChangeSpeed;
            
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

