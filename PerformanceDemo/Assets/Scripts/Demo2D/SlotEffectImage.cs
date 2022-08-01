namespace PerformanceDemo.Demo2D
{
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.U2D;
    using System.Collections.Generic;

    public class SlotEffectImage : MonoBehaviour
    {
        [SerializeField] private SpriteAtlas[] spriteAtlases;
        [SerializeField] private Image[] imageList;

        private Dictionary<string, Sprite> spriteDics = new Dictionary<string, Sprite>();

        public void RandomImage()
        {
            for (int i = 0; i < imageList.Length; i++)
            {
                int atlasIndex = Random.Range(0, spriteAtlases.Length - 1);
                string name = $"Icon_0{atlasIndex + 1}_00";
                if (!spriteDics.ContainsKey(name))
                {
                    spriteDics.Add(name, spriteAtlases[atlasIndex].GetSprite(name));
                }
                imageList[i].sprite = spriteDics[name];
            }

        }
    }
}