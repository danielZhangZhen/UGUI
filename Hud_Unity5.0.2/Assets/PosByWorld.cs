﻿using UnityEngine;
using System.Collections;

public class PosByWorld : MonoBehaviour
{
    public Transform objTrans;
    public Vector3 offset = new Vector3(0, 1, 0);
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(objTrans.position + offset);
        this.transform.position = pos;
    }
}
