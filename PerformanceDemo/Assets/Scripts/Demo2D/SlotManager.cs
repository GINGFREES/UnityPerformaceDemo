
namespace PerformanceDemo.Demo2D
{
    using System;
    using UnityEngine;
    using UnityEngine.U2D;
    using System.Collections.Generic;
    public class SlotManager
    {
        private static Lazy<SlotManager> LazyInstance =
            new Lazy<SlotManager>(() => new SlotManager());

        public static SlotManager Instance => LazyInstance.Value;

        public static void CleanUp()
        {
            LazyInstance = null;
            GC.Collect();
        }

        private Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();

        private int updateTime = 0;
        private int[] currentRandomIndexes = null;

        public Sprite GetRandomSprite(string name, SpriteAtlas spriteAtlas)
        {
            if (!sprites.ContainsKey(name))
            {
                sprites.Add(name, spriteAtlas.GetSprite(name));
            }
            return sprites[name];
            //return spriteAtlas.GetSprite(name);
        }

        public int[] GetRandomIndex(int count)
        {
            if (updateTime > 0)
            {
                updateTime++;
                if (updateTime == 4)
                {
                    updateTime = 0;
                }
                return currentRandomIndexes;
            }

            updateTime++;
            if (currentRandomIndexes == null)
            {
                currentRandomIndexes = new int[count];
            }

            for (int i = 0; i < count; i++)
            {
                currentRandomIndexes[i] = UnityEngine.Random.Range(0, 4);
            }
            return currentRandomIndexes;
        }
    }
}