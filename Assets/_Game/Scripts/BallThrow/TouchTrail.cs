using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTrail : MonoBehaviour
{
    public float distanceFromCarema = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Input.simulateMouseWithTouches = true;
    }

    // Update is called once per frame
    void Update()
    {
        TouchControll();
    }
    void TouchControll()
    {
        if (Input.touchCount <= 0) return;
        Touch touch = Input.GetTouch(0);
        Vector2 mousePos = Input.mousePosition;
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x,mousePos.y,distanceFromCarema));
    }
}
