using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubePainter : MonoBehaviour
{
    [SerializeField] private Material neutralMaterial;
    [SerializeField] private List<Material> allFaceMaterials;

    private List<Material> materialsInPlay = new List<Material>();
    private CubeFace[] faces;

    public void Init()
    {
        faces = FindObjectsOfType<CubeFace>();

        // start out the entire cube as netural
        foreach (CubeFace pieceFace in faces)
        {
            pieceFace.MeshMaterial = neutralMaterial;
        }
    }

    public void AddNextMaterial()
    {
        if (allFaceMaterials.Count > 0)
        {
            Material newMaterial = allFaceMaterials[0];
            materialsInPlay.Add(newMaterial);
            allFaceMaterials.Remove(newMaterial);
        }
    }

    public void PaintRandomFace()
    {
        CubeFace[] eligableFaces = faces.Where(x => x.MeshMaterial != neutralMaterial).ToArray();

        if (eligableFaces.Length > 0)
        {
            CubeFace randomFace = eligableFaces[Random.Range(0, eligableFaces.Length)];
            Material randomMaterial = materialsInPlay[Random.Range(0, materialsInPlay.Count)];
            randomFace.MeshMaterial = randomMaterial;
        }
        else
        {
            // TODO: lose the game because the cube is full
        }
    }
}
