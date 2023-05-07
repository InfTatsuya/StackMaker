using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishBridge : MonoBehaviour
{
    [SerializeField] Color color;

    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            meshRenderer.material.color = color;
        }
    }
}
