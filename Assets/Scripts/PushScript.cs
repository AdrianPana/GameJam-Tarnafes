using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushScript : MonoBehaviour, IPushable
{
    [SerializeField]
    private GameObject collisionCheckerPrefab;
    private GameObject collisionChecker;
    [SerializeField]
    private float timeToMove = 0.15f;
    private bool isMoving = false;
    private Vector3 origPos, targetPos;
    public bool isChecked;

    // Start is called before the first frame update
    void Start()
    {
        collisionChecker = this.transform.Find("CollisionChecker").gameObject;
        collisionChecker.GetComponent<CollisionChecker>().collided.AddListener(CheckerCollisionEnter);
        collisionChecker.GetComponent<CollisionChecker>().endCollided.AddListener(CheckerCollisionExit);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isChecked)
        {
            if (collisionChecker.GetComponent<CollisionChecker>().getCollisionState())
            {
                isChecked = true;
            }
        }
    }

    public void checkPushable(Vector2 destination)
    {
        collisionChecker.transform.position = destination;
    }

    public bool Push(Vector2 direction, Vector2 destination)
    {
        if (!isMoving)
        {
            collisionChecker.transform.position = destination;

            if (isChecked)
            {
                collisionChecker.transform.position = this.transform.position;
                return false;
            }
            else
            {
                StartCoroutine(MovePushable(direction));
                collisionChecker.transform.position = this.transform.position;
                return true;
            }
        }

        return false;
    }

    private IEnumerator MovePushable(Vector3 direction)
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

        collisionChecker.transform.position = transform.position;

        isMoving = false;
    }

    private void CheckerCollisionEnter()
    {
        isChecked = true;
    }
    private void CheckerCollisionExit()
    {
        isChecked = false;
    }
}
