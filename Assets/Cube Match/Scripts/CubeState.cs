using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeState : MonoBehaviour
{
    public List<GameObject> Front = new List<GameObject>();
    public List<GameObject> Back = new List<GameObject>();
    public List<GameObject> Up = new List<GameObject>();
    public List<GameObject> Down = new List<GameObject>();
    public List<GameObject> Left = new List<GameObject>();
    public List<GameObject> Right = new List<GameObject>();


    public void PickUp(List<GameObject> cubeSide)
    {
        foreach (GameObject face in cubeSide)
        {
            // attach the parent of each face (the little cube)
            // to the parent of the 4th index (the little cube in the middle)
            if (face != cubeSide[4])
            {
                face.transform.parent.transform.parent = cubeSide[4].transform.parent;
            }
        }

        // start the side rotation logic
        cubeSide[4].transform.parent.GetComponent<PivotRotation>().Rotate(cubeSide);
    }

    public void PutDown(List<GameObject> littleCubes, Transform pivot)
    {
        foreach (GameObject littleCube in littleCubes)
        {
            if (littleCube != littleCubes[4])
            {
                littleCube.transform.parent.transform.parent = pivot;
            }
        }
    }
}
