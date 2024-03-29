﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefence
{
    abstract class TextureManager
    {
        public static Texture2D backgroundTexture;
        public static Texture2D towerTexture;
        public static Texture2D wowerTexture;
        public static Texture2D enemyTexture;
        public static Texture2D enemySpriteSheet;
        public static Texture2D enemyHurtSpriteSheet;
        public static Texture2D bulletTexture;
        public static Texture2D hitboxTexture;
        public static Texture2D heartTexture;

        public static Texture2D transparentBackground;

        public static void LoadContent(ContentManager Content)
        {
            transparentBackground = Content.Load<Texture2D>("transparentbackground1280x720");

            backgroundTexture = Content.Load<Texture2D>("background");
            towerTexture = Content.Load<Texture2D>("towers");
            wowerTexture = Content.Load<Texture2D>("tower");
            bulletTexture = Content.Load<Texture2D>("bullets");
            enemyTexture = Content.Load<Texture2D>("enemy");
            enemySpriteSheet = Content.Load<Texture2D>("enemySpriteSheet");
            enemyHurtSpriteSheet = Content.Load<Texture2D>("enemyHurtSpriteSheet");
            hitboxTexture = Content.Load<Texture2D>("tile");
            heartTexture = Content.Load<Texture2D>("heart");
        }
    }
}
