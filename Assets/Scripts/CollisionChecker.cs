using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    [SerializeField]
    private bool inCollision;
    private GameObject collidesWith;
    [SerializeField]
    public bool isPushable;
    [SerializeField]
    public GameObject collidedObject = null;

    public GameObject getCollidesWith()
    {
        return collidesWith;
    }

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
        if (other.gameObject.CompareTag("Pushable"))
        {
            isPushable = true;
            collidedObject = other.gameObject;
        }
        else
        {
            isPushable = false;
            collidedObject = null;
        }
        inCollision = true;

        if (!other.gameObject.CompareTag("Player"))
        {
            inCollision = true;
        }

        collidesWith = other.gameObject;
        {
            Debug.Log("Collided with anything");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pushable"))
        {
            isPushable = true;
            collidedObject = other.gameObject;
        }
        else
        {
            isPushable = false;
            collidedObject = null;
        }
        inCollision = true;

        if (!other.gameObject.CompareTag("Player"))
        {
            inCollision = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        inCollision = false;
        collidesWith = null;
    }
}
