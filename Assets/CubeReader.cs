using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeReader : MonoBehaviour
{
    [SerializeField] private CubeState cubeState;

    [Header("Rays")]
    public Transform Up;
    public Transform Down;
    public Transform Front;
    public Transform Back;
    public Transform Left;
    public Transform Right;

    private int layerMask = 1 << 6;

    void Update()
    {
        List<GameObject> facesHit = new List<GameObject>();
        Vector3 ray = Front.transform.position;
        RaycastHit hit;

        if (Physics.Raycast(ray, Front.forward, out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(ray, Front.forward * hit.distance, Color.yellow);
            facesHit.Add(hit.collider.gameObject);
            print(hit.collider.gameObject.name);
        }
        else
        {
            Debug.DrawLine(ray, Front.forward * 1000, Color.green);
        }

        cubeState.Front = facesHit;
    }
}
