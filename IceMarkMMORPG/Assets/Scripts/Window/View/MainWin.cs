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

    public InputField inputField;

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

    public void OnGMCommand(string msg)
    {
        if (msg.StartsWith("item"))
        {
            string[] arr = msg.Split(' ');
            int id, count;
            if (arr.Length == 3 && int.TryParse(arr[1], out id) && int.TryParse(arr[2], out count))
            {
                GameData.BagData.AddItemToBag(id, count);
            }
            else
            {
                Debug.LogError("格式错误，应为：item ID 数量");
            }
        }
        else
        {
            Debug.LogError("格式错误，应为：item ID 数量");
        }
    }

    public MainController Controller
    {
        set { _controller = value; }
    }
}
