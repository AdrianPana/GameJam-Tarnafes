using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHearts(int hp) {
        for(int i = 0; i < 3; i++) {
            if(i < hp) {
                transform.GetChild(i).gameObject.SetActive(true);
            } else {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
