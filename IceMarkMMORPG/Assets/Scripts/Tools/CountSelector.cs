using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CountSelector : MonoBehaviour
{
    enum Type { Increase, Reduce }
    public InputField inputField;
    public GameObject buttonIncrease;
    public GameObject buttonReduce;

    public int minValue;
    public int maxValue;

    private int _count = 1;
    private bool _isLooping = false;
    private Type _type;

    void Awake()
    {
        EventTrigger trigger1 = buttonIncrease.AddComponent<EventTrigger>();
        trigger1.triggers = new List<EventTrigger.Entry>();

        EventTrigger.Entry entryDown1 = new EventTrigger.Entry();
        entryDown1.eventID = EventTriggerType.PointerDown;
        entryDown1.callback.AddListener(OnDown);
        trigger1.triggers.Add(entryDown1);

        EventTrigger.Entry entryUp1 = new EventTrigger.Entry();
        entryUp1.eventID = EventTriggerType.PointerUp;
        entryUp1.callback.AddListener(OnUp);
        trigger1.triggers.Add(entryUp1);

        EventTrigger trigger2 = buttonReduce.AddComponent<EventTrigger>();

        trigger2.triggers = new List<EventTrigger.Entry>();

        EventTrigger.Entry entryDown2 = new EventTrigger.Entry();
        entryDown2.eventID = EventTriggerType.PointerDown;
        entryDown2.callback.AddListener(OnDown);
        trigger2.triggers.Add(entryDown2);

        EventTrigger.Entry entryUp2 = new EventTrigger.Entry();
        entryUp2.eventID = EventTriggerType.PointerUp;
        entryUp2.callback.AddListener(OnUp);
        trigger2.triggers.Add(entryUp2);
    }

    private void OnDown(BaseEventData arg0)
    {
        switch (arg0.selectedObject.name)
        {
            case "ButtonIncrease":
                _type = Type.Increase;
                break;
            case "ButtonReduce":
                _type = Type.Reduce;
                break;
        }
        _isLooping = true;
        StartCoroutine(OnValueChange());
    }

    private void OnUp(BaseEventData arg0)
    {
        _isLooping = false;
    }

    private IEnumerator OnValueChange()
    {
        while (_isLooping)
        {
            switch (_type)
            {
                case Type.Increase:
                    if (_count < maxValue) _count++;
                    break;
                case Type.Reduce:
                    if (_count > minValue) _count--;
                    break;
            }
            inputField.text = _count.ToString();
            yield return new WaitForSeconds(0.15f);
        }
    }

    public int Count { get { return _count; } }
}