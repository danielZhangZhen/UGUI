//-------------------------------
//该Demo由风冻冰痕所写
//http://icemark.cn/blog
//转载请说明出处
//-------------------------------
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager
{
    private static Dictionary<string, ControllerBase> _components;
    public static void Init()
    {
        if (_components != null) _components.Clear();
        _components = new Dictionary<string, ControllerBase>();

        _components.Add(Window.MainWin, new MainController());
        _components.Add(Window.BagWin, new BagController());
    }

    public static ControllerBase GetControler(string name)
    {
        if (_components == null)
        {
            Debug.LogError("编辑器代码已修改，需重新运行游戏");
        }
        if (_components.ContainsKey(name))
        {
            return _components[name];
        }
        return null;
    }
}