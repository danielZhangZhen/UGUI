using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Camera _2DCamera;
    private Transform _myTransform;
    private Transform _transBorder;
    private Transform _transButton;
    private bool _isDraging = false;      //是否正在拖动摇杆
    private bool _fixedJoystick = true;  //是否锁定摇杆
    private float _radius = 90f;
    private Tweener _buttonTweener;

    void Awake()
    {
        _myTransform = transform;
        _2DCamera = _myTransform.parent.Find("Camera").GetComponent<Camera>();
        _transBorder = _myTransform.Find("Border");
        _transButton = _transBorder.Find("Button");
    }

    void Update()
    {
        if (_isDraging)
        {
            Debug.Log(_transButton.localPosition);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_transBorder as RectTransform, eventData.position, _2DCamera, out pos))
        {
            _transButton.localPosition = Vector2.ClampMagnitude(pos, _radius);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDraging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDraging = false;
        if (_transButton.localPosition != Vector3.zero)
        {
            _buttonTweener = _transButton.DOLocalMove(Vector3.zero, 0.3f).SetEase(Ease.OutBack);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_buttonTweener != null && _buttonTweener.IsPlaying()) _buttonTweener.Kill(false);
        if (_fixedJoystick)
        {
            OnDrag(eventData);
        }
        else
        {
            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_myTransform as RectTransform, eventData.position, _2DCamera, out pos))
            {
                _transBorder.localPosition = pos;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _transBorder.localPosition = Vector3.zero;
        OnEndDrag(eventData);
    }
}
