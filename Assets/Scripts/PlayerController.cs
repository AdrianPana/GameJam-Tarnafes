using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private bool isMoving;
    private Vector3 origPos, targetPos;
    private bool newInput = false;

    PlayerInputActions inputControls;
    private CollisionChecker collisionChecker;
    Vector2 currentDirection = Vector2.zero;

    [SerializeField]
    private float timeToMove = 0.2f;
    [SerializeField]
    private float speed = 10.0f;

    void Start()
    {
        inputControls = new PlayerInputActions();
        inputControls.Enable();
        collisionChecker = this.GetComponentInChildren<CollisionChecker>();
    }

    void Update()
    {
        Vector2 input = inputControls.BaseCharacter.Move.ReadValue<Vector2>();

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
