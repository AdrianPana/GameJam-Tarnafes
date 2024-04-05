using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    [SerializeField]
    private bool inCollision;

    // Start is called before the first frame update
    void Start()
    {
        inCollision = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(inCollision);
    }

    public bool getCollisionState()
    {
        return inCollision;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        inCollision = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        inCollision = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        inCollision = false;
    }
}
