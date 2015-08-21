//这个类是本地的数据配置，一般由策划负责管理的excel表格导出到这里
using System.Collections.Generic;

public class GameConfig
{
    private static Dictionary<object, _unit_of_items> _items = null;

    public static Dictionary<object, _unit_of_items> items
    {
        get
        {
            if (_items == null)
            {
                init_items();
            }
            return _items;
        }
    }

    private static void init_items()
    {
        _items = new Dictionary<object, _unit_of_items>();
        _items.Add(10001, new _unit_of_items(0, 10001, "Item10001", "消耗A", 1, 1));
        _items.Add(10002, new _unit_of_items(0, 10002, "Item10002", "消耗B", 1, 1));
        _items.Add(10003, new _unit_of_items(0, 10003, "Item10003", "消耗C", 1, 1));

        _items.Add(20001, new _unit_of_items(1, 20001, "Item20001", "材料A", 2, 1));
        _items.Add(20002, new _unit_of_items(1, 20002, "Item20002", "材料B", 2, 1));
        _items.Add(20003, new _unit_of_items(1, 20003, "Item20003", "材料C", 2, 1));

        _items.Add(30001, new _unit_of_items(2, 30001, "Item30001", "装备A", 3, 1));
        _items.Add(30002, new _unit_of_items(2, 30002, "Item30002", "装备B", 3, 1));
        _items.Add(30003, new _unit_of_items(3, 30003, "Item30003", "装备C", 3, 1));
    }
}

public struct _unit_of_items
{
    public int bagType; //背包类型
    public int id;      //物品ID
    public string icon; //物品图标
    public string name; //物品名称
    public int type;    //物品类型
    public int weight;  //物品重量

    public _unit_of_items(int _bagType, int _id, string _icon, string _name, int _type, int _weight)
    {
        bagType = _bagType;
        id = _id;
        icon = _icon;
        name = _name;
        type = _type;
        weight = _weight;
    }
}
