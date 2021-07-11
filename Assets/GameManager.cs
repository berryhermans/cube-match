using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CubePainter cubePainter;

    private void Start()
    {
        cubePainter.Init();
        cubePainter.AddNextMaterial();
        cubePainter.PaintRandomFace();
    }
}
