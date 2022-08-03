namespace PerformanceDemo
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.U2D;
    using IModels;
    using System.Linq;

    public class EffectManager
    {
        private static Lazy<EffectManager> lazyEffectManager =
            new Lazy<EffectManager>(() => new EffectManager());

        public static EffectManager Instance => lazyEffectManager.Value;

        private List<IView> viewList = new List<IView>();
        public int viewCount => viewList.Count;

        private Dictionary<string, Sprite> sequenceSprites = new Dictionary<string, Sprite>();

        public static void CleanUp()
        {
            lazyEffectManager = new Lazy<EffectManager>(() => new EffectManager());
            GC.Collect();
        }

        public void AddView(IView view)
        {
            viewList.Add(view);
            viewList.Sort((x, y) => x.Fps - y.Fps);
        }

        public void EffectCallAll() => viewList.ForEach(x => x.EffectCall());

        public Sprite GetSprite(SpriteAtlas spriteAtlas, string name)
        {
            if (!sequenceSprites.ContainsKey(name))
            {
                sequenceSprites.Add(name, spriteAtlas.GetSprite(name));
            }
            return sequenceSprites[name];
        }

        public IView GetViewByIndex(int index) => viewList[index];
    }
}
