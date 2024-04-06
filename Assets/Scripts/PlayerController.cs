using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using UnityEngine.Tilemaps;


public class PlayerController : MonoBehaviour
{
    public Animator animator;

    private bool isMoving, isAttacking;
    private Vector3 origPos, targetPos;
    private Vector2 lastInput;
    private Vector2 direction;

    PlayerInputActions inputControls;
    private CollisionChecker collisionChecker;
    Vector2 currentDirection = Vector2.zero;
    Tilemap tilemap;

    [SerializeField]
    private float timeToMove = 0.2f;
    [SerializeField]
    private float moveCooldown = 0.5f;
    private float moveCooldownTimer = 0.0f;

    public GameObject slashPrefab;

    public bool isColliding;

    public int hp = 3;

    void Start()
    {
        inputControls = new PlayerInputActions();
        inputControls.Enable();
        collisionChecker = this.GetComponentInChildren<CollisionChecker>();
        collisionChecker.collided.AddListener(CheckerCollisionEnter);
        collisionChecker.endCollided.AddListener(CheckerCollisionExit);
        tilemap = GameObject.Find("Ground").GetComponent<Tilemap>();
        lastInput = Vector2.down;
    }

    void Update()
    {
        Vector2 input = inputControls.BaseCharacter.Move.ReadValue<Vector2>();
        float attackInput = inputControls.BaseCharacter.Attack.ReadValue<float>();
        direction = GetDirection(input);

        if (input == Vector2.zero)
        {
            Animate(GetDirection(lastInput));
            if (attackInput > 0 && !isAttacking && !isMoving)
            {
                StartCoroutine(Attack(lastInput));
            }
        }
        else
        {
            Animate(direction);
        }
        Die();

        if (!isAttacking)
        {
            TryToMove(direction);
        }

        
    }

    private void TryToMove(Vector2 input)
    {
        ColliderSeek(direction);

        if (!isColliding)
        {
            if (collisionChecker.getCollisionState())
            {
                isColliding = true;
            }
        }

        if (isColliding && collisionChecker.isPushable)
        {
            IPushable box = null;
            if (collisionChecker.collidedObject)
                box = collisionChecker.collidedObject.GetComponent<IPushable>();
            if (box != null)
            {


                if (box.Push(direction, transform.position +
                    new Vector3(direction.x, direction.y, 0)))
                {
                    ColliderRetract();
                    Move(direction);
                }
            }
        }

        if (!isColliding)
        {
            ColliderRetract();
            Move(direction);
        }

        if (input != Vector2.zero)
        {
            lastInput = input;
        }
    }

    private void CheckerCollisionEnter()
    {
        isColliding = true;
    }
    private void CheckerCollisionExit()
    {
        isColliding = false;
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

    public IEnumerator Attack(Vector2 direction)
    {
        isAttacking = true;

        var slash = Instantiate(slashPrefab);
        slash.transform.position = this.transform.position + new Vector3(direction.x, direction.y, 0);
        yield return null;

        Invoke("ResetState", slash.GetComponent<SlashScript>().animationTime);
    }

    public void ResetState()
    {
        isAttacking = false;
    }

    public void Move(Vector2 direction)
    {
        if (isMoving)
            return;

        //Vector2 direction = Vector2.zero;

        ////if (input.y != 0 && input.y == lastInput.y && input.x != 0 && input.x != lastInput.x)
        ////{
        ////    direction = input.x > 0 ? Vector3.right : Vector3.left;
        ////}
        //if (input.x != 0)
        //{
        //    direction = input.x > 0 ? Vector3.right : Vector3.left;
        //}
        //else if (input.y != 0)
        //{
        //    direction = input.y > 0 ? Vector3.up : Vector3.down;
        //}

        StartCoroutine(MovePlayer(direction));
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

        //CenterOnCell();

        isMoving = false;
    }

    private void Animate(Vector2 input)
    {
        animator.SetFloat("Horizontal", input.x);
        animator.SetFloat("Vertical", input.y);
        animator.SetFloat("Speed", input.sqrMagnitude);
        transform.localScale = new Vector3(input.x > 0 ? -1 : input.x < 0 ? 1 : transform.localScale.x, 1, 1);
    }

    private void Die()
    {
        if (hp <= 0)
        {
            Debug.Log("Player is dead");
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage() {
        hp--;
    }

    private void CenterOnCell()
    {
        //Vector3 worldPos = Camera.main.ScreenToWorldPoint(transform.position);
        Vector3Int cell = tilemap.WorldToCell(transform.position);
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell);

        transform.position = cellCenterPos;
    }
}
