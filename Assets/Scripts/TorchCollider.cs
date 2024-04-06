using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TorchCollider : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !PlayerController.hasTorch)
        {
            PlayerController.hasTorch = true;
            Inventory.hasTorch = true;
            Destroy(this.gameObject);
        }
    }

}