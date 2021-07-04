using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotRotation : MonoBehaviour
{
    [SerializeField] private CubeReader cubeReader;
    [SerializeField] private CubeState cubeState;
    
    private List<GameObject> activeSide;
    private Vector3 localForward;
    private Vector3 mouseRef;
    private bool dragging = false;
    private float sensitivity = 0.4f;
    private Vector3 rotation;

    void Update()
    {
        if (dragging)
        {
            SpinSide(activeSide);
            if (Input.GetMouseButtonUp(0))
            {
                dragging = false;
            }
        }
    }

    public void Rotate(List<GameObject> side)
    {
        activeSide = side;
        mouseRef = Input.mousePosition;
        dragging = true;

        // create a vector to rotate around
        localForward = Vector3.zero - side[4].transform.parent.transform.localPosition;
    }

    private void SpinSide(List<GameObject> side)
    {
        // reset the rotation
        rotation = Vector3.zero;

        // current mouse position minus the last mouse position
        Vector3 mouseOffset = (Input.mousePosition - mouseRef);

        if (side == cubeState.Front)
        {
            rotation.z = (mouseOffset.x + mouseOffset.y) * sensitivity * -1;
        }

        // rotate
        transform.Rotate(rotation, Space.Self);

        // store mouse
        mouseRef = Input.mousePosition;
    }
}
