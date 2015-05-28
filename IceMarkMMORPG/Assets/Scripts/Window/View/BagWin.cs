//-------------------------------
//该Demo由风冻冰痕所写
//http://icemark.cn/blog
//转载请说明出处
//-------------------------------
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class BagWin : MonoBase
{
    private BagController _controller;

    public Transform itemPanel;
    public GameObject itemPrefab;
    public List<Toggle> _toggleList;

    private List<BagItem> _newItemList;
    private List<BagItem> _oldItemList;
    private int _bagType;               //当前的背包类型[0:消耗][1:材料][2:装备]
    private bool _isPlaying = false;    //标记是否正在播放缓动动画

    void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            Toggle toggle = _toggleList[i];
            int index = i;
            _toggleList[i].onValueChanged.AddListener(delegate(bool value) { OnValueChange(index, value); });
        }
    }

    void Start()
    {
        _newItemList = new List<BagItem>();
        CreateItemGrid(0);
        _bagType = 0;
        UpdateBagData(_bagType, -1);
    }

    private void OnValueChange(int index, bool value)
    {
        if (_isPlaying)
        {
            _toggleList[_bagType].isOn = true;
            return;
        }
        if (value && index != _bagType)
        {
            _isPlaying = true;

            //应该会有人奇怪为什么下面这句要这样写而不是直接写成：
            //_olditemList = _newitemList
            //是因为这样写是相当于新建一个列表，只是里面的元素跟_newitemList的一样
            //但是 _olditemList = _newitemList 的话，这两个列表所引用的对象就是同一组元素了
            _oldItemList = new List<BagItem>(_newItemList);

            CreateItemGrid(_bagType < index ? 300 : -300);
            int offset = _bagType < index ? -300 : 300;
            int num = -offset / 300;
            for (int i = _oldItemList.Count - 1; i >= 0; i--)
            {
                GameObject item = _oldItemList[i].gameObject;
                Tweener tween = item.transform.DOLocalMoveX(offset, 0.5f);
                tween.SetRelative();
                tween.SetEase(Ease.InOutCubic);

                //本来我的想法是每一个元素缓动完成后就把自身删除掉
                //但是实测证明这样的做法会导致卡顿
                //所以就换一种方式实现，改成等所有缓动动画结束后再统一删除
                //所以注释掉下面这一行
                //tween.OnComplete(delegate() { GameTools.Destroy(item); });

                //下面这一句注释掉的原因是因为整个背包切换的动画始终结束于新添加的格子，所以只需要侦听_newItemList的缓动完成就好，性能问题，能省则省
                //tween.OnComplete(OnTweenComplete);

                //呃……下面这一句，能看懂的就看看，看不懂的就照抄
                //主要作用是当背包切换动画从左往右切换时，先从第5排开始
                //而从右往左切换就是从第1排开始
                //这里的i % 5的取值始终是[0,1,2,3,4]里面的2f就是这里取值的中间值：2
                //num其实就是+1和-1，
                float delay = (i % 5) * num + (1 - num) * 2f;
                //乘以0.02f就是减少每一排的切换间隔
                tween.SetDelay(delay * 0.02f);
            }
            for (int i = _newItemList.Count - 1; i >= 0; i--)
            {
                GameObject item = _newItemList[i].gameObject;
                Tweener tween = item.transform.DOLocalMoveX(offset, 0.5f);
                tween.SetRelative();
                tween.SetEase(Ease.InOutCubic);

                tween.OnComplete(OnTweenComplete);

                //下面这句跟上面的一样↑
                float delay = (i % 5) * num + (1 - num) * 2f;
                //+5是因为后面这一页动画要在前一页开始后才开始，乘以0.02f就是减少每一排的切换间隔
                tween.SetDelay((delay + 5) * 0.02f);
            }
            _bagType = index;
            UpdateBagData(_bagType, -1);
        }
    }

    private int index = 0;
    private void OnTweenComplete()
    {
        index++;
        //当index累计到30，也就是所有缓动完成后，证明当次背包切换完成
        if (index == 30)
        {
            //用for循环遍历删除元素的时候，要从后往前删，不然会出错，有疑问的童鞋可以尝试一下从前往后删
            for (int i = _oldItemList.Count - 1; i >= 0; i--)
            {
                GameTools.Destroy(_oldItemList[i].gameObject);
            }
            //当然，也可以用下面的这个办法删除所有元素
            //while (_olditemList.Count > 0)
            //{
            //    GameTools.Destroy(_olditemList[0].gameObject);
            //    _olditemList.RemoveAt(0);
            //}
            _oldItemList.Clear();
            index = 0;
            _isPlaying = false;
        }
    }

    /// <summary>
    /// 实例化一整页的Item
    /// </summary>
    /// <param name="offset">偏移[-300:前一页][0:当前][300:后一页]</param>
    private void CreateItemGrid(int offset)
    {
        _newItemList.Clear();
        int index = 0;
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject go = GameTools.AddChild(itemPanel, itemPrefab);
                go.name = "item " + (index < 10 ? "0" + index : index.ToString());
                go.transform.localPosition = new Vector3(j * 60 - 120 + offset, 150 - i * 60, 0);
                _newItemList.Add(go.GetComponent<BagItem>());
                index++;
            }
        }
    }

    /// <summary>
    /// 更新指定BagType和Index的Item
    /// </summary>
    /// <param name="bagType">背包类型[0:消耗][1:材料][2:装备]</param>
    /// <param name="index">索引[-1:所有][0~29:Index]</param>
    public void UpdateBagData(int bagType, int index)
    {
        if (bagType != _bagType) return;
        Dictionary<int, DataItem> dataDict = GameData.BagData.GetItemDictByBagType(_bagType);
        if (index < 0)
        {
            for (int i = 0; i < 30; i++)
            {
                _newItemList[i].SetInfo(_bagType, i, dataDict.ContainsKey(i) ? dataDict[i] : null);
            }
        }
        else
        {
            _newItemList[index].SetInfo(_bagType, index, dataDict.ContainsKey(index) ? dataDict[index] : null);
        }
    }

    public void OnArrangeBag()
    {
        GameData.BagData.ArrangeBag(_bagType);
    }

    /// <summary>
    /// 关闭窗口
    /// </summary>
    public void OnCloseWindow()
    {
        WindowManager.GetInstance().CloseWindow(Window.BagWin);
    }

    public BagController Controller { set { _controller = value; } }
}
