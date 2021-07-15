using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAnimator : MonoBehaviour
{
    [SerializeField] public GameObject Cube;

    [Header("Animation Config")]
    public Vector3 CubePunchScale;
    public float CubePunchUpDuration;
    public float CubePunchDownDuration;

    private void OnEnable()
    {
        CubeReader.OnMatch += (matches) => { PunchCube(); };
    }

    public void PunchCube()
    {
        print("punch!");
        Cube.transform.DOScale(CubePunchScale, CubePunchUpDuration).OnComplete(() => {
            Cube.transform.DOScale(Vector3.one, CubePunchDownDuration);
        });
    }
}
