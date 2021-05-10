using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IWorkProcess{
    protected AnimationSystem animation;

    public IWorkProcess(Transform transform)
    {
        animation = transform.GetComponent<AnimationSystem>();
    }
    public abstract void Work( OnWorkDone done);

}
