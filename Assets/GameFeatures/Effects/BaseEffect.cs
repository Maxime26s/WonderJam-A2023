using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEffect : MonoBehaviour
{
    [SerializeField]
    bool _isOver = false;

    public bool isOver { get => _isOver; set => _isOver = value; }

    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    protected virtual void OnEnable()
    {
        BeatController.Instance.OnBeatEvent += Tick;
    }

    protected virtual void OnDisable()
    {
        BeatController.Instance.OnBeatEvent -= Tick;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public abstract void Tick();
}
