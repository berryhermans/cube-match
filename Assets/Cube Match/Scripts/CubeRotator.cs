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

    private int layerMask = 1 << 6;
    private bool isDragging = false;

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
            // raycast from the mouse towards the cube to see if a face is hit
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f, layerMask))
            {
                // we hit a face, a side has to be rotated instead of the cube, which is done elsewhere
                return;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            // get the 2d screen position where the mouse/finger is pressed
            downPressPos = Input.mousePosition;
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
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

            isDragging = false;
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
