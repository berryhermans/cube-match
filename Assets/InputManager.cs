using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector2 downPressPos;
    private Vector2 upPressPos;
    private Vector2 currentSwipe;

    public delegate void SwipeAction();
    public static event SwipeAction OnSwipeLeft;
    public static event SwipeAction OnSwipeRight;
    public static event SwipeAction OnSwipeUpLeft;
    public static event SwipeAction OnSwipeUpRight;
    public static event SwipeAction OnSwipeDownLeft;
    public static event SwipeAction OnSwipeDownRight;

    private void Update()
    {
        Swipe();
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
                OnSwipeLeft?.Invoke();
            }
            else if (IsRightSwipe())
            {
                Debug.Log("Swipe Right");
                OnSwipeRight?.Invoke();
            }
            else if (IsUpLeftSwipe())
            {
                Debug.Log("Swipe Up Left");
                OnSwipeUpLeft?.Invoke();
            }
            else if (IsUpRightSwipe())
            {
                Debug.Log("Swipe Up Right");
                OnSwipeUpRight?.Invoke();
            }
            else if (IsDownLeftSwipe())
            {
                Debug.Log("Swipe Down Left");
                OnSwipeDownLeft?.Invoke();
            }
            else if (IsDownRightSwipe())
            {
                Debug.Log("Swipe Down Right");
                OnSwipeDownRight?.Invoke();
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
