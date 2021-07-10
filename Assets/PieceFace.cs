using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceFace : MonoBehaviour
{
    [SerializeField] private MeshRenderer mesh;

    public Material MeshMaterial
    {
        get => mesh.material;
        set => mesh.material = value;
    }
}
