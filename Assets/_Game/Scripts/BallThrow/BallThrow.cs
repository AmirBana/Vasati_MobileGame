using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallThrow : MonoBehaviour
{


    void Start()
    {
        
    }

    void Update()
    {
        if (Input.touchCount > 0)
            GetTouch();
    }
    void GetTouch()
    {
        var touch = Input.GetTouch(0);

    }

}
