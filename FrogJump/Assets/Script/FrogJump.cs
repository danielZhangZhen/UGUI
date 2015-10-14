using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public enum FrogType { Left, Center, Right }

public class FrogJump : MonoBehaviour
{
    private int _middleIndex;
    private int _num = 7;
    private List<FrogItem> _btnList;

    void Awake()
    {
        _middleIndex = (_num - 1) / 2;
        _btnList = new List<FrogItem>();
        for (var i = 0; i < _num; i++)
        {
            GameObject go = GameTools.AddChild(transform, Resources.Load<GameObject>("FrogItem"));
            go.name = "FrogItem" + i;
            FrogItem item = go.GetComponent<FrogItem>();
            item.SetInfo(i < _middleIndex ? FrogType.Left : i > _middleIndex ? FrogType.Right : FrogType.Center);
            go.transform.localPosition = new Vector3(i * 102 - _middleIndex * 102, 0, 0);
            item.onClick.AddListener(OnFrogClick);
            _btnList.Add(item);
        }
    }

    private void OnFrogClick(FrogItem frog)
    {
        int pos = _btnList.IndexOf(frog);
        switch (frog.Type)
        {
            case FrogType.Left:
                if (_btnList[pos + 1].Type == FrogType.Center) SawpBtn(frog, _btnList[pos + 1]);
                else if (_btnList[pos + 2].Type == FrogType.Center) SawpBtn(frog, _btnList[pos + 2]);
                break;
            case FrogType.Right:
                if (_btnList[pos - 1].Type == FrogType.Center) SawpBtn(frog, _btnList[pos - 1]);
                else if (_btnList[pos - 2].Type == FrogType.Center) SawpBtn(frog, _btnList[pos - 2]);
                break;
        }
    }

    private void SawpBtn(FrogItem btn1, FrogItem btn2)
    {
        Vector3 tmpPos = btn1.transform.localPosition;
        btn1.transform.localPosition = btn2.transform.localPosition;
        btn2.transform.localPosition = tmpPos;
        _btnList.Sort();
    }
}