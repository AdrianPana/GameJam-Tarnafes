using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory: MonoBehaviour
{
    public static bool hasTorch = false;

    [SerializeField]
    private GameObject torch;

    void Start()
    {
        torch.SetActive(false);
    }

    void Update()
    {
        if (hasTorch)
        {
            Debug.Log("Torch acquired");
            torch.SetActive(true);
        }
    }

}