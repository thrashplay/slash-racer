using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Automatically destroy this object when the player drives past it
public class AutoDestroy : MonoBehaviour
{
    // top of the segment, in world coordinates
    private float _top;

    void Start()
    {
        _top = transform.position.y + transform.localScale.y / 2;
    }

    void Update()
    {
        float viewBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
        if (_top < viewBottom) 
        {
            Destroy(gameObject, 0);
        }
    }
}
