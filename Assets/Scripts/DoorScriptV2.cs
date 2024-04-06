using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScriptV2 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string sceneName;
    [SerializeField] private GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // Debug.Log(other.gameObject.tag);
        // Debug.Log(other.gameObject.name);
        if(other.gameObject.tag == "CollisionChecker"){
            // DontDestroyOnLoad(player);
            SceneManager.LoadScene(sceneName);
        }
    }
}
