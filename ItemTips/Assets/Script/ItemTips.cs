using DG.Tweening;
using UnityEngine;

public class ItemTips : MonoBehaviour
{

    public GameObject _prefab;      //格子的Prefab
    public int cellWidth;           //格子的宽度
    public int cellHeight;          //格子的高度
    public int Spacing;             //格子的间距
    public RectTransform _border;   //背景框

    private float _time = 0.5f;

    void Start()
    {
        int i = 0, j = 0;
        //物品数量应该由外部传入，这里我用随机数代替，Random.Range包前不包后，也就是随机值为1~15
        int itemCount = Random.Range(1, 16);
        //横向的数量
        int horizontalCount = Mathf.Min(itemCount, 5);
        //纵向的数量
        int verticalCount = Mathf.Min(Mathf.CeilToInt(itemCount / 5f), 3);

        for (int k = 0; k < itemCount; k++)
        {
            //计算X轴的偏移
            int valueX = (i * (cellWidth + Spacing)) - (horizontalCount - 1) * (cellWidth + Spacing) / 2;
            //计算Y轴的偏移
            int valueY = (j * (cellHeight + Spacing)) - (verticalCount - 1) * (cellHeight + Spacing) / 2;
            //实例化对象并设置名称和坐标
            GameObject go = GameTools.AddChild(transform, _prefab);
            go.name = "Item" + k;
            //下面的Y值取反，是因为顺着Y轴方向向下，数值减小
            go.transform.localPosition = new Vector3(valueX, -valueY, 0);
            //这里主要判断当一行图标排满5个时，使其换行
            if (++i == 5) { i = 0; j++; }

            //下面的这几句跟布局无关，就是做缓动效果的

            //缓动 透明度
            CanvasGroup icon = go.GetComponent<CanvasGroup>();
            icon.alpha = 0;
            icon.DOFade(1, _time).SetDelay(k * 0.1f);
            //缓动 缩放
            icon.transform.localScale = Vector3.one * 0.5f;
            icon.transform.DOScale(1, _time).SetDelay(k * 0.1f);
        }
        //动态改变背景框的大小
        _border.sizeDelta = new Vector2(horizontalCount * cellWidth + 20, verticalCount * cellHeight + 20);
    }
}
