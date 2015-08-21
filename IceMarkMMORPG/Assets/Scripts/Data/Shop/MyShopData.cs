//-------------------------------
//该Demo由风冻冰痕所写
//http://icemark.cn/blog
//转载请说明出处
//-------------------------------
using System.Collections.Generic;

public class MyShopData
{
    private List<DataShop> _shopList;

    public MyShopData()
    {
        _shopList = new List<DataShop>();
        _shopList.Add(new DataShop(10001, 1));
        _shopList.Add(new DataShop(10002, 1));
        _shopList.Add(new DataShop(10003, 1));
        _shopList.Add(new DataShop(20001, 1));
        _shopList.Add(new DataShop(20002, 1));
        _shopList.Add(new DataShop(20003, 1));
        _shopList.Add(new DataShop(30001, 1));
        _shopList.Add(new DataShop(30002, 1));
        _shopList.Add(new DataShop(30003, 1));
    }

    public List<DataShop> ShopList { get { return _shopList; } }
}