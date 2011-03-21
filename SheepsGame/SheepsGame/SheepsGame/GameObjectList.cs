using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SheepsGame.GameObjects;

/*
 * Код взят из статьи: http://theindiestone.com/community/post1297.html
 * Автор: lemmy
 */

namespace SheepsGame
{
    public class GameObjectList : List<GameObject>
    {
        // internal list for handling deletions.
        List<GameObject> RemoveList = new List<GameObject>();

        // internal method for removing items we're done with.
        // (required as removing from a list during a foreach causes an exception)
        void PurgeRemovedItems()
        {
            foreach (var item in RemoveList)
            {
                Remove(item);
            }
            RemoveList.Clear();
        }

        public void LoadContent()
        {
            foreach (var item in this)
            {
                item.LoadContent();
            }
        }

        public void Update(GameTime Time)
        {
            foreach (var item in this)
            {
                item.Update(Time);
            }
        }

        public void Draw(SpriteBatch Batch)
        {
            foreach (var item in this)
            {
                item.Draw(Batch);
            }
        }
    }
}
