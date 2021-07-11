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
    private Vector3 rotateRef;
    private bool isDragging = false;
    private bool isAutoRotating = false;
    private float sensitivity = 0.2f;
    private float speed = 300f;
    private Vector3 rotation;

    public static event UserAction OnUserRotation;
    public delegate void UserAction();

    private Quaternion targetQuaternion;

    void Update()
    {
        if (isDragging)
        {
            SpinSide(activeSide);
            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                RotateToRightAngle();
            }
        }
        if (isAutoRotating)
        {
            AutoRotate();
        }
    }

    public void Rotate(List<GameObject> side)
    {
        activeSide = side;
        mouseRef = Input.mousePosition;
        rotateRef = transform.rotation.eulerAngles;
        isDragging = true;

        // create a vector to rotate around
        localForward = Vector3.zero - side[4].transform.parent.transform.localPosition;
    }

    public void RotateToRightAngle()
    {
        Vector3 vec = transform.localEulerAngles;
        // round vec to nearest 90 degress
        vec.x = Mathf.Round(vec.x / 90) * 90;
        vec.y = Mathf.Round(vec.y / 90) * 90;
        vec.z = Mathf.Round(vec.z / 90) * 90;

        // is the target angle a different angle than at the start of the drag?
        if (rotateRef.normalized != vec.normalized)
        {
            OnUserRotation?.Invoke();
        }

        targetQuaternion.eulerAngles = vec;
        isAutoRotating = true;
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
        if (side == cubeState.Back)
        {
            rotation.z = (mouseOffset.x + mouseOffset.y) * sensitivity * 1;
        }
        if (side == cubeState.Up)
        {
            rotation.y = (mouseOffset.x + mouseOffset.y) * sensitivity * 1;
        }
        if (side == cubeState.Down)
        {
            rotation.y = (mouseOffset.x + mouseOffset.y) * sensitivity * -1;
        }
        if (side == cubeState.Left)
        {
            rotation.x = (mouseOffset.x + mouseOffset.y) * sensitivity * -1;
        }
        if (side == cubeState.Right)
        {
            rotation.x = (mouseOffset.x + mouseOffset.y) * sensitivity * 1;
        }

        // rotate
        transform.Rotate(rotation, Space.Self);

        // store mouse
        mouseRef = Input.mousePosition;
    }

    private void AutoRotate()
    {
        isDragging = false;
        float step = speed * Time.deltaTime;
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetQuaternion, step);

        // if within onde degree, set angle to target agnle and end the rotation
        if (Quaternion.Angle(transform.localRotation, targetQuaternion) <= 1)
        {
            transform.localRotation = targetQuaternion;
            cubeState.PutDown(activeSide, transform.parent);
            cubeReader.ReadState();

            isAutoRotating = false;
            isDragging = false;
        }
    }
}
