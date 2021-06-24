using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotator : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float rotationSpeed = 200f;

    private Vector2 downPressPos;
    private Vector2 upPressPos;
    private Vector2 currentSwipe;

    private void Update()
    {
        Swipe();

        if (transform.rotation != target.transform.rotation)
        {
            float rotationStep = rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target.transform.rotation, rotationStep);
        }
    }

    private void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // get the 2d screen position where the mouse/finger is pressed
            downPressPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            // get the 2d screen position where the mouse/finger is released
            upPressPos = Input.mousePosition;

            // get the direction of the swipe
            currentSwipe = (upPressPos - downPressPos).normalized;

            if (IsLeftSwipe())
            {
                Debug.Log("Swipe Left");
                target.transform.Rotate(0, 90, 0, Space.World);
            }
            else if (IsRightSwipe())
            {
                Debug.Log("Swipe Right");
                target.transform.Rotate(0, -90, 0, Space.World);
            }
            else if (IsUpLeftSwipe())
            {
                Debug.Log("Swipe Up Left");
                target.transform.Rotate(90, 0, 0, Space.World);
            }
            else if (IsUpRightSwipe())
            {
                Debug.Log("Swipe Up Right");
                target.transform.Rotate(0, 0, -90, Space.World);
            }
            else if (IsDownLeftSwipe())
            {
                Debug.Log("Swipe Down Left");
                target.transform.Rotate(0, 0, 90, Space.World);
            }
            else if (IsDownRightSwipe())
            {
                Debug.Log("Swipe Down Right");
                target.transform.Rotate(-90, 0, 0, Space.World);
            }
        }
    }

    private bool IsLeftSwipe()
    {
        return currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f;
    }

    private bool IsRightSwipe()
    {
        return currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f;
    }

    private bool IsUpLeftSwipe()
    {
        return currentSwipe.y > 0 && currentSwipe.x < 0;
    }

    private bool IsUpRightSwipe()
    {
        return currentSwipe.y > 0 && currentSwipe.x > 0;
    }

    private bool IsDownLeftSwipe()
    {
        return currentSwipe.y < 0 && currentSwipe.x < 0;
    }

    private bool IsDownRightSwipe()
    {
        return currentSwipe.y < 0 && currentSwipe.x > 0;
    }
}
