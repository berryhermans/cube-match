using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubePainter : MonoBehaviour
{
    [SerializeField] private CubeFaceType neutralCubeFaceType;
    [SerializeField] private List<CubeFaceType> allCubeFaceTypes;

    private List<CubeFaceType> cubeFaceTypesInPlay = new List<CubeFaceType>();
    private CubeFace[] faces;

    public void Init()
    {
        faces = FindObjectsOfType<CubeFace>();

        // start out the entire cube as netural
        foreach (CubeFace pieceFace in faces)
        {
            pieceFace.Type = neutralCubeFaceType;
        }
    }

    public void AddNextMaterial()
    {
        if (allCubeFaceTypes.Count > 0)
        {
            CubeFaceType newType = allCubeFaceTypes[0];
            cubeFaceTypesInPlay.Add(newType);
            allCubeFaceTypes.Remove(newType);
        }
    }

    public void PaintRandomFace()
    {
        CubeFace[] eligableFaces = faces.Where(x => x.MeshMaterial.color == neutralCubeFaceType.Material.color).ToArray();

        if (eligableFaces.Length > 0)
        {
            CubeFace randomFace = eligableFaces[Random.Range(0, eligableFaces.Length)];
            CubeFaceType randomType = cubeFaceTypesInPlay[Random.Range(0, cubeFaceTypesInPlay.Count)];
            randomFace.Type = randomType;
        }
        else
        {
            // TODO: lose the game because the cube is full
        }
    }
}
