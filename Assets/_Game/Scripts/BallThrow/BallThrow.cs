using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallThrow : MonoBehaviour
{
    float startTime, endTime, swipeDistance, swipeTime;
    private Vector2 startPos;
    private Vector2 endPos;

    public float MinSwipDist = 0;
    private float BallVelocity = 0;
    private float BallSpeed = 0;
    public float MaxBallSpeed = 350;
    private Vector3 angle;

    private bool thrown, holding;
    private Vector3 newPosition, resetPos;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        resetPos = transform.position;
        ResetBall();
    }

/*    private void OnMouseDown()
    {
        startTime = Time.time;
        startPos = Input.mousePosition;
        holding = true;
    }

    private void OnMouseDrag()
    {
        PickupBall();
    }

    private void OnMouseUp()
    {
        endTime = Time.time;
        endPos = Input.mousePosition;
        swipeDistance = (endPos - startPos).magnitude;
        swipeTime = endTime - startTime;

        if (swipeTime < 0.5f && swipeDistance > 30f)
        {
            //throw ball
            CalSpeed();
            CalAngle();
            rb.AddForce(new Vector3((angle.x * BallSpeed), (angle.y * BallSpeed / 3), (angle.z * BallSpeed) * 2));
            rb.useGravity = true;
            holding = false;
            thrown = true;
            Invoke("ResetBall", 4f);
        }
        else
            ResetBall();
    }*/
    //TouchFunctions
    void GetTouch()
    {
        if(Input.touchCount <= 0) return;
        Debug.Log("Touch started");
        startTime = Time.time;
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            startPos = touch.position;
            holding = true;
        }
        if(touch.phase == TouchPhase.Moved)
        {
            PickupBall(touch);
        }
        if (touch.phase == TouchPhase.Ended)
        {
            FinishTouch(touch);
        }
    }
    void FinishTouch(Touch touch)
    {
        endTime = Time.time;
        endPos = touch.position;
        Debug.Log(endPos);
        swipeDistance = (endPos - startPos).magnitude;
        Debug.Log("Swipre Dstance: "+ swipeDistance);
        swipeTime = endTime - startTime;

        if (swipeTime < 0.5f && swipeDistance > 30f)
        {
            //throw ball
            CalSpeed();
            CalAngle();
            rb.AddForce(new Vector3((angle.x * BallSpeed), (angle.y * BallSpeed / 3), (angle.z * BallSpeed) * 2));
            rb.useGravity = true;
            holding = false;
            thrown = true;
            Invoke("ResetBall", 4f);
        }
        else
            ResetBall();
    }
    void ResetBall()
    {
        angle = Vector3.zero;
        endPos = Vector2.zero;
        startPos = Vector2.zero;
        BallSpeed = 0;
        startTime = 0;
        endTime = 0;
        swipeDistance = 0;
        swipeTime = 0;
        thrown = holding = false;
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        transform.position = resetPos;
    }

    void PickupBall(Touch touch)
    {
        //mouse
        //Vector3 pos = Input.mousePosition;
        //touch
        Vector3 pos = touch.position;
        pos.z = Camera.main.nearClipPlane * 5f;
        newPosition = Camera.main.ScreenToWorldPoint(pos);
        //transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, 80f * Time.deltaTime);
    }
    private void Update()
    {
        GetTouch();
    }

    private void CalAngle()
    {
        angle = Camera.main.ScreenToWorldPoint(new Vector3(endPos.x, endPos.y + 50f, (Camera.main.nearClipPlane + 5)));
    }

    void CalSpeed()
    {
        if (swipeTime > 0)
            BallVelocity = swipeDistance / (swipeDistance - swipeTime);

        BallSpeed = BallVelocity * 40;

        if (BallSpeed <= MaxBallSpeed)
        {
            BallSpeed = MaxBallSpeed;
        }
        swipeTime = 0;
    }

}
