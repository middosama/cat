using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Common;

public class Follower : MonoBehaviour
{
    private Vector2 targetPosition;
    public OnWorkDone doneEvent;

    public float speed = 100f;
    public float wayPointDistance = 1f;

    Path path;
    int currentPoint = 0;

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", .5f, .5f);
        targetPosition = rb.position;
        //TryGetComponent<IFollowerEventListener>(out bindingObject);
    }
    public void StartPath(Vector2 targetPosition, OnWorkDone doneEvent)
    {
        this.doneEvent = null;
        OnEndedPath();
        this.targetPosition = targetPosition;
        this.doneEvent = doneEvent;
    }
    public void StopPath()
    {
        OnEndedPath();
    }
    void UpdatePath()
    {
        if (seeker.IsDone() && targetPosition != rb.position)
        {
            seeker.StartPath(rb.position, targetPosition, OnPathComplete);
        }
        else
        {
            path = null;
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentPoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (path == null)
            return;
        Vector2 direction = (Vector2)path.vectorPath[currentPoint] - rb.position;
        if (direction.magnitude <= wayPointDistance)
        {
            currentPoint++;
            if (currentPoint >= path.vectorPath.Count)
            {
                OnEndedPath();
                return;
            }
            direction = (Vector2)path.vectorPath[currentPoint] - rb.position;
            rb.velocity = direction.normalized * speed;
            return;
        }

    }

    

    void OnEndedPath()
    {
        path = null;
        currentPoint = 0;
        rb.velocity = Vector2.zero;
        targetPosition = rb.position;
        if (doneEvent != null)
        {
            doneEvent();
        }
    }
}
