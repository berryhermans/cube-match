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
            if (face[i].name[0] == 'F')
            {
                map.GetComponent<Image>().color = Color.blue;
            }
            if (face[i].name[0] == 'B')
            {
                map.GetComponent<Image>().color = Color.green;
            }
            if (face[i].name[0] == 'L')
            {
                map.GetComponent<Image>().color = new Color(1, 0.5f, 0, 1);
            }
            if (face[i].name[0] == 'R')
            {
                map.GetComponent<Image>().color = Color.red;
            }
            if (face[i].name[0] == 'U')
            {
                map.GetComponent<Image>().color = Color.yellow;
            }
            if (face[i].name[0] == 'D')
            {
                map.GetComponent<Image>().color = Color.white;
            }
            i++;
        }
    }
}
