using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {
        //transform.Rotate(Vector3.up, Time.deltaTime * 60);
        transform.Rotate(0.3f, 0.3f, 0.3f);
    }
}
