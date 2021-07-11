using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CubePainter cubePainter;
    [SerializeField] private CubeReader cubeReader;

    private void Start()
    {
        cubePainter.Init();
        cubePainter.AddNextMaterial();
        cubePainter.PaintRandomFace();

        cubeReader.ReadState();
    }
}
