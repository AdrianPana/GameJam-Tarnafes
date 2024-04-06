using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class EnemyController : MonoBehaviour
{
    private bool isMoving = false;
    private Vector3 origPos, targetPos;

    private const float framesToWait = 0.5f;
    private float framesFromLastMove = 0;

    // PlayerInputActions inputControls;
    private CollisionChecker collisionChecker;
    public bool isColliding;
    Vector2 currentDirection = Vector2.zero;

    [SerializeField]
    private float timeToMove = 0.2f;

    Tilemap tilemap;

    void Start()
    {
        // inputControls = new PlayerInputActions();
        // inputControls.Enable();
        collisionChecker = this.GetComponentInChildren<CollisionChecker>();
        collisionChecker.collided.AddListener(CheckerCollisionEnter);
        collisionChecker.endCollided.AddListener(CheckerCollisionExit);
        tilemap = GameObject.Find("Ground").GetComponent<Tilemap>();
    }


    void Update()
    {
        // Vector2 input = inputControls.BaseCharacter.Move.ReadValue<Vector2>();

        if (framesFromLastMove < framesToWait)
        {
            framesFromLastMove += Time.deltaTime;
            return;
        }
        if (framesFromLastMove >= framesToWait)
        {
            framesFromLastMove = 0;
        }

        Vector2 input = randomDirection();

        currentDirection = input;
        this.transform.Find("CollisionChecker").transform.position = transform.position +
                                            new Vector3(input.x, input.y, 0);

        Invoke("TryToMove", 1.0f);

    }

    public void TryToMove()
    {
        if (!isColliding)
        {
            var newDirection = GetDirection(currentDirection);
            StartCoroutine(MovePlayer(newDirection));
        }
    }

    public Vector2 GetDirection(Vector2 input)
    {
        Vector2 direction = Vector2.zero;

        if (input.x != 0)
        {
            direction = input.x > 0 ? Vector3.right : Vector3.left;
        }
        else if (input.y != 0)
        {
            direction = input.y > 0 ? Vector3.up : Vector3.down;
        }

        return direction;
    }

    private void ColliderSeek(Vector2 input)
    {
        this.transform.Find("CollisionChecker").transform.position = targetPos +
                                    new Vector3(input.x, input.y, 0);
        collisionChecker.CenterOnCell();
    }

    private void ColliderRetract()
    {
        this.transform.Find("CollisionChecker").transform.position = this.transform.position;
        collisionChecker.CenterOnCell();
    }
    private void CheckerCollisionEnter()
    {
        isColliding = true;
    }
    private void CheckerCollisionExit()
    {
        isColliding = false;
    }

    private Vector2 randomDirection()
    {
        System.Random random = new System.Random();
        int direction = random.Next(0, 4);
        // If collides with wall, try another direction
        switch (direction)
        {
            case 0:
                return new Vector2(0, 1);
            case 1:
                return new Vector2(0, -1);
            case 2:
                return new Vector2(1, 0);
            case 3:
                return new Vector2(-1, 0);
            default:
                return new Vector2(0, 0);
        }
    }

    private IEnumerator MovePlayer(Vector3 direction)
    {
        isMoving = true;

        float elapsedTime = 0;

        origPos = transform.position;
        targetPos = origPos + direction;

        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        CenterOnCell();

        isMoving = false;
    }

    public void TakeDamage()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage();
        }
    }

    private void CenterOnCell()
    {
        Vector3Int cell = tilemap.WorldToCell(transform.position);
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell);

        transform.position = cellCenterPos;
    }
}
