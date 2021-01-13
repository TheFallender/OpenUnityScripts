/************************************
 * PlayerMovement.cs                *
 * Created by: Agustin Ferrari      *
 ************************************/


// With this system you are able to have a 2D basic
// platformer movement, including jump and floor
// detection.
// P.S. This system uses the script InputSettings.cs


using UnityEngine;
using System;
using System.Collections;
using static Settings.Inputs;

public class PlayerMovement : MonoBehaviour {
    //Singleton
    public static PlayerMovement Instance = null;

    //Components
    private BoxCollider2D boxCol2d = null;

    //Parameters
    [SerializeField]
    private float movementSpeed = 10f;     //Speed of the player (can be modified)
    [SerializeField]
    private float jumpForce = 25f;         //Jump force (can be modified)

    //Variables
    private bool jumpAvailable = false;
    private bool disableCanJump = false;

    //Methods
    private void Awake () {
        if (Instance != null) {
            Destroy(transform);
            return;
        }

        Instance = this;

        //BoxCollider2D
        boxCol2d = GetComponent<BoxCollider2D>();
    }

    private void Update () {
        if (!disableCanJump && !jumpAvailable)
            jumpAvailable = CanJump();
        if (jumpAvailable && GetInputDown(ActionsInp.Jump))
            Jump();
    }

    private void FixedUpdate () {
        //Perform Movement
        MovementX();
    }

    private void MovementX () {
        //Movement
        float playerDir = 0f;

        //Movement Select Dir
        if (GetInput(ActionsInp.Left))
            playerDir += -1f;
        if (GetInput(ActionsInp.Right))
            playerDir += 1f;

        //Perform the movement in the given direction
        transform.Translate(Vector3.right * playerDir * movementSpeed * Time.fixedDeltaTime);
    }


    private void Jump () {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
        GetComponent<Rigidbody2D>().AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        jumpAvailable = false;
        StartCoroutine(JumpDisableTimer());
    }

    //BoxCasts parameters
    readonly float angle = 0f;
    readonly Vector2 dir = Vector2.down;
    readonly float distance = 0.1f;

    private bool CanJump () {
        //BoxCast param
        Vector3 originPosition = boxCol2d.bounds.center;
        Vector3 size = new Vector3(boxCol2d.bounds.size.x * 0.90f, boxCol2d.bounds.size.y);
        int layerToHit = LayerMask.GetMask("Ground");

        //Cast the box
        RaycastHit2D hittedObj = Physics2D.BoxCast(originPosition, size, angle, dir, distance, layerToHit);

        //Return wether it did hit or not 
        return (hittedObj.collider != null);
    }

    private IEnumerator JumpDisableTimer () {
        disableCanJump = true;
        yield return new WaitForSeconds(0.1f);
        disableCanJump = false;
    }
}