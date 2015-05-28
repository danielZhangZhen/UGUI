//-------------------------------
//该Demo由风冻冰痕所写
//http://icemark.cn/blog
//转载请说明出处
//-------------------------------
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Image iconSprite;
    public Text textCount;
    public bool dragOnSurfaces = true;

    private int _bagType;
    private int _index;

    private GameObject m_DraggingIcon;
    private RectTransform m_DraggingPlane;

    public void SetInfo(int bagType, int index, DataItem item)
    {
        _bagType = bagType;
        _index = index;
        iconSprite.gameObject.SetActive(item != null);
        textCount.gameObject.SetActive(item != null);
        if (item == null) return;
        IconTools.SetIcon(iconSprite, item.Icon);
        textCount.text = item.Count.ToString();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var canvas = FindInParents<Canvas>(gameObject);
        if (canvas == null || !hasData) return;

        // We have clicked something that can be dragged.
        // What we want to do is create an icon for this.
        m_DraggingIcon = new GameObject("icon");

        m_DraggingIcon.transform.SetParent(canvas.transform, false);
        m_DraggingIcon.transform.SetAsLastSibling();

        var image = m_DraggingIcon.AddComponent<Image>();
        // The icon will be under the cursor.
        // We want it to be ignored by the event system.
        m_DraggingIcon.AddComponent<IgnoreRaycast>();
        image.sprite = iconSprite.sprite;
        //image.SetNativeSize();
        image.rectTransform.sizeDelta = new Vector2(54f, 54f);

        if (dragOnSurfaces) m_DraggingPlane = transform as RectTransform;
        else m_DraggingPlane = canvas.transform as RectTransform;

        SetDraggedPosition(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (m_DraggingIcon != null) SetDraggedPosition(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (m_DraggingIcon != null) Destroy(m_DraggingIcon);
    }

    public void OnDrop(PointerEventData eventData)
    {
        BagItem itemFrom = eventData.pointerDrag.GetComponent<BagItem>();
        if (!itemFrom.hasData || itemFrom.BagType != _bagType) return;
        //告诉数据中心我们要拖动类型为_bagType的背包，从itemFrom.Index拖动到_index
        GameData.BagData.DragItem(_bagType, itemFrom.Index, _index);
    }

    private void SetDraggedPosition(PointerEventData data)
    {
        if (dragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
            m_DraggingPlane = data.pointerEnter.transform as RectTransform;

        var rt = m_DraggingIcon.GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out globalMousePos))
        {
            rt.position = globalMousePos;
            rt.rotation = m_DraggingPlane.rotation;
        }
    }

    public static T FindInParents<T>(GameObject go) where T : Component
    {
        if (go == null) return null;
        var comp = go.GetComponent<T>();

        if (comp != null)
            return comp;

        Transform t = go.transform.parent;
        while (t != null && comp == null)
        {
            comp = t.gameObject.GetComponent<T>();
            t = t.parent;
        }
        return comp;
    }

    public bool hasData { get { return iconSprite.gameObject.activeInHierarchy; } }

    public int BagType { get { return _bagType; } }

    public int Index { get { return _index; } }
}