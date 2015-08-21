using System.Collections.Generic;
public class ShopController : ControllerBase
{
    //private ShopWin _win;

    public ShopController()
    {
        _windowPath = Window.ShopWin;
    }

    override protected void Init()
    {
        base.Init();
        //_win = _mono as ShopWin;
        //_win.Controller = this;
    }

    override public void Destroy()
    {
        base.Destroy();
        //_win.Controller = null;
        //_win = null;
    }
}