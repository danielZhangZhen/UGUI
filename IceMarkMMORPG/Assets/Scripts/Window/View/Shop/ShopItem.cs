//-------------------------------
//该Demo由风冻冰痕所写
//http://icemark.cn/blog
//转载请说明出处
//-------------------------------
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : UIMultiScrollIndex
{
    public Image iconSprite;
    public Text textName;
    public CountSelector countSelector;

    private DataShop _item;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void UpdateInfo(int index)
    {
        base.UpdateInfo(index);
        SetInfo(GameData.ShopData.ShopList[index]);
    }

    public void SetInfo(DataShop item)
    {
        _item = item;
        IconTools.SetIcon(iconSprite, item.Icon);
        textName.text = item.Name;
    }

    public void OnBuy()
    {
        GameData.BagData.AddItemToBag(_item.Id, countSelector.Count);
    }
}
