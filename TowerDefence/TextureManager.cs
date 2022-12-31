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
        public static Texture2D towerTexture;
        public static Texture2D enemyTexture;
        public static Texture2D bulletTexture;

        public static void LoadContent(ContentManager Content)
        {
            towerTexture = Content.Load<Texture2D>("tower");
            bulletTexture = Content.Load<Texture2D>("bullet");
            enemyTexture = Content.Load<Texture2D>("enemy");
        }
    }
}
