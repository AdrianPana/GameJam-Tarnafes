using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.Tilemaps;

public class CollisionChecker : MonoBehaviour
{
    [SerializeField]
    private bool inCollision;
    private GameObject collidesWith;
    [SerializeField]
    public bool isPushable;
    [SerializeField]
    public GameObject collidedObject = null;
    Tilemap tilemap;

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
        else
        {
            isPushable = false;
            collidedObject = null;
        }
        if (!other.gameObject.CompareTag("Player"))
        {
            inCollision = true;
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

    public void CenterOnCell()
    {
        //Vector3 worldPos = Camera.main.ScreenToWorldPoint(transform.position);
        Vector3Int cell = tilemap.WorldToCell(transform.position);
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell);

        transform.position = cellCenterPos;
    }
}
