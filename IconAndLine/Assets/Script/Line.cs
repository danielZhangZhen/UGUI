using UnityEngine;

public class Line : MonoBehaviour
{
    public GameObject square;
    public GameObject triangle;

    /// <summary>
    /// 根据长度和角度开始画线
    /// </summary>
    /// <param name="length">长度</param>
    /// <param name="angle">角度</param>
    public void DrawLine(float length, float angle)
    {
        int count = Mathf.FloorToInt((length - 120) / 16);
        int i = 0;
        for (i = 0; i < count; i++)
        {
            GameObject go = GameTools.AddChild(transform, square);
            go.transform.localPosition = new Vector3(i * 16 + 60, 0, 0);
        }
        GameObject tri = GameTools.AddChild(transform, triangle);
        tri.transform.localPosition = new Vector3(i * 16 + 60, 0, 0);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
