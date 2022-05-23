//2022 - Levi D. Smith
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace EnemyBehaviors {
    public class GameManager : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Texture2D sprPlayer;
        public Texture2D sprEnemy;
        public Texture2D sprBlock;
        public Texture2D sprEnemyChild;

        public List<Enemy> enemies;
        public List<Block> blocks;
        public List<Player> players;

        public SpriteFont myfont;
        public SpriteFont myfontsmall;
        KeyboardState previousState;

        int iScreen;
        public const int TOTAL_SCREENS = 6;
        public const int UNIT_SIZE = 64;

        public GameManager() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            iScreen = 0;
        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here

            base.Initialize();

            enemies = new List<Enemy>();
            blocks = new List<Block>();
            players = new List<Player>();

            loadScreen();



        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            sprPlayer = Content.Load<Texture2D>("player");
            sprEnemy = Content.Load<Texture2D>("enemy");
            sprEnemyChild = Content.Load<Texture2D>("enemy_child");
            sprBlock = Content.Load<Texture2D>("block");

            myfont = Content.Load<SpriteFont>("myfont");
            myfontsmall = Content.Load<SpriteFont>("myfontsmall");

        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            handleInputKeyboard();


            // TODO: Add your update logic here
            foreach (Enemy enemy in enemies) {
                enemy.Update((float) gameTime.ElapsedGameTime.TotalSeconds);

            }

            foreach (Player player in players) {
                player.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            }

            base.Update(gameTime);
        }

        private void handleInputKeyboard() {
            KeyboardState state = Keyboard.GetState();
            Keys key;

            key = Keys.Space;
            if (state.IsKeyDown(key) && !previousState.IsKeyDown(key)) {
                nextScreen();
            }

            //player controls
            key = Keys.Left;
            if (state.IsKeyDown(key) && !previousState.IsKeyDown(key)) {
                foreach (Player player in players) {
                    player.vel_x = -2f * UNIT_SIZE;
                }
            } else if (!state.IsKeyDown(key) && previousState.IsKeyDown(key)) {
                foreach (Player player in players) {
                    if (player.vel_x < 0f) {
                        player.vel_x = 0f;
                    }
                }
            }

            key = Keys.Right;
            if (state.IsKeyDown(key) && !previousState.IsKeyDown(key)) {
                foreach (Player player in players) {
                    player.vel_x = 2f * UNIT_SIZE;
                }
            } else if (!state.IsKeyDown(key) && previousState.IsKeyDown(key)) {
                foreach (Player player in players) {
                    if (player.vel_x > 0f) {
                        player.vel_x = 0f;
                    }
                }
            }

            key = Keys.Up;
            if (state.IsKeyDown(key) && !previousState.IsKeyDown(key)) {
                foreach (Player player in players) {
                    player.vel_y = -2f * UNIT_SIZE;
                }
            } else if (!state.IsKeyDown(key) && previousState.IsKeyDown(key)) {
                foreach (Player player in players) {
                    if (player.vel_y < 0f) {
                        player.vel_y = 0f;
                    }
                }
            }

            key = Keys.Down;
            if (state.IsKeyDown(key) && !previousState.IsKeyDown(key)) {
                foreach (Player player in players) {
                    player.vel_y = 2f * UNIT_SIZE;
                }
            } else if (!state.IsKeyDown(key) && previousState.IsKeyDown(key)) {
                foreach (Player player in players) {
                    if (player.vel_y > 0f) {
                        player.vel_y = 0f;
                    }
                }
            }



            previousState = state;

        }

        private void nextScreen() {
            iScreen++;
            if(iScreen >= TOTAL_SCREENS) {
                iScreen = 0;
            }
            loadScreen();

        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            drawScreenTitle();

            foreach (Block block in blocks) {
                block.Draw(_spriteBatch);
            }

            foreach (Enemy enemy in enemies) {
                enemy.Draw(_spriteBatch);
            }

            foreach (Player player in players) {
                player.Draw(_spriteBatch);
            }


            _spriteBatch.End();

            base.Draw(gameTime);
            
        }

        private void drawScreenTitle() {
            string strTitle = "";
            if (iScreen == 0) {
                strTitle = "Back and Forth - Timer based";
            } else if (iScreen == 1) {
                strTitle = "Back and Forth - Collision based";
            } else if (iScreen == 2) {
                strTitle = "Enemy Chase";
            } else if (iScreen == 3) {
                strTitle = "Move, Stop, Change Direction";
            } else if (iScreen == 4) {
                strTitle = "Orbital Children";
            } else if (iScreen == 5) {
                strTitle = "Orbital Children - variable radius\nplus moving";

            }

            _spriteBatch.DrawString(myfont, strTitle, new Vector2(32, 32), Color.White);

        }

        public void loadScreen() {
            enemies.Clear();
            blocks.Clear();
            players.Clear();


            if (iScreen == 0) {
                Enemy enemy;

                enemy = new EnemyBackAndForthTimer(sprEnemy, this);
                enemy.setPosition(UNIT_SIZE * 1, UNIT_SIZE * 2);
                enemy.vel_x = UNIT_SIZE * 2f;
                ((EnemyBackAndForthTimer)enemy).setCountdown(2f);
                
                enemies.Add(enemy);

                enemy = new EnemyBackAndForthTimer(sprEnemy, this);
                enemy.setPosition(UNIT_SIZE * 1, UNIT_SIZE * 4);
                enemy.vel_x = UNIT_SIZE * 2f;
                ((EnemyBackAndForthTimer)enemy).setCountdown(1f);
                enemies.Add(enemy);

                enemy = new EnemyBackAndForthTimer(sprEnemy, this);
                enemy.setPosition(UNIT_SIZE * 1, UNIT_SIZE * 6);
                enemy.vel_x = UNIT_SIZE * 4f;
                ((EnemyBackAndForthTimer)enemy).setCountdown(2f);
                enemies.Add(enemy);

            } else if (iScreen == 1) {
                Enemy enemy;
                Block block;

                //enemy 1
                enemy = new EnemyBackAndForthCollision(sprEnemy, this);
                enemy.setPosition(UNIT_SIZE * 4, UNIT_SIZE * 2);
                enemy.vel_x = UNIT_SIZE * 2f;
                enemies.Add(enemy);

                block = new Block(sprBlock, UNIT_SIZE * 1, UNIT_SIZE * 2);
                blocks.Add(block);

                block = new Block(sprBlock, UNIT_SIZE * 7, UNIT_SIZE * 2);
                blocks.Add(block);

                //enemy 2
                enemy = new EnemyBackAndForthCollision(sprEnemy, this);
                enemy.setPosition(UNIT_SIZE * 4, UNIT_SIZE * 4);
                enemy.vel_x = UNIT_SIZE * 2f;
                enemies.Add(enemy);

                block = new Block(sprBlock, (int) (UNIT_SIZE * 2.5f), UNIT_SIZE * 4);
                blocks.Add(block);

                block = new Block(sprBlock, (int) (UNIT_SIZE * 5.5f), UNIT_SIZE * 4);
                blocks.Add(block);

                //enemy 3
                enemy = new EnemyBackAndForthCollision(sprEnemy, this);
                enemy.setPosition(UNIT_SIZE * 4, UNIT_SIZE * 6);
                enemy.vel_x = UNIT_SIZE * 4f;
                enemies.Add(enemy);

                block = new Block(sprBlock, UNIT_SIZE * 0, UNIT_SIZE * 6);
                blocks.Add(block);

                block = new Block(sprBlock, UNIT_SIZE * 10, UNIT_SIZE * 6);
                blocks.Add(block);

            } else if (iScreen == 2) {
                Player player;
                Enemy enemy;

                //player 1
                player = new Player(sprPlayer, this);
                player.setPosition(UNIT_SIZE * 2, UNIT_SIZE * 2);
                players.Add(player);

                //enemy 1
                enemy = new EnemyChaseEndDistance(sprEnemy, this);
                enemy.setPosition(UNIT_SIZE * 8, UNIT_SIZE * 2);
                enemies.Add(enemy);


                //enemy 2
                enemy = new EnemyChaseEndTimer(sprEnemy, this);
                enemy.setPosition(UNIT_SIZE * 8, UNIT_SIZE * 5);
                enemies.Add(enemy);

            } else if (iScreen == 3) {
                Enemy enemy;

                //enemy 1
                enemy = new EnemyMoveStopChangeDirection(sprEnemy, this);
                ((EnemyMoveStopChangeDirection)enemy).setCountdown(1f, 0.5f);
                enemy.setPosition(UNIT_SIZE * 2, UNIT_SIZE * 2);
                enemies.Add(enemy);

                //enemy 2
                enemy = new EnemyMoveStopChangeDirection(sprEnemy, this);
                ((EnemyMoveStopChangeDirection)enemy).setCountdown(1f, 2f);
                enemy.setPosition(UNIT_SIZE * 8, UNIT_SIZE * 2);
                enemies.Add(enemy);

                //enemy 3
                enemy = new EnemyMoveStopChangeDirection(sprEnemy, this);
                ((EnemyMoveStopChangeDirection)enemy).setCountdown(0.25f, 0.25f);
                enemy.setPosition(UNIT_SIZE * 4, UNIT_SIZE * 6);
                enemies.Add(enemy);


            } else if (iScreen == 4) {
                Enemy enemy;

                //enemy 1
                enemy = new EnemyOrbital(sprEnemy, this);
                ((EnemyOrbital)enemy).setChildren(8, 2f, 0.5f);
                enemy.setPosition(UNIT_SIZE * 2, UNIT_SIZE * 2);
                enemies.Add(enemy);

                //enemy 2
                enemy = new EnemyOrbital(sprEnemy, this);
                ((EnemyOrbital)enemy).setChildren(10, 3f, 0.25f);
                enemy.setPosition(UNIT_SIZE * 8, UNIT_SIZE * 2);
                enemies.Add(enemy);

                //enemy 3
                enemy = new EnemyOrbital(sprEnemy, this);
                ((EnemyOrbital)enemy).setChildren(4, 1f, -1f);
                enemy.setPosition(UNIT_SIZE * 2, UNIT_SIZE * 6);
                enemies.Add(enemy);

            } else if (iScreen == 5) {
                Enemy enemy;

                //enemy 1
                enemy = new EnemyOrbitalVariableRadiusPlusMoving(sprEnemy, this);
                ((EnemyOrbitalVariableRadiusPlusMoving)enemy).setChildren(8, 1f, 3f, 0.5f);
                enemy.setPosition(UNIT_SIZE * 4, UNIT_SIZE * 4);
                enemies.Add(enemy);

            }

        }

        public bool checkCollision(Enemy in_enemy, Block in_block) {
            bool hasCollided = true;
            if (in_enemy.x + in_enemy.w < in_block.x || 
                in_enemy.x > in_block.x + in_block.w ||
                in_enemy.y + in_enemy.h < in_block.y ||
                in_enemy.y > in_block.y + in_block.h) {
                hasCollided = false;
            }

            return hasCollided;
        }
    }
}
