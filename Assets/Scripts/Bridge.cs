using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] GameObject brick;

    public void ShowBrick()
    {
        brick.SetActive(true);
    }
}
