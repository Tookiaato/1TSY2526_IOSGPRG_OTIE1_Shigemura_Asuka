using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Slash : MonoBehaviour
{
    public float slashDirection;

    private Vector3 firstPos;
    private Vector3 lastPos;
    private float swipeRange = 2;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;

            if (touch.phase == TouchPhase.Began)
            {
                firstPos = touchPosition;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                lastPos = touchPosition;
                float swipeDistance = Vector3.Distance(firstPos, lastPos);

                Vector2 distance = lastPos - firstPos;

                float distanceX = Mathf.Abs(distance.x);
                float distanceY = Mathf.Abs(distance.y);

                if (firstPos.y > lastPos.y && distanceY > distanceX && swipeDistance > swipeRange)
                {
                    slashDirection = 1;
                    Debug.Log("Swipe down");
                }
                else if (firstPos.y < lastPos.y && distanceY > distanceX  && swipeDistance > swipeRange)
                {
                    slashDirection = 2;
                    Debug.Log("Swipe up");
                }
                else if (firstPos.x < lastPos.x && distanceX > distanceY && swipeDistance > swipeRange)
                {
                    slashDirection = 3;
                    Debug.Log("Swipe right");
                }
                else if (firstPos.x > lastPos.x && distanceX > distanceY && swipeDistance > swipeRange)
                {
                    slashDirection = 4;
                    Debug.Log("Swipe left");
                }
            }
        }
    }
}
