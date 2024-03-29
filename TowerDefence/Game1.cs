﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UserInterfaceForm;

namespace TowerDefence
{
    public enum GameStates { Menu, Play, End }

    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public static Form1 userInterfaceForm;

        public static Point windowSize { get; private set; }
        public static Random random = new();

        Emitter particleSystem;

        public static Player player;

        public static GameStates gameState;

        static Rectangle enemyPathX;
        static Rectangle enemyPathY;
        public static Rectangle EnemyPathX { get { return enemyPathX; } }
        public static Rectangle EnemyPathY { get { return enemyPathY; } }

        public static RenderTarget2D renderTarget;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            windowSize = new Point(1280, 720);
            graphics.PreferredBackBufferWidth = windowSize.X;
            graphics.PreferredBackBufferHeight = windowSize.Y;
            graphics.ApplyChanges();

            enemyPathX = new Rectangle(0, (windowSize.Y - 100) / 2, windowSize.X, 100);
            enemyPathY = new Rectangle((windowSize.X - 100) / 2, 0, 100, windowSize.Y);

            renderTarget = new RenderTarget2D(GraphicsDevice, windowSize.X, windowSize.Y);

            gameState = GameStates.Menu;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            TextureManager.LoadContent(Content);

            //DrawOnRenderTarget();

            particleSystem = new Emitter(TextureManager.bulletTexture, Vector2.Zero);
            
            userInterfaceForm = new Form1();
            userInterfaceForm.Show();

            player = new Player();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (userInterfaceForm.startButtonPressed)
            {
                StartGame();
                Debug.WriteLine("Start Button Pressed");
                userInterfaceForm.startButtonPressed = false;
            }

            particleSystem.Position = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            particleSystem.Update();

            switch (gameState)
            {
                case GameStates.Menu:
                    break;
                case GameStates.Play:
                    player.Update(gameTime);

                    // ToList() iterates over a copy of the enemies list so as to not cause error when modifying the original list. Bad for large collections => performance hit.
                    foreach (Enemy enemy in Enemy.enemies.ToList())
                        enemy.Update(gameTime);

                    foreach (Tower tower in Tower.towers.ToList())
                        tower.Update(gameTime);

                    foreach (Bullet bullet in Bullet.bullets.ToList())
                        bullet.Update(gameTime);
                    break;
                case GameStates.End:
                    break;
                default:
                    break;
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            DrawOnRenderTarget();

            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(TextureManager.backgroundTexture, Vector2.Zero, Color.White);

            spriteBatch.Draw(renderTarget, Vector2.Zero, Color.White);

            player.Draw(spriteBatch);

            particleSystem.Draw(spriteBatch);

            //DrawPathHitboxes();

            foreach (Enemy enemy in Enemy.enemies)
                enemy.Draw(spriteBatch);

            foreach (Tower tower in Tower.towers)
                if(tower.preview)
                    tower.Draw(spriteBatch);

            foreach (Bullet bullet in Bullet.bullets)
                bullet.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void DrawOnRenderTarget()
        {
            GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(Color.Transparent);

            spriteBatch.Begin();

            //spriteBatch.Draw(renderTarget, Vector2.Zero, Color.White);
            foreach (Tower tower in Tower.towers)
                if(!tower.preview)
                    tower.Draw(spriteBatch);
            spriteBatch.Draw(TextureManager.transparentBackground, Vector2.Zero, Color.White);

            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
        }

        public void DrawPathHitboxes()
        {
            spriteBatch.Draw(TextureManager.hitboxTexture, enemyPathX, Color.Green * 0.5f);
            spriteBatch.Draw(TextureManager.hitboxTexture, enemyPathY, Color.Green * 0.5f);
        }

        public void StartGame()
        {
            player.Reset();
            Enemy.enemies.Clear();
            Bullet.bullets.Clear();
            Tower.towers.Clear();
            gameState = GameStates.Play;
        }
    }
}