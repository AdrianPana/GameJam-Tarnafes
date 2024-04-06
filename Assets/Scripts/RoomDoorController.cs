using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoomDoorController : MonoBehaviour
{
    [SerializeField] private RoomDoorController pairDoor;

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
    [SerializeField] private GameObject player;

    [SerializeField] private DoorPosition doorPosition;
    private Vector3 spawnPoint;

    [SerializeField] private SceneType sceneType;
    [SerializeField] private Vector3 centerPosition;


    void Start()
    {
        switch(doorPosition){
            case DoorPosition.Top:
                spawnPoint = transform.position + new Vector3(0, -2.0f, 0);
                break;
            case DoorPosition.Bottom:
                spawnPoint = transform.position + new Vector3(0, 2.0f, 0);
                break;
            case DoorPosition.Left:
                spawnPoint = transform.position + new Vector3(2.0f, 0, 0);
                break;
            case DoorPosition.Right:
                spawnPoint = transform.position + new Vector3(-2.0f, 0, 0);
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
        Debug.Log(player.transform.position);
        
    }

    private void OnTriggerStay2D(Collider2D other) {

        if(other.gameObject.CompareTag("Player")){
            Debug.Log("Player entered door");
            player.GetComponent<PlayerController>().isDisabled = true;
            MovePlayer(pairDoor.spawnPoint);    
            MoveCamera(pairDoor.centerPosition);
        }
    }

    private void MoveCamera(Vector3 center){
        Camera.main.transform.position = center + new Vector3(0, 0, -10);
    }

    private void MovePlayer(Vector3 center){
        Debug.Log("Moving player");
        Vector3 goToPosition = new Vector3(center.x, center.y, -1);
        Debug.Log(player.transform.position);
        
        player.transform.position = goToPosition;
        Debug.Log(player.transform.position);
        player.GetComponent<PlayerController>().isDisabled = false;
    }


}