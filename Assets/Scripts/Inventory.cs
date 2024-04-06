using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory: MonoBehaviour
{
    public static bool hasTorch = false;
    public static bool hasSword = false;

    [SerializeField]
    private GameObject torch;

    [SerializeField]
    private GameObject sword;

    void Start()
    {
        sword.SetActive(false);
        torch.SetActive(false);
    }

    void Update()
    {
        if (hasTorch)
        {
            Debug.Log("Torch acquired");
            torch.SetActive(true);
            GameObject.Find("Player").GetComponent<PlayerController>().MakeBrighterLight();
        }

        if (hasSword)
        {
            Debug.Log("Sword acquired");
            sword.SetActive(true);
            GameObject.Find("Player").GetComponent<PlayerController>().MakeBrighterLight();
        }
    }

}