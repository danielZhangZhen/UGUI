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

    private List<BagItem> _newitemList;
    private List<BagItem> _olditemList;
    private Dictionary<int, DataItem> _dataDict;
    private int _nowPage;               //当前的背包页卡[0:消耗][1:材料][2:装备]
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

    private void OnValueChange(int index, bool value)
    {
        if (_isPlaying)
        {
            _toggleList[_nowPage].isOn = true;
            return;
        }
        if (value && index != _nowPage)
        {
            _isPlaying = true;

            //应该会有人奇怪为什么下面这句要这样写而不是直接写成：
            //_olditemList = _newitemList
            //是因为这样写是相当于新建一个列表，只是里面的元素跟_newitemList的一样
            //但是 _olditemList = _newitemList 的话，这两个列表所引用的对象就是同一组元素了
            _olditemList = new List<BagItem>(_newitemList);

            CreateItemGrid(_nowPage < index ? 300 : -300);
            int offset = _nowPage < index ? -300 : 300;
            int num = -offset / 300;
            for (int i = _olditemList.Count - 1; i >= 0; i--)
            {
                GameObject item = _olditemList[i].gameObject;
                Tweener tween = item.transform.DOLocalMoveX(offset, 0.5f);
                tween.SetRelative();
                tween.SetEase(Ease.InOutCubic);

                //本来我的想法是每一个元素缓动完成后就把自身删除掉
                //但是实测证明这样的做法会导致卡顿
                //所以就换一种方式实现，改成等所有缓动动画结束后再统一删除
                //所以注释掉下面这一行
                //tween.OnComplete(delegate() { GameTools.Destroy(item); });

                //下面这一句注释掉的原因是因为整个背包切换的动画始终结束于新添加的格子，所有只需要侦听_newitemList的缓动完成就好，性能问题，能省则省
                //tween.OnComplete(OnTweenComplete);

                //呃……下面这一句，能看懂的就看看，看不懂的就照抄
                //主要作用是当背包切换动画从左往右切换时，先从第5排开始
                //而从右往左切换就是从第一排开始
                //这里的i % 5的取值始终是0,1,2,3,4，里面的2f就是这里取值的中间值：2
                //num其实就是+1和-1，
                float delay = (i % 5) * num + (1 - num) * 2f;
                //乘以0.02f就是减少每一排的切换间隔
                tween.SetDelay(delay * 0.02f);
            }
            for (int i = _newitemList.Count - 1; i >= 0; i--)
            {
                GameObject item = _newitemList[i].gameObject;
                Tweener tween = item.transform.DOLocalMoveX(offset, 0.5f);
                tween.SetRelative();
                tween.SetEase(Ease.InOutCubic);

                tween.OnComplete(OnTweenComplete);

                //下面这句跟上面的一样↑
                float delay = (i % 5) * num + (1 - num) * 2f;
                //+5是因为后面这一页动画要在前一页开始后才开始，乘以0.02f就是减少每一排的切换间隔
                tween.SetDelay((delay + 5) * 0.02f);
            }
            _nowPage = index;
            UpdateShow();
        }
    }

    private int index = 0;
    private void OnTweenComplete()
    {
        index++;
        //当index累计到30，也就是所有缓动完成后证明当次背包切换完成
        if (index == 30)
        {
            //用for循环遍历删除元素的时候，要从后往前删，不然会出错，有疑问的童鞋可以尝试一下从前往后删
            for (int i = _olditemList.Count - 1; i >= 0; i--)
            {
                GameTools.Destroy(_olditemList[i].gameObject);
            }
            //当然，也可以用下面的这个办法删除所有元素
            //while (_olditemList.Count > 0)
            //{
            //    GameTools.Destroy(_olditemList[0].gameObject);
            //    _olditemList.RemoveAt(0);
            //}
            _olditemList.Clear();
            index = 0;
            _isPlaying = false;
        }
    }

    void Start()
    {
        _newitemList = new List<BagItem>();
        _dataDict = new Dictionary<int, DataItem>();
        CreateItemGrid(0);
        UpdateShow();
    }

    public void UpdateShow()
    {
        switch (_nowPage)
        {
            case 0:
                _dataDict = GameData.BagData.ConsumableDict;
                break;
            case 1:
                _dataDict = GameData.BagData.MaterialDict;
                break;
            case 2:
                _dataDict = GameData.BagData.EquipmentDict;
                break;
        }
        for (int i = 0; i < 30; i++)
        {
            if (_dataDict.ContainsKey(i))
            {
                _newitemList[i].SetInfo(_dataDict[i]);
            }
            else
            {
                _newitemList[i].SetInfo(null);
            }
        }
    }

    private void CreateItemGrid(int offset)
    {
        _newitemList.Clear();
        int index = 0;
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject go = GameTools.AddChild(itemPanel, itemPrefab);
                go.name = "item " + (index < 10 ? "0" + index : index.ToString());
                go.transform.localPosition = new Vector3(j * 60 - 120 + offset, 150 - i * 60, 0);
                _newitemList.Add(go.GetComponent<BagItem>());
                index++;
            }
        }
    }

    public void OnCloseWindow()
    {
        WindowManager.GetInstance().CloseWindow(Window.BagWin);
    }

    public BagController Controller
    {
        set { _controller = value; }
    }
}
