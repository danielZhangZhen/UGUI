//-------------------------------
//该Demo由风冻冰痕所写
//http://icemark.cn/blog
//转载请说明出处
//-------------------------------
using UnityEngine;

public class UIMultiScrollIndex : MonoBehaviour
{
    private UIMultiScroller _scroller;
    private int _index;

    protected virtual void Awake() { }

    public int Index
    {
        get { return _index; }
        set
        {
            _index = value;
            transform.localPosition = _scroller.GetPosition(_index);
            gameObject.name = "Scroll" + (_index < 10 ? "0" + _index : _index.ToString());
            UpdateInfo(_index);
        }
    }

    public void UpdateIndex()
    {
        UpdateInfo(_index);
    }

    protected virtual void UpdateInfo(int index) { }

    public UIMultiScroller Scroller
    {
        set { _scroller = value; }
    }
}
