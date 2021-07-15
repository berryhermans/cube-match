using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int MovesBeforeAddingColour;

    [SerializeField] private CubePainter cubePainter;
    [SerializeField] private CubeReader cubeReader;

    private int moveCount;

    private void OnEnable()
    {
        PivotRotation.OnUserRotation += IncreaseMoveCount;
        cubeReader.OnMatch += MatchFaces;
    }

    private void OnDisable()
    {
        PivotRotation.OnUserRotation -= IncreaseMoveCount;
        cubeReader.OnMatch -= MatchFaces;
    }

    private void Start()
    {
        cubePainter.Init();
        cubePainter.AddNextMaterial();
        cubePainter.PaintRandomFace();

        cubeReader.ReadState();
    }

    private void IncreaseMoveCount()
    {
        moveCount++;

        if (moveCount % MovesBeforeAddingColour == 0)
        {
            cubePainter.AddNextMaterial();
        }

        cubePainter.PaintRandomFace();
    }

    private void MatchFaces(CubeFace[] faces)
    {

    }
}
