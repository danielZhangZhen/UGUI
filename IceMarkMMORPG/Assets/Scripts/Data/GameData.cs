public class GameData
{
    private static bool _isStart = false;
    public static MyBagData BagData;

    public static void InitData()
    {
        if (_isStart) return;

        //初始化数据
        BagData = new MyBagData();

        _isStart = true;
    }
}