//-------------------------------
//该Demo由风冻冰痕所写
//http://icemark.cn/blog
//转载请说明出处
//-------------------------------
using System.Collections.Generic;

public class MyBagData
{
    public DataEvent.UpdateBagData OnUpdateBagData;

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

    /// <summary>
    /// 处理Item的拖动
    /// </summary>
    /// <param name="bagType">背包类型</param>
    /// <param name="indexFrom">原始Item</param>
    /// <param name="indexTo">目标Item</param>
    public void DragItem(int bagType, int indexFrom, int indexTo)
    {
        Dictionary<int, DataItem> itemDict = GetItemDictByBagType(bagType);
        //原Item存在，开始更新数据
        if (itemDict.ContainsKey(indexFrom))
        {
            //目标Item存在，交换数据
            if (itemDict.ContainsKey(indexTo))
            {
                DataItem temp = itemDict[indexFrom];
                itemDict[indexFrom] = itemDict[indexTo];
                itemDict[indexTo] = temp;
            }
            else    //否则就移动数据
            {
                itemDict[indexTo] = itemDict[indexFrom];
                itemDict.Remove(indexFrom);
            }
        }
        //通知背包窗口更新对应的Item
        if (OnUpdateBagData != null) OnUpdateBagData(bagType, indexFrom);
        if (OnUpdateBagData != null) OnUpdateBagData(bagType, indexTo);
    }

    /// <summary>
    /// 根据背包类型来获取Item的列表
    /// </summary>
    /// <param name="bagType">背包类型[0:消耗][1:材料][2:装备]</param>
    /// <returns></returns>
    public Dictionary<int, DataItem> GetItemDictByBagType(int bagType)
    {
        switch (bagType)
        {
            case 0: return _consumableDict;
            case 1: return _materialDict;
            case 2: return _equipmentDict;
        }
        return null;
    }
}