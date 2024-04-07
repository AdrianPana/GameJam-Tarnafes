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
}
