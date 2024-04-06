using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;
using UnityEngine.Experimental.GlobalIllumination;


public class PlayerController : MonoBehaviour
{
    public Animator animator;
    static public bool hasTorch = false;
    private float invulnerabilityTime = 1.0f;

    [SerializeField]
    private bool isInvulnerable = false;
    private float invulnerabilityTimer = 0f;

    [SerializeField]
    private GameObject deathSound;
    public bool isMoving, isAttacking, isDisabled;
    private Vector3 origPos, targetPos;
    private Vector2 lastInput;
    private Vector2 direction;

    PlayerInputActions inputControls;
    private CollisionChecker collisionChecker;
    Vector2 currentDirection = Vector2.zero;
    Vector2 input;
    Tilemap tilemap;

    [SerializeField]
    private float timeToMove = 0.2f;
    [SerializeField]
    private float moveCooldown = 0.5f;
    private float moveCooldownTimer = 0.0f;

    public GameObject slashPrefab;

    public bool isColliding;

    public static int hp = 3;
    private int framesFromLastMove = 0;
    private const int framesToWait = 20;
    public int timeAlive = 0;

    void Awake() 
    {

    }
    void Start()
    {
        deathSound.SetActive(false);
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
        if(isDisabled)
        {
            return;
        }
        UpdateHearts();
        input = inputControls.BaseCharacter.Move.ReadValue<Vector2>();
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
            Invoke("TryToMove", 1.0f);
            //TryToMove(direction);        }

            if (isInvulnerable)
            {
                invulnerabilityTimer -= Time.deltaTime;
                if (invulnerabilityTimer <= 0)
                {
                    this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                    isInvulnerable = false;
                }

            }


        }
    }

    private void TryToMove()
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


    public void MakeInvulnerable()
    {
        isInvulnerable = true;
        invulnerabilityTimer = invulnerabilityTime;
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0.5f, 1);
    }

    public bool IsInvulnerable()
    {
        return isInvulnerable;
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

        CenterOnCell();

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
            deathSound.SetActive(true);
            animator.SetFloat("Dead", 1);
            // wait for sound to finish
            Invoke("DestroyPlayer", 1.0f);
            isDisabled = true;

        }
    }


    private void DestroyPlayer(){
        Destroy(this.gameObject);
    }
    public void TakeDamage() {
        if (isInvulnerable)
            return;
        hp--;
        this.MakeInvulnerable();

        UpdateHearts();
    }

    private void CenterOnCell()
    {
        Vector3Int cell = tilemap.WorldToCell(transform.position);
        Vector3 cellCenterPos = tilemap.GetCellCenterWorld(cell);
        transform.position = cellCenterPos;
    }

    private void UpdateHearts() 
    {
        HeartsScript hearts = GameObject.Find("Hearts").GetComponent<HeartsScript>();
        hearts.UpdateHearts(hp);
    }

    public void MakeBrighterLight() 
    {
        Transform light = transform.GetChild(1);
        light.GetComponent<UnityEngine.Rendering.Universal.Light2D>().pointLightOuterRadius = 3.11f;
        light.GetComponent<UnityEngine.Rendering.Universal.Light2D>().pointLightInnerRadius = 1.0f;
        
    }

    
}
