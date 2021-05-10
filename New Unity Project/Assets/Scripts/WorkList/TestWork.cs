using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWork : IWorkProcess
{
    public Rigidbody2D rb;
    public Follower follower;
    public Vector2 pos;
    public TestWork(Transform transform, Vector2 pos) :base(transform)
    {
        rb = transform.GetComponent<Rigidbody2D>();
        follower = transform.GetComponent<Follower>();
        this.pos = pos;
    }


    public override void Work( OnWorkDone done)
    {
        Debug.Log("testWork"+ pos);

        follower.StartPath(pos, ()=>{
            animation.AnimTest(() => done());
        });
    }




}
