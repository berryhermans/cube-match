using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeMap : MonoBehaviour
{
    [SerializeField] private CubeState cubeState;

    [SerializeField] private Transform up;
    [SerializeField] private Transform down;
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    [SerializeField] private Transform front;
    [SerializeField] private Transform back;

    public void Set()
    {
        UpdateMap(cubeState.Front, front);
        UpdateMap(cubeState.Back, back);
        UpdateMap(cubeState.Left, left);
        UpdateMap(cubeState.Right, right);
        UpdateMap(cubeState.Up, up);
        UpdateMap(cubeState.Down, down);
    }

    private void UpdateMap(List<GameObject> face, Transform side) 
    {
        int i = 0;
        foreach (Transform map in side)
        {
            map.GetComponent<Image>().color = face[i].GetComponent<CubeFace>().MeshMaterial.color;
            i++;
        }
    }
}
