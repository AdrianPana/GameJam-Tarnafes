using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public Sprite newSprite;

    void Start()
    {
        Invoke("ChangePlayer", 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangePlayer()
    {
        player.GetComponent<SpriteRenderer>().sprite = newSprite;
        Invoke("GoToMenu", 10.0f);
    }

    private void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
