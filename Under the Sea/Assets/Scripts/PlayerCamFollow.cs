using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamFollow : MonoBehaviour {

    private GameObject cam;         // To hold camera object
    private GameObject player;      // To hold player object
    private Vector3 cam_offset;     // To hold the camera's offset location
    private int quadrant;           // To hold the current quadrant
    private float x_playerOffset;   // To hold a value for x
    private float z_playerOffset;   // To hold a value for z
    private float player_speed;     // To hold the speed of the player's movement
    private float camera_speed;     // To hold the speed of the camera mevement

    // Use this for initialization
    void Start()
    {
        quadrant = 0;                                           // set quadrant to the first one
        cam = GameObject.FindGameObjectWithTag("MainCamera");   // set cam GameObject to the main camera object
        player = GameObject.FindGameObjectWithTag("Player");    // set player GameObject to the player object
        camera_speed = 8f;                                      // set camera speed to 8, follow speed to get to player
        player_speed = 0.1f;                                    // set player speed to 0.1f, contant speed of movement underwater of otter
    }

    void LateUpdate()
    {
        FollowPlayer();   // Constant movement of camera, follows player
    }

    /// <summary>
    /// FollowPlayer method, Does two things:
    /// 1. Camera follows player
    /// 2. Determines directional movement
    /// </summary>
    public void FollowPlayer()
    {
        cam.transform.LookAt(player.transform);         // Camera looks at player at all times
        float step = camera_speed * Time.deltaTime;     // step, speed that camera will take to new location

        // Switch statement, Determines quadrant based on int value 'quadrant'
        // each quadrant that changes these things:
        // 1. setting directional movement
        // 2. Changing camera offset
        switch (quadrant)
        {
            // if -1, set to quadrant 3, looping
            case -1:
                quadrant = 3;
                break;
            case 0:
                SetX(0.0f);
                SetZ(-player_speed);
                cam_offset = new Vector3(player.transform.position.x - 8f, cam.transform.position.y, player.transform.position.z);
                break;
            case 1:
                SetX(player_speed);
                SetZ(0.0f);
                cam_offset = new Vector3(player.transform.position.x, cam.transform.position.y, player.transform.position.z - 8f);
                break;
            case 2:
                SetX(0.0f);
                SetZ(player_speed);
                cam_offset = new Vector3(player.transform.position.x + 8f, cam.transform.position.y, player.transform.position.z);
                break;
            case 3:
                SetX(-player_speed);
                SetZ(0.0f);
                cam_offset = new Vector3(player.transform.position.x, cam.transform.position.y, player.transform.position.z + 8f);
                break;
            // if 4, set to quadrant 1, looping
            case 4:
                quadrant = 0;
                break;
        }
        cam.transform.position = Vector3.MoveTowards(cam.transform.position, cam_offset, step); // Camera moves toward player, with every new offset
    }

    // Increment quadrant by 1
    public void MoveQuadrantRight()
    {
        quadrant++;
    }
    // Decrement quadrant by 1
    public void MoveQuadrantLeft()
    {
        quadrant--;
    }

    // set players x offset, to said value
    public void SetX(float x)
    {
        x_playerOffset = x;
    }

    // get players x value
    public float GetX()
    {
        return x_playerOffset;
    }

    // set players z offset, to said value 
    public void SetZ(float z)
    {
        z_playerOffset = z;
    }

    // get players z value
    public float GetZ()
    {
        return z_playerOffset;
    }
}
