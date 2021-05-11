using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IWorkProcess
{
    protected AnimationSystem animation;
    public bool interupted;
    public int priority;

    public IWorkProcess(Transform transform, int priority)
    {
        animation = transform.GetComponent<AnimationSystem>();
        this.priority = priority;
    }
    public IWorkProcess(Transform transform)
    {
        animation = transform.GetComponent<AnimationSystem>();
        this.priority = 0;
    }
    public virtual void Work(OnWorkDone done) {
        interupted = false;
    }

    public virtual void Stop(OnWorkDone done)
    {
        interupted = true;
    }

}
