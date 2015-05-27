using System;

public class WindowManager
{
    private static WindowManager _instance;

    private WindowManager()
    {
        if (_instance != null)
        {
            throw new Exception("单件实例错误");
        }
        _instance = this;
    }

    public static WindowManager GetInstance()
    {
        if (_instance != null)
        {
            return _instance;
        }
        return new WindowManager();
    }

    /// <summary>
    /// 打开窗口
    /// </summary>
    public void OpenWindow(string name, System.Object[] param = null)
    {
        ControllerManager.GetControler(name).Create();
    }

    /// <summary>
    /// 关闭窗口
    /// </summary>
    public void CloseWindow(string name)
    {
        ControllerManager.GetControler(name).Destroy();
    }
}