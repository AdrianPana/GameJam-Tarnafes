using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    [SerializeField]
    private string sceneName = "Dungeon";

    public void EnterDoor()
    {
        SceneManager.LoadScene(sceneName);
    }
    void Start()
    {   
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with player");
            //EnterDoor();
            Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Player"))
        {
        }
    }


}
