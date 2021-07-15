using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubeReader : MonoBehaviour
{
    [SerializeField] private CubeState cubeState;
    [SerializeField] private CubeMap cubeMap;
    [SerializeField] private CubePainter cubePainter;
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

    public delegate void MatchAction(CubeFace[] matchingFaces);
    public static event MatchAction OnMatch;

    void Awake()
    {
        SetRayTransforms();
    }

    public void ReadState()
    {
        // set the state of each position in the list of sides so we know what color is in what position
        cubeState.Up = ReadSide(upRays, Up);
        cubeState.Down = ReadSide(downRays, Down);
        cubeState.Left = ReadSide(leftRays, Left);
        cubeState.Right = ReadSide(rightRays, Right);
        cubeState.Front = ReadSide(frontRays, Front);
        cubeState.Back = ReadSide(backRays, Back);

        // update the map with the found positions
        cubeMap.Set();
    }

    public void DetectMatches()
    {
        DetectMatches(upRays, Up);
        DetectMatches(downRays, Down);
        DetectMatches(leftRays, Left);
        DetectMatches(rightRays, Right);
        DetectMatches(frontRays, Front);
        DetectMatches(downRays, Down);
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

    public List<GameObject> ReadSide(List<GameObject> rayStarts, Transform rayTransform)
    {
        List<GameObject> facesHit = new List<GameObject>();

        foreach (GameObject rayStart in rayStarts)
        {
            Vector3 ray = rayStart.transform.position;
            RaycastHit hit;

            if (Physics.Raycast(ray, rayTransform.forward, out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(ray, rayTransform.forward * hit.distance, Color.yellow, 1f);
                facesHit.Add(hit.collider.gameObject);
            }
            else
            {
                Debug.DrawLine(ray, rayTransform.forward * 1000, Color.green, 1f);
            }
        }

        return facesHit;
    }

    private void DetectMatches(List<GameObject> rayStarts, Transform rayTransform)
    {
        List<CubeFace> faces = ReadSide(rayStarts, rayTransform).Select(x => x.GetComponent<CubeFace>()).ToList();

        CubeFace[] row1 = new CubeFace[] { faces[0], faces[1], faces[2] };
        CubeFace[] row2 = new CubeFace[] { faces[3], faces[4], faces[5] };
        CubeFace[] row3 = new CubeFace[] { faces[6], faces[7], faces[8] };
        CubeFace[] col1 = new CubeFace[] { faces[0], faces[3], faces[6] };
        CubeFace[] col2 = new CubeFace[] { faces[1], faces[4], faces[7] };
        CubeFace[] col3 = new CubeFace[] { faces[2], faces[5], faces[8] };

        if (row1.All(x => x.MeshMaterial.color == row1[0].MeshMaterial.color && x.MeshMaterial.color != cubePainter.NeutralMaterial.color))
            OnMatch(row1);
        if (row2.All(x => x.MeshMaterial.color == row2[0].MeshMaterial.color && x.MeshMaterial.color != cubePainter.NeutralMaterial.color))
            OnMatch(row2);
        if (row3.All(x => x.MeshMaterial.color == row3[0].MeshMaterial.color && x.MeshMaterial.color != cubePainter.NeutralMaterial.color))
            OnMatch(row3);
        if (col1.All(x => x.MeshMaterial.color == col1[0].MeshMaterial.color && x.MeshMaterial.color != cubePainter.NeutralMaterial.color))
            OnMatch(col1);
        if (col2.All(x => x.MeshMaterial.color == col2[0].MeshMaterial.color && x.MeshMaterial.color != cubePainter.NeutralMaterial.color))
            OnMatch(col2);
        if (col3.All(x => x.MeshMaterial.color == col3[0].MeshMaterial.color && x.MeshMaterial.color != cubePainter.NeutralMaterial.color))
            OnMatch(col3);
    }
}
