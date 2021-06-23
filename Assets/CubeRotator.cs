using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotator : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float rotationSpeed = 200f;

    private void OnEnable()
    {
        InputManager.OnSwipeLeft += () => { target.transform.Rotate(0, 90, 0, Space.World); };
        InputManager.OnSwipeRight += () => { target.transform.Rotate(0, -90, 0, Space.World); };
        InputManager.OnSwipeUpLeft += () => { target.transform.Rotate(90, 0, 0, Space.World); };
        InputManager.OnSwipeUpRight += () => { target.transform.Rotate(0, 0, -90, Space.World); };
        InputManager.OnSwipeDownLeft += () => { target.transform.Rotate(0, 0, 90, Space.World); };
        InputManager.OnSwipeDownRight += () => { target.transform.Rotate(-90, 0, 0, Space.World); };
    }

    private void Update()
    {
        if (transform.rotation != target.transform.rotation)
        {
            float rotationStep = rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target.transform.rotation, rotationStep);
        }
    }
}
