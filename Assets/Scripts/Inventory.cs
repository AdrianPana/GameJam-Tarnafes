using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory: MonoBehaviour
{
    public bool hasTorch = false;
    public bool hasSword = false;
    public bool isStrong = false;

    [SerializeField]
    private GameObject torch;

    [SerializeField]
    private GameObject sword;

    [SerializeField]
    private GameObject potion;

    void Start()
    {
        sword.SetActive(false);
        torch.SetActive(false);
        potion.SetActive(false);
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
        }

        if (isStrong)
        {
            Debug.Log("Strong acquired");
            potion.SetActive(true);
        }
    }

}