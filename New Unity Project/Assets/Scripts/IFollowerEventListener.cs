using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFollowerEventListener
{
    void OnEndedPath(Vector2 targetPosition);
}
