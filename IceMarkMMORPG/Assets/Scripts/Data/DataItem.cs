//这里为了贪图方便，省略了一部分数据，比如标志唯一性的Sid
using UnityEngine;

public class DataItem
{
    private _unit_of_items _data;

    public DataItem(int id)
    {
        if (!GameConfig.items.ContainsKey(id))
        {
            Debug.LogError("ID为" + id + "的物品不存在！");
            return;
        }
        _data = GameConfig.items[id];
    }

    public int Id { get { return _data.id; } }

    public string Icon { get { return _data.icon; } }

    public string Name { get { return _data.name; } }

    public int Type { get { return _data.type; } }
}