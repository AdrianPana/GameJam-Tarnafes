using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TorchCollider : MonoBehaviour
{
    public PlayerController player;
    public Inventory inv;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !player.hasTorch)
        {
            player.hasTorch = true;
            inv.hasTorch = true;
            Destroy(this.gameObject);
        }
    }

}