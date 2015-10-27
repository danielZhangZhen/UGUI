using UnityEngine;

public class UIDepth : MonoBehaviour
{
    public int orderInLayer;
    void Start()
    {
        Renderer[] renders = GetComponentsInChildren<Renderer>();

        foreach (Renderer render in renders)
        {
            render.sortingOrder = orderInLayer;
        }
    }
}