using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] GameObject wallObject;
    [SerializeField] GameObject brickObject;
    [SerializeField] bool isWall = true;

    private BoxCollider wallCollider;

    public GameObject BrickObject { get { return brickObject; } }


    private void Start()
    {
        wallCollider = GetComponent<BoxCollider>();
        wallCollider.enabled = isWall;
    }

    private void OnValidate()
    {
        if (isWall)
        {
            wallObject.SetActive(true);

            brickObject.SetActive(false);
        }
        else
        {
            wallObject.SetActive(false);

            brickObject.SetActive(true);
        }
    }

    public void SetIsWall(bool isWall)
    {
        this.isWall = isWall;
    }

}
