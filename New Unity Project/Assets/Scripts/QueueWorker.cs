
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueWorker : MonoBehaviour
{
    List<IWorkProcess> workQueue;
    public IWorkProcess working;
    public Camera cam;
    public Object isWorking;
    public readonly System.Comparison<IWorkProcess> SORT_BY_PRIORITY = (x, y) => { return y.priority - x.priority; };

    // Start is called before the first frame update
    void Start()
    {
        workQueue = new List<IWorkProcess>();
        isWorking = new Object();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PushWork(new TestWork(transform, cam.ScreenToWorldPoint(Input.mousePosition)));
            Debug.Log("push");
            Debug.DrawLine(transform.position, cam.ScreenToWorldPoint(Input.mousePosition));

        }
        if (Input.GetMouseButtonDown(1))
        {
            PushWork(new TestWork(transform, cam.ScreenToWorldPoint(Input.mousePosition),2));
        }
        if (Input.GetMouseButtonDown(2))
        {
            Debug.Log("continue");
            NextWork();

        }
    }
    public void PushWork(IWorkProcess work)
    {
        workQueue.Add(work);
        SortWork();
        if(working != null && (work.priority > working.priority))
        {
            WorkInterrupt();
        }
        NextWork();
    }
    public void WorkInterrupt()
    {
        working.Stop(() => { });
        working = null;
    }

    public void WorkContinue()
    {
        NextWork();
    }
    void NextWork()
    {
        if (working == null && workQueue.Count > 0)
        {
            working = workQueue[0];
            working.Work(() =>
            {
                Debug.Log(working.interupted);
                if (working.interupted)
                {
                }
                else
                {
                    Debug.Log("done");
                    workQueue.Remove(working);
                    working = null;
                    NextWork();
                }
            });
        }
    }
    void SortWork()
    {
        workQueue.Sort(SORT_BY_PRIORITY);
    }

}
