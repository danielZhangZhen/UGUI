using System.Collections.Generic;
//-------------------------------
//该Demo由风冻冰痕所写
//http://icemark.cn/blog
//转载请说明出处
//-------------------------------
using UnityEngine;

public class ShopWin : MonoBase
{
    //private ShopController _controller;

    public GameObject content;
    public GameObject itemPrefab;
    public UIMultiScroller scroller;

    void Start()
    {
        scroller.DataCount = GameData.ShopData.ShopList.Count;
        scroller.Begin();
    }

    /// <summary>
    /// 关闭窗口
    /// </summary>
    public void OnCloseWindow()
    {
        WindowManager.GetInstance().CloseWindow(Window.ShopWin);
    }

    //public ShopController Controller { set { _controller = value; } }
}