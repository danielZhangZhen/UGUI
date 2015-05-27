//-------------------------------
//该Demo由风冻冰痕所写
//http://icemark.cn/blog
//转载请说明出处
//-------------------------------
using UnityEngine;
using UnityEngine.UI;

public class BagItem : MonoBehaviour
{
    public Image _iconSprite;

    public void SetInfo(DataItem item)
    {
        _iconSprite.gameObject.SetActive(item != null);
        if (item == null) return;
        IconTools.SetIcon(_iconSprite, item.Icon);
    }
}