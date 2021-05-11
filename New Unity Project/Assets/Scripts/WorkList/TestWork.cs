using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWork : IWorkProcess
{
    public Rigidbody2D rb;
    public Follower follower;
    public Vector2 pos;
    public TestWork(Transform transform, Vector2 pos,int priority) :base(transform,priority)
    {
        rb = transform.GetComponent<Rigidbody2D>();
        follower = transform.GetComponent<Follower>();
        this.pos = pos;
        this.priority = priority;
    }
    public TestWork(Transform transform, Vector2 pos) : base(transform)
    {
        rb = transform.GetComponent<Rigidbody2D>();
        follower = transform.GetComponent<Follower>();
        this.pos = pos;
    }

    public override void Stop(OnWorkDone done)
    {
        base.Stop(done);
        Debug.Log("testWork");
        follower.StopPath();
    }

    public override void Work( OnWorkDone done)
    {
        base.Work(done);
        Debug.Log("testWork"+ pos);

        follower.StartPath(pos, ()=>{
            if(!interupted)
            animation.AnimTest(() => done());
        });
    }




}
