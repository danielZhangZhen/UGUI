//-------------------------------
//该Demo由风冻冰痕所写
//http://icemark.cn/blog
//转载请说明出处
//-------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMono : MonoBehaviour
{
    void Awake()
    {
        //设置游戏品质
        QualitySettings.SetQualityLevel(3);
        //设置游戏分辨率
        Screen.SetResolution(1280, 800, false);
        //初始化数据
        ControllerManager.Init();
        GameData.InitData();
    }

    void Start()
    {
        WindowManager.GetInstance().OpenWindow(Window.MainWin);
    }
}