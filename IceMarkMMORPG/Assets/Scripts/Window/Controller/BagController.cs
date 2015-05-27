public class BagController : ControllerBase
{
    private BagWin _win;

    public BagController()
    {
        _windowPath = Window.BagWin;
    }

    override protected void Init()
    {
        base.Init();
        _win = _mono as BagWin;
        _win.Controller = this;
    }

    override public void Destroy()
    {
        base.Destroy();
    }
}