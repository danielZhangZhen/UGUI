using UnityEngine;

public class ControllerBase : IController
{
    protected string _windowPath;
    protected GameObject _mono_object;
    protected MonoBase _mono;

    virtual public bool Create()
    {
        if (_mono_object != null) return false;
        Init();
        return true;
    }

    virtual protected void Init()
    {
        _mono_object = MonoBehaviour.Instantiate(Resources.Load(_windowPath), Vector3.zero, Quaternion.identity) as GameObject;
        _mono_object.name = _windowPath.Substring(_windowPath.LastIndexOf("/") + 1);
        _mono = _mono_object.GetComponent<MonoBase>();
        if (_mono != null) _mono.SetDepth();
    }

    virtual public void Destroy()
    {
        GameObject.Destroy(_mono_object);
        _mono_object = null;
    }

    public GameObject MonoObject
    {
        get { return _mono_object; }
    }
}