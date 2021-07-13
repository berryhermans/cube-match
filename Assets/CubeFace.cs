using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFace : MonoBehaviour
{
    [SerializeField] private MeshRenderer mesh;

    public CubeFaceType Type
    {
        get => Type;
        set {
            Type = value;
            MeshMaterial = value.Material;
        }
    }

    public Material MeshMaterial
    {
        get => mesh.material;
        private set => mesh.material = value;
    }
}
