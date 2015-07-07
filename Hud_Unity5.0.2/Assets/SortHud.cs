using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SortHud : MonoBehaviour
{
    void Update()
    {
        List<Transform> subTrans = new List<Transform>();
        foreach (Transform trans in transform)
        {
            subTrans.Add(trans);
        }
        subTrans.Sort((a, b) => { return (int)(b.position.z - a.position.z) * 1000; });
        for (int i = 0; i < subTrans.Count; i++)
        {
            subTrans[i].SetSiblingIndex(i);
        }
    }
}
