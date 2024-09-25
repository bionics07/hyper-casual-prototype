using UnityEngine;

public class NextStairBlockView : MonoBehaviour
{
    [SerializeField]
    private BlocksStairsController m_stairsController;

    public bool OnInteract()
    {
        return m_stairsController.TryCreateNewBlock();
    }
}
