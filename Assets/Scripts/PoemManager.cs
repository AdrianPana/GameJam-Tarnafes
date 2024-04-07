using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PoemManager: MonoBehaviour
{

    public static PoemManager instance;
    static public int poemCount = 0;
    private int poemTotal = 4;

    [SerializeField]
    private TMP_Text poemText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void OnGUI()
    {
        poemText.text = poemCount + "/" + poemTotal;
    }

    public void ChangePoems()
    {
        poemCount++;
    }
     
    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Player") && !PlayerController.hasSword)
    //     {
            
    //         Destroy(this.gameObject);
    //     }
    // }
}