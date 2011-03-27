using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SheepsGame.GameObjects.Ufo
{
    class Ray : GameObject
    {
        private const string textureName = "ray";

        private byte alpha = 150;
        private bool alphaGrow = false;

        Sheep current_sheep = null;
        bool abducting = false;

        public Ray() : base(Vector2.Zero, textureName) 
        {
            visible = false;
            this.originRelative = Origin.TopCenter;
        }

        public void fire()
        {
            this.visible = true;

            if (!hasSheep() && !Game1.game.player.hasSheep())
            {
                Sheep found_sheep = findSheep();
                if (found_sheep != null)
                    startSheepAbduction(found_sheep);
            }
            else if (isOverPlatform() && Game1.game.player.hasSheep())
            {
                startSheepDescention(Game1.game.player.popSheep());
            }
        }

        public void stopFire()
        {
            visible = false;
            abortAbduction();
        }

        void startSheepAbduction(Sheep sheep)
        {
            current_sheep = sheep;
            current_sheep.freezed = true;
            abducting = true;
        }

        void abortAbduction()
        {
            abducting = false;
        }

        void startSheepDescention(Sheep sheep)
        {
            current_sheep = sheep;
            current_sheep.position = this.position;
            current_sheep.visible = true;
            abducting = false;
        }

        bool isOverPlatform()
        {
            if (this.Bounds.Intersects(Game1.game.platform.Bounds))
                return true;
            return false;
        }

        bool hasSheep()
        {
            return current_sheep != null;
        }

        Sheep findSheep()
        {
            Sheep nearestSheep = null;
            foreach (Sheep sheep in Game1.game.sheeps)
            {
                if (this.Bounds.Intersects(sheep.Bounds))
                {
                    if (nearestSheep == null)
                        nearestSheep = sheep;
                    else
                        nearestSheep = getNearestSheep(nearestSheep, sheep);
                }
            }

            return nearestSheep;
        }

        Sheep getNearestSheep(Sheep one, Sheep two)
        {
            if (sqrDistance(this.position, one.position) < sqrDistance(this.position, two.position))
                return one;
            return two;
        }

        float sqrDistance(Vector2 a, Vector2 b)
        {
            return (b.X - a.X) * (b.X - a.X) + (b.Y - a.Y) * (b.Y - a.Y);
        }

        public override void Update(GameTime gameTime)
        {
            animateRow();

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (hasSheep())
            {
                // Поднимаем овцу на борт
                if (abducting)
                {
                    processAbduction(elapsed);
                }
                else
                {
                    // Спускаем овцу на землю
                    processFalling(elapsed);

                }
            }
        }

        #region Update SubMethods
        private void processFalling(float elapsed)
        {
            if (current_sheep.position.Y < Sheep.getStandartSheepY())
            {
                const float fallingSpeed = 120f; // pixels per Second
                const float fallingScaleSpeed = 1.2f; //percent per Second

                current_sheep.position.Y += fallingSpeed * elapsed;
                current_sheep.scale += fallingScaleSpeed * elapsed;
            }
            else
            {
                current_sheep.scale = 1f;
                current_sheep.freezed = false;
                current_sheep = null;
            }
        }

        private void processAbduction(float elapsed)
        {
            const float shiftAccuracy = 5;
            const float abductionSpeed = 30f; //pixels per Second
            const float abductionScaleSpeed = .3f; //percent per Second

            if (current_sheep.position.X > position.X + shiftAccuracy)
                current_sheep.position.X -= abductionSpeed * elapsed;
            else if (current_sheep.position.X < position.X - shiftAccuracy)
                current_sheep.position.X += abductionSpeed * elapsed;

            current_sheep.position.Y -= abductionSpeed * elapsed;

            current_sheep.scale -= abductionScaleSpeed * elapsed;

            if (current_sheep.position.Y < position.Y || current_sheep.scale < .01f)
            {
                current_sheep.visible = false;
                Game1.game.player.setSheep(current_sheep);
                current_sheep = null;
            }
        }

        void animateRow()
        {
            if (visible)
            {
                // Анимция луча
                if (alphaGrow)
                    alpha += 5;
                else
                    alpha -= 5;

                if (alpha <= 120 || alpha >= 200)
                    alphaGrow = !alphaGrow;
            }
        }
        #endregion
    }
}
