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

    // Start is called before the first frame update
    void Start()
    {
        collisionChecker = this.transform.Find("CollisionChecker").gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool Push(Vector2 direction)
    {
        if (!isMoving)
        {

            collisionChecker.transform.position = this.transform.position + new Vector3(direction.x, direction.y, 0);
            Debug.Log(this.transform.position);
            Debug.Log(direction);

            if (collisionChecker.GetComponent<CollisionChecker>().getCollisionState())
            {
                //collisionChecker.transform.position = this.transform.position;
                return false;
            }
            else
            {
                StartCoroutine(MovePushable(direction));
                //collisionChecker.transform.position = this.transform.position;
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

        transform.position = targetPos;

        isMoving = false;
    }
}
