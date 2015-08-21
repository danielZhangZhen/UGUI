public class MainController : ControllerBase
{
    //private MainWin _win;

    public MainController()
    {
        _windowPath = Window.MainWin;
    }

    override protected void Init()
    {
        base.Init();
        //_win = _mono as MainWin;
        //_win.Controller = this;
    }

    override public void Destroy()
    {
        //_win.Controller = this;
        base.Destroy();
    }
}