using System.Collections.Generic;
using UnityEngine;

public class BlocksStairsController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Vector3 m_blocksDistanceOffset;
    [SerializeField] private int m_maxBlocksCount;

    [Header("References")] [SerializeField] private NextStairBlockView m_nextStairBlock;

    [Header("Prefabs")]
    [SerializeField] private GameObject m_blockViewPrefab;

    List<GameObject> m_blocks = new List<GameObject>();

    public bool CanCreateBlock => m_blocks.Count < m_maxBlocksCount;

    private void Start()
    {
        CreateNewBlock();
    }

    public bool TryCreateNewBlock()
    {
        if (CanCreateBlock)
        {
            CreateNewBlock();
            return true;
        }

        m_nextStairBlock.gameObject.SetActive(false);
        return false;
    }

    private void CreateNewBlock()
    {
        GameObject block = Instantiate(m_blockViewPrefab, transform);
        block.transform.position = GetNextBlockPosition();
        m_blocks.Add(block);
        UpdateNextStairBlockPosition();
    }

    private void UpdateNextStairBlockPosition()
    {
        if (CanCreateBlock)
        {
            m_nextStairBlock.transform.position = GetNextBlockPosition();
        }
        else
        {
            m_nextStairBlock.gameObject.SetActive(false);
        }
    }

    private Vector3 GetNextBlockPosition()
    {
        Vector3 offsetPosition =  new Vector3(m_blocksDistanceOffset.x * m_blocks.Count, m_blocksDistanceOffset.y * m_blocks.Count, m_blocksDistanceOffset.z * m_blocks.Count);
        return transform.position + offsetPosition;
    }
}
