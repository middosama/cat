using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueWorker : MonoBehaviour
{
    List<IWorkProcess> workQueue;
    public IWorkProcess working;
    public Camera cam;
    public Object isWorking;

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
    }
    public void PushWork(IWorkProcess work)
    {
        workQueue.Add(work);
        NextWork();
    }
    void NextWork()
    {

        if (working == null && workQueue.Count > 0)
        {
            working = workQueue[0];
            working.Work(() =>
            {
                Debug.Log("done");
                workQueue.Remove(working);
                working = null;
                NextWork();
            });
        }


    }

}
