using UnityEngine;

namespace Assets.Assets.Scripts.Collectables
{
    public class DroppedBlockCollectable : MonoBehaviour
    {
        public void Setup()
        {

        }

        public void OnCollected()
        {
            gameObject.SetActive(false);
        }
    }
}
