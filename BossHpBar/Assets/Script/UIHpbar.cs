//-------------------------------
//该Demo由风冻冰痕所写
//http://icemark.cn/blog
//转载请说明出处
//-------------------------------
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("IceMark/UIHpbar")]
public class UIHpbar : MonoBehaviour
{
    enum Type { Top, Bottom }
    private Color32[] _topColors = { new Color32(181, 30, 16, 255), new Color32(248, 159, 40, 255), new Color32(7, 183, 74, 255), new Color32(1, 164, 231, 255) };

    public Image imageTop;              //主血条
    public Image imageLight;            //血条减少时的闪光
    public Image imageBottom;           //血条减少的递减动画
    public Image imageNext;             //下一条血条
    public Text textCount;              //血条数量显示

    private float _nowValue;            //相对于总血量的当前血量
    private List<float> _maxHpList;     //记录每一条血条所容纳的血量
    private int _index = int.MaxValue;  //血条的实时索引，会跟随血条动画而改变
    private int _currentIndex;          //血条的索引，当前处于第几条
    private float _currentValue;        //血条的血量，只表示当前血条的血量
    private int _colorOffset = 40;      //颜色偏移 [Color > 0 : 变暗][Color < 0 : 变亮]

    //测试代码，实际项目中由外部调用-----------------------------------------------
    void Start()
    {
        /*
         * 这里为了测试，所以在Start里面直接调用
         * 实际项目里面肯定是在BOSS出现的时候调用Init
         * 然后把BOSS血量和血条总数量传进来
         * 增加血条数量，记得给 _topColors 添加血条颜色
         */
        Init(10000, 4);
    }

    void Update()
    {
        //测试，点击鼠标就扣血了
        if (Input.GetMouseButtonDown(0))
        {
            //随机扣血，看起来比较像战斗的伤害输出
            float value = Random.Range(100, 500);
            SetValue(_nowValue > value ? _nowValue - value : 0);
        }
    }
    //测试代码，实际项目中由外部调用-----------------------------------------------

    /// <summary>
    /// 初始化血条数据
    /// </summary>
    /// <param name="maxValue">血条血量</param>
    /// <param name="count">血条数量</param>
    public void Init(float maxValue, int count)
    {
        _nowValue = maxValue;
        _maxHpList = new List<float>();
        float step = Mathf.CeilToInt(maxValue / count);
        float value = maxValue;
        for (int i = 0; i < count; i++)
        {
            _maxHpList.Add(value > step ? step : value);
            value -= step;
        }
        _currentIndex = count - 1;
        _currentValue = _maxHpList[_currentIndex];
        SetIndex(count - 1);
    }

    /// <summary>
    /// 设置血量
    /// </summary>
    /// <param name="nowValue">血量</param>
    public void SetValue(float nowValue)
    {
        float val = _nowValue;

        float cValue = nowValue;
        for (int i = 0; i < _maxHpList.Count; i++)
        {
            if (cValue > _maxHpList[i])
            {
                cValue -= _maxHpList[i];
            }
            else
            {
                _currentIndex = i;
                _currentValue = cValue;
                break;
            }
        }

        UpdateTopHpBar();

        //这两句是扣血的瞬间那个闪光效果
        imageLight.color = Color.white;
        imageLight.DOFade(0, 0.2f);

        DOTween.To(() => val, x => val = x, nowValue, 0.5f).OnUpdate(() =>
        {
            _nowValue = val;
            float value = val;
            for (int i = 0; i < _maxHpList.Count; i++)
            {
                if (value > _maxHpList[i])
                {
                    value -= _maxHpList[i];
                }
                else
                {
                    UpdateHpBar(i, value);
                    break;
                }
            }
        });
    }

    private void UpdateHpBar(int index, float nValue)
    {
        float value = nValue / _maxHpList[index];
        imageBottom.fillAmount = value;
        imageLight.fillAmount = value;
        if (_index != value) SetIndex(index);
    }

    private void SetIndex(int value)
    {
        _index = value;
        UpdateTopHpBar();
        imageBottom.color = GetColorByIndex(_index, Type.Bottom);

        //如果当前血条Index大于0，也就是底下还有血条，就应该把底下的血条显示出来
        if (_index > 0)
        {
            imageNext.color = GetColorByIndex(_index - 1, Type.Top);
            imageNext.fillAmount = 1;
        }
        else imageNext.fillAmount = 0;
        //更新血条数量显示
        textCount.text = "×" + (_index + 1);
    }

    /// <summary>
    /// 更新最顶上的血条
    /// </summary>
    private void UpdateTopHpBar()
    {
        //这里判断_index == _currentIndex主要是因为扣血跨血条的时候
        //必须等到血条扣到跟当前血条相同Index的时候才显示当前值
        if (_index == _currentIndex)
        {
            imageTop.fillAmount = _currentValue / _maxHpList[_currentIndex];
            imageTop.color = GetColorByIndex(_index, Type.Top);
        }
        else imageTop.fillAmount = 0;

        imageBottom.color = GetColorByIndex(_index, Type.Bottom);

    }

    /// <summary>
    /// 根据血条Index获取血条颜色
    /// </summary>
    /// <param name="index">Index[0:第一条][1:第二条]</param>
    /// <returns>颜色</returns>
    private Color GetColorByIndex(int index, Type type)
    {
        switch (type)
        {
            case Type.Top:
                return _topColors[index];
            case Type.Bottom:
                Color32 color = _topColors[index];
                return new Color((color.r - _colorOffset) / 255f, (color.g - _colorOffset) / 255f, (color.b - _colorOffset) / 255f, 1f);
            default:
                return Color.white;
        }
    }
}