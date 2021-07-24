using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // CAMERA VARIABLES
    public Transform target;
    public Vector3 newPos;
    public float smoothing = 0.25f;
    public float camTargetDist;
    public float playercamTargetDist;
    public float camPlayerDist;
    public GameObject player;
    public Rigidbody2D playerRb;

    // START
    void Start() 
    {
        player = GameObject.Find("Droid");
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    // LATE UPDATE
    void LateUpdate()
    {      
        MoveCamera();
    }

    // MOVE CAMERA
    void MoveCamera()
    {        
        if(transform.position != target.position)
        {
            // CALCULATE DISTANCES
            playercamTargetDist = Vector3.Distance(target.transform.position, playerRb.transform.position);
            camTargetDist = Vector2.Distance(
                new Vector2(transform.position.x, transform.position.y), 
                new Vector2(target.transform.position.x, target.transform.position.y));
            camPlayerDist = Vector2.Distance(
                new Vector2(transform.position.x, transform.position.y), 
                new Vector2(playerRb.transform.position.x, playerRb.transform.position.y));

            // IF TARGET AND CAM POSITION IS TOO CLOSE TO PLAYER, SET POSITION TO PLAYERS
            if ((Mathf.FloorToInt(playercamTargetDist * 16f) < 1)
            && (Mathf.FloorToInt(camPlayerDist * 16f) < 1)) {
                transform.position = new Vector3(playerRb.transform.position.x, playerRb.transform.position.y, transform.position.z);
            } else {
                Vector3 camTarget = Vector3.Lerp(transform.position, target.position, smoothing);
                //Vector3 camPlayerDiff = transform.position - playerRb.transform.position;

                // IF CAM IS ALMOST AT TARGET, SET POSITION TO TARGETS
                if (Mathf.FloorToInt(camTargetDist * 16f) > 1){
                    Vector2 camPlayerDiff = new Vector2(camTarget.x, camTarget.y) 
                        - new Vector2(playerRb.transform.position.x, playerRb.transform.position.y);
                    float newX = playerRb.transform.position.x + ((float)(Mathf.RoundToInt(camPlayerDiff.x * 16f)) / 16);
                    float newY = playerRb.transform.position.y + ((float)(Mathf.RoundToInt(camPlayerDiff.y * 16f)) / 16);
                    newPos = new Vector3(newX, newY, transform.position.z);
                    transform.position = newPos; 
                } else {
                    Vector2 targetPlayerDiff = new Vector2(target.position.x, target.position.y) 
                        - new Vector2(playerRb.transform.position.x, playerRb.transform.position.y);
                    float newX = playerRb.transform.position.x + ((float)(Mathf.RoundToInt(targetPlayerDiff.x * 16f)) / 16);
                    float newY = playerRb.transform.position.y + ((float)(Mathf.RoundToInt(targetPlayerDiff.y * 16f)) / 16);
                    newPos = new Vector3(newX, newY, transform.position.z);
                    transform.position = newPos; 
                    //transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
                }
            }            
        }  
    }
}
