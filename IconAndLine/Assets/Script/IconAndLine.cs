using UnityEngine;

public class IconAndLine : MonoBehaviour
{
    public Transform _icon1Trans;
    public Transform _icon2Trans;
    public GameObject linePrefab;

    void Start()
    {
        RandomPosition();
    }

    private void RandomPosition()
    {
        //这里为了看出各种效果，所以采用随机点的方式，各位根据实际情况来确定点的位置
        Vector3 pos1 = RandomVector();
        Vector3 pos2 = RandomVector();
        //两点之间的距离
        float length = Vector3.Distance(pos1, pos2);
        //为了不要让随机出的点挨得太近（当然 实际项目中也不可能），所以这里加个限制，当两点距离过小时，重新随机pos2
        while (length < 120)
        {
            pos2 = RandomVector();
            length = Vector3.Distance(pos1, pos2);
        }
        _icon1Trans.localPosition = pos1;
        _icon2Trans.localPosition = pos2;

        //两点的角度
        float angle = Mathf.Atan2(pos2.y - pos1.y, pos2.x - pos1.x) * 180 / Mathf.PI;
        GameObject go = GameTools.AddChild(_icon1Trans.transform, linePrefab);
        Line line = go.GetComponent<Line>();
        line.DrawLine(length, angle);
    }

    /// <summary>
    /// 随机出在屏幕上的一个点
    /// </summary>
    /// <returns></returns>
    private Vector3 RandomVector()
    {
        //这里的-50是因为此Demo上的Icon宽度为100，为了不会随机出的点导致Icon超出屏幕，所以-50
        int width = Random.Range(-(Screen.width / 2 - 50), Screen.width / 2 - 50);
        //这里的-43是因为此Demo上的Icon高度为86，为了不会随机出的点导致Icon超出屏幕，所以-43
        int height = Random.Range(-(Screen.height / 2 - 43), Screen.height / 2 - 43);
        Vector3 pos = new Vector3(width, height, 0);
        return pos;
    }
}
