using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Orientation
{
    TopLeft,
    TopRight,
    BottomLeft,
    BottomRight
}

public class Push : MonoBehaviour
{
    [Tooltip("Vi tri cua goc vuong trong tam giac")]
    [SerializeField] Orientation orientation;

    [SerializeField]
    Vector3[] normalVectorArray =
    {
        new Vector3(1, 0, -1),
        new Vector3(-1, 0, -1),
        new Vector3(1, 0, 1),
        new Vector3(-1, 0, 1)
    };

    [SerializeField] Vector3 normalVector;
    public Vector3 NormalVector { get =>  normalVector; }

    private void OnValidate()
    {
        normalVector = normalVectorArray[(int)orientation];
    }
}
