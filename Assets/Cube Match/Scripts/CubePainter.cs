using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubePainter : MonoBehaviour
{
    [SerializeField] private Material neutralMaterial;
    [SerializeField] private List<Material> allMaterials;

    public Material NeutralMaterial
    {
        get => neutralMaterial;
    }

    private List<Material> materialsInPlay = new List<Material>();
    private CubeFace[] faces;

    private void OnEnable()
    {
        CubeReader.OnMatch += PaintFacesNeutral;
    }

    private void OnDisable()
    {
        
    }

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
        if (allMaterials.Count > 0)
        {
            Material newMaterial = allMaterials[0];
            materialsInPlay.Add(newMaterial);
            allMaterials.Remove(newMaterial);
        }
    }

    public void PaintRandomFace()
    {
        CubeFace[] eligableFaces = faces.Where(x => x.MeshMaterial.color == neutralMaterial.color).ToArray();

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

    private void PaintFacesNeutral(CubeFace[] faces)
    {
        foreach (CubeFace face in faces)
        {
            face.MeshMaterial = neutralMaterial;
        }
    }
}
