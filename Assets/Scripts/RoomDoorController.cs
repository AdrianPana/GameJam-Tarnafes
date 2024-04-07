using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoomDoorController : MonoBehaviour
{
    [SerializeField] private RoomDoorController pairDoor;
    [SerializeField] private bool isDark = false;

    public enum SceneType
    {
        Scene1,
        Scene2,
        Scene3
    }

    public enum DoorPosition
    {
        Top,
        Bottom,
        Left,
        Right,
        None
    }

    [SerializeField] private int id;
    private GameObject player;

    [SerializeField] private DoorPosition doorPosition;
    private Vector3 spawnPoint;

    [SerializeField] private SceneType sceneType;
    [SerializeField] private Vector3 centerPosition;

    [SerializeField] float posOfsset = 1.4f;


    void Start()
    {
        player = GameObject.Find("Player");

        switch(doorPosition){
            case DoorPosition.Top:
                spawnPoint = transform.position + new Vector3(0, -posOfsset, 0);
                break;
            case DoorPosition.Bottom:
                spawnPoint = transform.position + new Vector3(0, posOfsset, 0);
                break;
            case DoorPosition.Left:
                spawnPoint = transform.position + new Vector3(posOfsset, 0, 0);
                break;
            case DoorPosition.Right:
                spawnPoint = transform.position + new Vector3(-posOfsset, 0, 0);
                break;
            case DoorPosition.None:
                spawnPoint = new Vector3(0, 0, 0);
                break;
        }
        // Debug.Log(spawnPoint + " in usa: " + id);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other) {

        if(other.gameObject.CompareTag("Player")){
            Debug.Log("Player entered door");
            player.GetComponent<PlayerController>().isDisabled = true;
            player.GetComponent<PlayerController>().SetLight(pairDoor.isDark);
            MovePlayer(pairDoor.spawnPoint);    
            MoveCamera(pairDoor.centerPosition);
        }
    }

    private void MoveCamera(Vector3 center){
        Camera.main.transform.position = center + new Vector3(0, 0, -10);
    }

    private void MovePlayer(Vector3 center){
        Vector3 goToPosition = new Vector3(center.x, center.y, -1);
        
        player.transform.position = goToPosition;
        player.GetComponent<PlayerController>().CenterOnCell();
        player.GetComponent<PlayerController>().isDisabled = false;
        //player.GetComponent<PlayerController>().collisionChecker.transform.position = player.transform.position;
    }


}
