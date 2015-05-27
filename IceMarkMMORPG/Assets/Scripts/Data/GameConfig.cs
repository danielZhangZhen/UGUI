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
        _items = new Dictionary<object, _unit_of_items>(1537);
        _items.Add(10001, new _unit_of_items(10001, "Item10001", "消耗A", 1));
        _items.Add(10002, new _unit_of_items(10002, "Item10002", "消耗B", 1));
        _items.Add(10003, new _unit_of_items(10003, "Item10003", "消耗C", 1));

        _items.Add(20001, new _unit_of_items(20001, "Item20001", "材料A", 2));
        _items.Add(20002, new _unit_of_items(20002, "Item20002", "材料B", 2));
        _items.Add(20003, new _unit_of_items(20003, "Item20003", "材料C", 2));

        _items.Add(30001, new _unit_of_items(30001, "Item30001", "装备A", 3));
        _items.Add(30002, new _unit_of_items(30002, "Item30002", "装备B", 3));
        _items.Add(30003, new _unit_of_items(30003, "Item30003", "装备C", 3));
    }
}

public struct _unit_of_items
{
    public int id;
    public string icon;
    public string name;
    public int type;

    public _unit_of_items(int _id, string _icon, string _name, int _type)
    {
        id = _id;
        icon = _icon;
        name = _name;
        type = _type;
    }
}
