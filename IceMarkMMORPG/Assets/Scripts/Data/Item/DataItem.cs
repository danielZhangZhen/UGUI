//-------------------------------
//该Demo由风冻冰痕所写
//http://icemark.cn/blog
//转载请说明出处
//-------------------------------
using System;
using UnityEngine;

public class DataItem : IComparable
{
    //这里为了贪图方便，省略了一部分数据，比如标志唯一性的Sid
    private _unit_of_items _data;
    private int _id;
    private int _count;

    public DataItem(int id, int count)
    {
        if (!GameConfig.items.ContainsKey(id))
        {
            Debug.LogError("ID为" + id + "的物品不存在！");
            return;
        }
        _id = id;
        _count = count;
        _data = GameConfig.items[id];
        for (int i = 0; i < 15; i++)
        {

        }
    }

    public int CompareTo(object obj)
    {
        int result = 1;
        DataItem data = (DataItem)obj;
        result = _id.CompareTo(data.Id);
        if (result != 0) return result;
        return data.Count.CompareTo(_count);
    }

    public void AddCount(int count)
    {
        _count += count;
    }

    public int Id { get { return _data.id; } }

    public string Icon { get { return _data.icon; } }

    public string Name { get { return _data.name; } }

    public int Type { get { return _data.type; } }

    public int Count { get { return _count; } }

    public int Weight { get { return _data.weight; } }
}