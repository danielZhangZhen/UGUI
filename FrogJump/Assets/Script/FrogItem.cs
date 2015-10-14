using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class FrogItem : MonoBehaviour, IComparable, IPointerClickHandler
{
    [Serializable]
    public class FrogClickedEvent : UnityEvent<FrogItem> { }

    // Event delegates triggered on click.
    [FormerlySerializedAs("onClick")]
    [SerializeField]
    private FrogClickedEvent m_OnClick = new FrogClickedEvent();
    public FrogClickedEvent onClick
    {
        get { return m_OnClick; }
        set { m_OnClick = value; }
    }

    private Transform _myTransform;
    private Image _icon;
    private FrogType _type;
    void Awake()
    {
        _myTransform = transform;
        _icon = _myTransform.Find("Icon").GetComponent<Image>();
    }

    public void SetInfo(FrogType type)
    {
        _type = type;
        _icon.gameObject.SetActive(type != FrogType.Center);
        if (type != FrogType.Center)
        {
            _icon.sprite = Resources.Load<Sprite>(type.ToString());
        }
    }

    public int CompareTo(object obj)
    {
        FrogItem role = (FrogItem)obj;
        int result = this.transform.localPosition.x.CompareTo(role.transform.localPosition.x);
        return result;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        m_OnClick.Invoke(this);
    }

    public FrogType Type { get { return _type; } }
}
