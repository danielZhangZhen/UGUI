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
        GameData.BagData.OnUpdateBagData += UpdateBagData;
    }

    private void UpdateBagData(int bagType, int index)
    {
        if (_win != null) _win.UpdateBagData(bagType, index);
    }

    override public void Destroy()
    {
        GameData.BagData.OnUpdateBagData -= UpdateBagData;
        base.Destroy();
        _win.Controller = null;
        _win = null;
    }
}