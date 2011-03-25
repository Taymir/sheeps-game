using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace SheepsGame.GameObjects
{
    public abstract class GameObject
    {
        String textureName;
        protected Texture2D texture;
        public Vector2 position;
        protected Vector2 origin;
        public float rotation = 0f;
        public float scale = 1f;
        public Boolean visible = true;
        public Boolean dead = false;

        SpriteEffects flip = SpriteEffects.None;

        public bool flipped
        {
            get
            {
                return this.flip == SpriteEffects.FlipHorizontally;
            }
            set
            {
                if (value)
                    this.flip = SpriteEffects.FlipHorizontally;
                else
                    this.flip = SpriteEffects.None;
            }
        }

        public int Width
        {
            get
            {
                return this.texture.Width;
            }
        }

        public int Height
        {
            get 
            {
                return this.texture.Height;
            }
        }

        public Rectangle Bounds
        {
            get
            {
                if (!visible)
                    return new Rectangle(int.MinValue, int.MinValue, 0, 0);

                Rectangle bounds = this.texture.Bounds;
                bounds.Offset((int)(this.position.X - this.origin.X), (int)(this.position.Y - this.origin.Y));
                return bounds;
            }
        }

        public GameObject(Vector2 position, String textureName)
        {
            this.position = position;
            this.textureName = textureName;
            this.origin = Vector2.Zero;
        }

        public virtual void LoadContent()
        {
            texture = Game1.game.Content.Load<Texture2D>(textureName);
        }

        protected virtual void UpdatePosition(Vector2 velocity)
        {
            this.position += velocity;
        }

        protected void SetOriginInCenter()
        {
            this.origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public virtual void Update(GameTime gameTime)
        {
            //@EMPTY
        }

        public virtual void Draw(SpriteBatch sprite)
        {
            if (visible)
            {
                Vector2 screenPosition = Game1.level1.GetScreenPosition(position);
                sprite.Draw(texture, screenPosition, null, Color.White, rotation, origin, scale, flip, 0f);
            }
        }
    }
}
