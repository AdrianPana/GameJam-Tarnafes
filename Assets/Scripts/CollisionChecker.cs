using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class CollisionChecker : MonoBehaviour
{
    [SerializeField]
    private bool inCollision;
    private GameObject collidesWith;
    [SerializeField]
    public bool isPushable;
    [SerializeField]
    public GameObject collidedObject = null;
    [SerializeField]
    public bool isAttackable;
    Tilemap tilemap;

    public UnityEvent collided;
    public UnityEvent endCollided;

    public GameObject getCollidesWith()
    {
        return collidesWith;
    }

    // Start is called before the first frame update
    void Start()
    {
        inCollision = false;
        tilemap = GameObject.Find("Ground").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool getCollisionState()
    {
        return inCollision;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Door")) 
        {
            Debug.Log("da");
            other.gameObject.GetComponent<DoorScript>().EnterDoor();
        }
        
        if (other.gameObject.CompareTag("Pushable"))
        {
            isPushable = true;
            collidedObject = other.gameObject;
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            isAttackable = true;
            collidedObject = other.gameObject;
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            inCollision = false;
            return;
        }
        else
        {
            isPushable = false;
            collidedObject = null;
        }
        if (!other.gameObject.CompareTag("Player"))
        {
            inCollision = true;
            collided.Invoke();
        }


        collidesWith = other.gameObject;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pushable"))
        {
            isPushable = true;
            collidedObject = other.gameObject;
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            inCollision = false;
            return;
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
            collided.Invoke();
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        inCollision = false;
        collidesWith = null;
        endCollided.Invoke();
    }

    public void CenterOnCell()
    {
        //Vector3 worldPos = Camera.main.ScreenToWorldPoint(transform.position);
        Vector3Int cell = tilemap.WorldToCell(transform.position);
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell);

        transform.position = cellCenterPos;
    }
}
