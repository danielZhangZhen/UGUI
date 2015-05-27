//-------------------------------
//该Demo由风冻冰痕所写
//http://icemark.cn/blog
//转载请说明出处
//-------------------------------
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;

public class MainWin : MonoBase
{
    private MainController _controller;

    void Start()
    {
    }

    public void OnMenuBarClick(string name)
    {
        switch (name)
        {
            case "Bag":
                WindowManager.GetInstance().OpenWindow(Window.BagWin);
                break;
        }
    }

    public MainController Controller
    {
        set { _controller = value; }
    }
}
