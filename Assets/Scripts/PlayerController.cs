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
    Vector2 currentDirection = Vector2.zero;

    [SerializeField]
    private float timeToMove = 0.2f;
    [SerializeField]
    private float speed = 10.0f;

    void Start()
    {
        inputControls = new PlayerInputActions();
        inputControls.Enable();
    }

    void Update()
    {
        Vector2 input = inputControls.BaseCharacter.Move.ReadValue<Vector2>();

        transform.position = transform.position + new Vector3(input.x, input.y, 0) * speed * Time.deltaTime;

        //if (input.x != 0 && input.y != 0 && !newInput)
        //{
        //    newInput = true;
        //    if (Mathf.RoundToInt(input.x) == Mathf.RoundToInt(currentDirection.x))
        //    {
        //        currentDirection = new Vector2(0, input.y);
        //    }
        //    else
        //    {
        //        currentDirection = new Vector2(input.x, 0);
        //    }
        //}
        //else
        //{
        //    newInput = false;
        //currentDirection = input;
        //}
        //StartCoroutine(MovePlayer(cu));

        //if (currentDirection.y > 0 && !isMoving)
        //{
        //}
        //if (currentDirection.y < 0 && !isMoving)
        //{
        //    StartCoroutine(MovePlayer(Vector3.down));
        //}
        //if (currentDirection.x > 0 && !isMoving)
        //{
        //    StartCoroutine(MovePlayer(Vector3.right));
        //}
        //if (currentDirection.x < 0 && !isMoving)
        //{
        //    StartCoroutine(MovePlayer(Vector3.left));
        //}
    }

    //private IEnumerator MovePlayer(Vector3 direction)
    //{
    //    isMoving = true;

    //    float elapsedTime = 0;

    //    origPos = transform.position;
    //    targetPos = origPos + direction;

    //    while (elapsedTime < timeToMove)
    //    {
    //        transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
    //        elapsedTime += Time.deltaTime;
    //        yield return null;
    //    }

    //    transform.position = targetPos;

    //    isMoving = false;
    //}
}
