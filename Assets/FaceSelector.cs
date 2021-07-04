using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceSelector : MonoBehaviour
{
    [SerializeField] private CubeState cubeState;
    [SerializeField] private CubeReader cubeReader;

    private int layerMask = 1 << 6;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            cubeReader.ReadState();

            // raycast from the mouse towards the cube to see if a face is hit
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f, layerMask))
            {
                GameObject face = hit.collider.gameObject;
                // make a list of all the sides (lists of face GameObjects)
                List<List<GameObject>> cubeSides = new List<List<GameObject>>()
                {
                    cubeState.Up,
                    cubeState.Down,
                    cubeState.Left,
                    cubeState.Right,
                    cubeState.Front,
                    cubeState.Back,
                };

                // if the face hit exists within a side
                foreach (List<GameObject> cubeSide in cubeSides)
                {
                    if (cubeSide.Contains(face))
                    {
                        // pick it up
                        cubeState.PickUp(cubeSide);
                    }
                }
            }
        }
    }
}
