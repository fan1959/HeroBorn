using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable<T> : MonoBehaviour where T:MonoBehaviour
{
    public float OnscreenDelay = 3f;
    private void Start()
    {
        Destroy(this.gameObject,OnscreenDelay);
    }
}
