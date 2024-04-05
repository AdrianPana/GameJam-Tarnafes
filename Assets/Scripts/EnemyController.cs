using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyController : MonoBehaviour
{
    private bool isMoving = false;
    private Vector3 origPos, targetPos;

    private const int framesToWait = 20;
    private int framesFromLastMove = 0;

    // PlayerInputActions inputControls;
    private CollisionChecker collisionChecker;
    Vector2 currentDirection = Vector2.zero;

    [SerializeField]
    private float timeToMove = 0.2f;

    private bool checkCollidesWithPlayer()
    {
        GameObject collidesWith = collisionChecker.getCollidesWith();
        if(collidesWith == null)
        {
            return false;
        }
        if (collidesWith.tag == "Player")
        {
            collidesWith.GetComponent<PlayerController>().TakeDamage();
            return true;
        }
        return false;
    }

    void Start()
    {
        // inputControls = new PlayerInputActions();
        // inputControls.Enable();
        collisionChecker = this.GetComponentInChildren<CollisionChecker>();
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

    void Update()
    {
        // Vector2 input = inputControls.BaseCharacter.Move.ReadValue<Vector2>();

        if (framesFromLastMove < framesToWait)
        {
            framesFromLastMove++;
            return;
        }
        if (framesFromLastMove == framesToWait)
        {
            framesFromLastMove = 0;
        }

        Vector2 input = randomDirection();
        if(checkCollidesWithPlayer())
        {
            Debug.Log("Collides with player");
        }
        //transform.position = transform.position + new Vector3(input.x, input.y, 0) * speed * Time.deltaTime;
        currentDirection = input;
        this.transform.Find("CollisionChecker").transform.position = targetPos +
                                            new Vector3(input.x, input.y, 0);


        if (!collisionChecker.getCollisionState())
        {

            if (currentDirection.y > 0 && !isMoving)
            {
                StartCoroutine(MovePlayer(Vector3.up));
            }
            if (currentDirection.y < 0 && !isMoving)
            {
                StartCoroutine(MovePlayer(Vector3.down));
            }
            if (currentDirection.x > 0 && !isMoving)
            {
                StartCoroutine(MovePlayer(Vector3.right));
            }
            if (currentDirection.x < 0 && !isMoving)
            {
                StartCoroutine(MovePlayer(Vector3.left));
            }
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

        isMoving = false;
    }
}
