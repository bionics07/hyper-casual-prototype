using System.Collections.Generic;
using Assets.Assets.Scripts.Collectables;
using UnityEngine;

namespace Assets.Assets.Scripts.Character
{
    public class CharacterCarryItemManager : MonoBehaviour
    {
        private const string BLOCKS_TAG = "Blocks";

        [Header("References")]
        [SerializeField] private GameObject m_blocksContainer;
        [SerializeField] private GameObject m_carriedBlockPrefab;

        [Header("View")] [SerializeField] private float m_blocksDistance;

        private List<GameObject> m_activeCarriedBlocks = new List<GameObject>();
        private Queue<GameObject> m_inactiveCarriedBlocks = new Queue<GameObject>();

        public bool CanPlaceBlock => m_activeCarriedBlocks.Count > 0;

        private void AddBlock()
        {
            GameObject block = m_inactiveCarriedBlocks.Count > 0 ? m_inactiveCarriedBlocks.Dequeue() : GetNewBlock();
            block.gameObject.SetActive(true);
            block.transform.SetAsLastSibling();

            float positionOffset = m_blocksDistance * m_activeCarriedBlocks.Count;
            Vector3 containerPosition = m_blocksContainer.transform.position;
            block.transform.position = new Vector3(containerPosition.x, containerPosition.y + positionOffset, containerPosition.z);
            m_activeCarriedBlocks.Add(block);
        }

        public void RemoveBlock()
        {
            GameObject block = m_activeCarriedBlocks[^1];
            m_activeCarriedBlocks.Remove(block);
            block.gameObject.SetActive(false);
            m_inactiveCarriedBlocks.Enqueue(block);
        }

        private GameObject GetNewBlock()
        {
            GameObject block = Instantiate(m_carriedBlockPrefab, m_blocksContainer.transform);
            return block;
        }

        private void TryInteractWithStair(NextStairBlockView nextStairBlockView)
        {
            if (!CanPlaceBlock)
            {
                return;
            }

            nextStairBlockView.OnInteract();
            RemoveBlock();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out DroppedBlockCollectable droppedBlock))
            {
                AddBlock();
                droppedBlock.OnCollected();
            }
            else if (other.TryGetComponent(out NextStairBlockView nextStairBlockView))
            {
                TryInteractWithStair(nextStairBlockView);
            }

        }
    }
}
