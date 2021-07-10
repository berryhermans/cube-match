using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubePainter : MonoBehaviour
{
    [SerializeField] private Material neutralMaterial;
    [SerializeField] private Material[] allPieceFaceMaterials;

    private List<Material> materialsInPlay;
    private PieceFace[] pieceFaces;

    private void Start()
    {
        pieceFaces = FindObjectsOfType<PieceFace>();

        // start out the entire cube as netural
        foreach (PieceFace pieceFace in pieceFaces)
        {
            pieceFace.MeshMaterial = neutralMaterial;
        }
    }
}
