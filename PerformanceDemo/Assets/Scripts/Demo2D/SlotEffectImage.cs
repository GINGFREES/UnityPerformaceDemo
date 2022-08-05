namespace PerformanceDemo.Demo2D
{
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.U2D;

    public class SlotEffectImage : MonoBehaviour
    {
        [SerializeField] private SpriteAtlas[] spriteAtlases;
        [SerializeField] private Image[] imageList;

        public int ImagesCount => imageList.Length;

        private void UpdateImageBySprites(int index, int atlasIndex)
        {
            string name = $"Icon_0{atlasIndex + 1}_00";
            Sprite sprite = SlotManager.Instance.GetRandomSprite(name, spriteAtlases[atlasIndex]);
            imageList[index].sprite = sprite;
        }

        public void UpdateImage(int[] randomIndexes)
        {
            for (int i = 0; i < ImagesCount; i++)
            {
                UpdateImageBySprites(i, randomIndexes[i]);
            }
        }
    }
}