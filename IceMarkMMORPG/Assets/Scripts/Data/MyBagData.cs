//-------------------------------
//该Demo由风冻冰痕所写
//http://icemark.cn/blog
//转载请说明出处
//-------------------------------
using System.Collections.Generic;

public class MyBagData
{
    private Dictionary<int, DataItem> _consumableDict;
    private Dictionary<int, DataItem> _materialDict;
    private Dictionary<int, DataItem> _equipmentDict;

    public MyBagData()
    {
        //呃，这里只是手动把每个背包里面的数据加进来
        //实际项目中肯定是通过与服务端通信获取
        _consumableDict = new Dictionary<int, DataItem>();
        _consumableDict.Add(0, new DataItem(10001));
        _consumableDict.Add(1, new DataItem(10002));
        _consumableDict.Add(2, new DataItem(10003));

        _materialDict = new Dictionary<int, DataItem>();
        _materialDict.Add(0, new DataItem(20001));
        _materialDict.Add(1, new DataItem(20002));
        _materialDict.Add(2, new DataItem(20003));

        _equipmentDict = new Dictionary<int, DataItem>();
        _equipmentDict.Add(0, new DataItem(30001));
        _equipmentDict.Add(1, new DataItem(30002));
        _equipmentDict.Add(2, new DataItem(30003));

    }

    public Dictionary<int, DataItem> ConsumableDict { get { return _consumableDict; } }

    public Dictionary<int, DataItem> MaterialDict { get { return _materialDict; } }

    public Dictionary<int, DataItem> EquipmentDict { get { return _equipmentDict; } }
}