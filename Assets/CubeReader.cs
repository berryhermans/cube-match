using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeReader : MonoBehaviour
{
    [SerializeField] private CubeState cubeState;
    [SerializeField] private CubeMap cubeMap;
    [SerializeField] private GameObject rayPrefab;

    [Header("Rays")]
    public Transform Up;
    public Transform Down;
    public Transform Front;
    public Transform Back;
    public Transform Left;
    public Transform Right;

    private int layerMask = 1 << 6;

    private List<GameObject> frontRays = new List<GameObject>();
    private List<GameObject> backRays = new List<GameObject>();
    private List<GameObject> upRays = new List<GameObject>();
    private List<GameObject> downRays = new List<GameObject>();
    private List<GameObject> leftRays = new List<GameObject>();
    private List<GameObject> rightRays = new List<GameObject>();

    void Start()
    {
        SetRayTransforms();

        
    }

    void Update()
    {
    }

    public void ReadState()
    {
        // set the state of each position in the list of sides so we know what color is in what position
        cubeState.Up = ReadFace(upRays, Up);
        cubeState.Down = ReadFace(downRays, Down);
        cubeState.Left = ReadFace(leftRays, Left);
        cubeState.Right = ReadFace(rightRays, Right);
        cubeState.Front = ReadFace(frontRays, Front);
        cubeState.Back = ReadFace(backRays, Back);

        // update the map with the found positions
        cubeMap.Set();
    }

    private void SetRayTransforms()
    {
        // populate the rays listst with raycasts eminating from the transform, angled towards the cube.
        upRays = BuildRays(Up, new Vector3(90, 90, 0));
        downRays = BuildRays(Down, new Vector3(270, 90, 0));
        leftRays = BuildRays(Left, new Vector3(0, 90, 0)); // new Vector3(0, 90, 0)
        rightRays = BuildRays(Right, new Vector3(0, 270, 0)); // new Vector3(0, 270, 0)
        frontRays = BuildRays(Front, new Vector3(0, 0, 0)); // new Vector3(0, 270, 0)
        backRays = BuildRays(Back, new Vector3(0, 180, 0)); // new Vector3(0, 180, 0)
    }

    private List<GameObject> BuildRays(Transform rayTransform, Vector3 direction)
    {
        // the ray count is used to name the rays so we can be sure they are in the right order
        int rayCount = 0;

        // this creates 9 rays in the shape of the side of the cube with
        // Ray - at the top left and Ray 8 at the bottom right:
        // |0|1|2|
        // |3|4|5|
        // |6|7|8|
        List<GameObject> rays = new List<GameObject>();
        for (int y = 1; y > -2; y--)
        {
            for (int x = -1; x < 2; x++)
            {
                Vector3 startPos = new Vector3(
                    rayTransform.localPosition.x + x,
                    rayTransform.localPosition.y + y,
                    rayTransform.localPosition.z);

                GameObject rayStart = Instantiate(rayPrefab, startPos, Quaternion.identity, rayTransform);
                rayStart.name = rayCount.ToString();
                rays.Add(rayStart);
                rayCount++;
            }
        }
        rayTransform.localRotation = Quaternion.Euler(direction);
        return rays;
    }

    public List<GameObject> ReadFace(List<GameObject> rayStarts, Transform rayTransform)
    {
        List<GameObject> facesHit = new List<GameObject>();

        foreach (GameObject rayStart in rayStarts)
        {
            Vector3 ray = rayStart.transform.position;
            RaycastHit hit;

            if (Physics.Raycast(ray, rayTransform.forward, out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(ray, rayTransform.forward * hit.distance, Color.yellow);
                facesHit.Add(hit.collider.gameObject);
                //print(hit.collider.gameObject.name);
            }
            else
            {
                Debug.DrawLine(ray, rayTransform.forward * 1000, Color.green);
            }
        }


        return facesHit;
    }
}
