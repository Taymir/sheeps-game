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
    public class GameObject
    {
        String textureName;
        protected Texture2D texture;
        public Vector2 position;
        protected Origin originRelative;
        private Vector2 origin;
        public float rotation = 0f;
        public float scale = 1f;
        public float depth = 0f;
        public Boolean visible = true;
        public Boolean dead = false;

        SpriteEffects flip = SpriteEffects.None;

        public int frameRate;
        bool animate;
        float vFrame;
        int frameWidth, frameHeight, framesPerRow, frameCount;

        public enum Origin
        {
            TopLeft,
            TopCenter,
            TopRight,
            
            MiddleLeft,
            MiddleCenter,
            MiddleRight,

            BottomLeft,
            BottomCenter,
            BottomRight
        }

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
            this.originRelative = Origin.TopLeft;
            this.origin = Vector2.Zero;
            this.animate = false;
        }

        public GameObject(Vector2 position, String textureName, int frameWidth, int frameHeight, int framesPerRow, int frameCount)
        {
            this.position = position;
            this.textureName = textureName;
            this.originRelative = Origin.TopLeft;
            this.origin = Vector2.Zero;
            
            this.animate = true;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.framesPerRow = framesPerRow;
            this.frameCount = frameCount;
            this.frameRate = 30;
            this.vFrame = 0f;
        }

        public virtual void LoadContent()
        {
            texture = Game1.game.Content.Load<Texture2D>(textureName);

            if(animate)
                this.origin = getOriginFromOriginRelative(originRelative, frameWidth, frameHeight);
            else
                this.origin = getOriginFromOriginRelative(originRelative, texture.Width, texture.Height);
        }

        private Vector2 getOriginFromOriginRelative(Origin relative, int width, int height)
        {
            Vector2 result = Vector2.Zero;

            switch (relative)
            {
                case Origin.TopLeft:
                    result = new Vector2(0f, 0f);
                    break;
                case Origin.TopCenter:
                    result = new Vector2(0.5f, 0f);
                    break;
                case Origin.TopRight:
                    result = new Vector2(1f, 0f);
                    break;
                case Origin.MiddleLeft:
                    result = new Vector2(0f, 0.5f);
                    break;
                case Origin.MiddleCenter:
                    result = new Vector2(0.5f, 0.5f);
                    break;
                case Origin.MiddleRight:
                    result = new Vector2(1f, 0.5f);
                    break;
                case Origin.BottomLeft:
                    result = new Vector2(0f, 1f);
                    break;
                case Origin.BottomCenter:
                    result = new Vector2(0.5f, 1f);
                    break;
                case Origin.BottomRight:
                    result = new Vector2(1f, 1f);
                    break;
            }
            result.X *= width;
            result.Y *= height;

            return result;
        }

        protected virtual void UpdatePosition(Vector2 velocity)
        {
            this.position += velocity;
        }

        public virtual void Update(GameTime gameTime)
        {
            //@EMPTY
            if (animate)
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

                vFrame += elapsed * frameRate;
                vFrame %= frameCount;
            }
        }

        public virtual void Draw(SpriteBatch sprite)
        {
            if (visible)
            {
                Vector2 screenPosition = Game1.level1.GetScreenPosition(position);

                Rectangle? sourceRect = null;
                if (animate)
                {
                    int Frame = (int)vFrame;
                    int y = (int)(Math.Floor(Frame / framesPerRow) * frameHeight);
                    int x = (int)(Frame % framesPerRow * frameWidth);

                    sourceRect = new Rectangle(x, y, frameWidth, frameHeight);
                }

                sprite.Draw(texture, screenPosition, sourceRect, Color.White, rotation, origin, scale, flip, depth);
            }
        }
    }
}
