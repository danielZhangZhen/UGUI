using UnityEngine;
using UnityEngine.EventSystems;

public class RotationModel : MonoBehaviour, IDragHandler
{
    public Transform target;
    public float speed = 1f;

    void Start()
    {
        if (target == null) target = transform;
    }

    public void OnDrag(PointerEventData eventData)
    {
        target.localRotation = Quaternion.Euler(0f, -0.5f * eventData.delta.x * speed, 0f) * target.localRotation;
    }
}