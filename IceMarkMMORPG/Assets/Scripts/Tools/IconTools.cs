//-------------------------------
//该Demo由风冻冰痕所写
//http://icemark.cn/blog
//转载请说明出处
//-------------------------------
using UnityEngine;
using UnityEngine.UI;

public class IconTools
{
    public static void SetIcon(Image img, string iconName)
    {
        string path = "";
        if (iconName.StartsWith("Item"))
        {
            path = "Texture/Item/";
        }
        img.sprite = Resources.Load<Sprite>(path + iconName);
    }
}