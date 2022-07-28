namespace PerformanceDemo.Demo2D
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.U2D;
    using IModels;

    public class Demo2DManager
    {
        private static Lazy<Demo2DManager> lazyDemo2DManager =
            new Lazy<Demo2DManager>(() => new Demo2DManager());

        public static Demo2DManager Instance => lazyDemo2DManager.Value;

        private List<IView> viewList = new List<IView>();
        public int viewCount => viewList.Count;

        private Dictionary<string, Sprite> sequenceSprites = new Dictionary<string, Sprite>();

        public static void CleanUp()
        {
            lazyDemo2DManager = new Lazy<Demo2DManager>(() => new Demo2DManager());
            GC.Collect();
        }

        public void AddView(IView view) => viewList.Add(view);

        public void EffectCallAll() => viewList.ForEach(x => x.EffectCall());

        public Sprite GetSprite(SpriteAtlas spriteAtlas, string name)
        {
            if (!sequenceSprites.ContainsKey(name))
            {
                sequenceSprites.Add(name, spriteAtlas.GetSprite(name));
            }
            return sequenceSprites[name];
        }

    }
}
