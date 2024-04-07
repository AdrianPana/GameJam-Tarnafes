using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FataScript : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (PoemManager.poemCount < 4) {
                textMeshProUGUI.gameObject.SetActive(true);
            }
            else
            {
                string newScene = collision.gameObject.GetComponent<PlayerController>().isStrong ? "Win" : "Lose";
                PoemManager.poemCount = 0;
                SceneManager.LoadScene(newScene);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        textMeshProUGUI.gameObject.SetActive(false);
    }
}
