/************************************
 * GrapplingHook.cs                  *
 * Created by: Agustin Ferrari      *
 ************************************/

// This script will simulate a GrapplingHook effect.
// You could use this to test some stuff.
// It works by using a raycast to the point you are looking
// and pulling you towards it (without taking into account
// any kind of obstacles).

using UnityEngine;

public class GrapplingHook : MonoBehaviour {
    //Variables
    [SerializeField]
    Transform characterToMove = null;

    public GameObject toPut;

    bool pullToObject = false;

    RaycastHit hittedStuff;

    private void Start () {
        //Set the player to move if it is not defined
        if (characterToMove == null)
            characterToMove = transform.parent;
    }

    private void Update () {
        //Attach to the surface
        if (Input.GetMouseButtonDown(0)) {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hittedStuff)) {
                pullToObject = true;                                                                                                                    //Start pulling
                Vector3 actualVelocity = characterToMove.GetComponent<Rigidbody>().velocity;                                                            //Velocity of the player
                characterToMove.GetComponent<Rigidbody>().velocity = new Vector3(actualVelocity.x * 0.5f, actualVelocity.y, actualVelocity.z * 0.5f);   //Halve horizontal velocity to magnify kinesthetics
            }
        }

        //Keep pulling until the user realeases the mouse
        if (Input.GetMouseButton(0) && pullToObject) {
            Vector3 normalizedvector = (hittedStuff.point - characterToMove.position).normalized;                           //Normalize direction
            characterToMove.GetComponent<Rigidbody>().AddForce(normalizedvector * 10000 * Time.deltaTime, ForceMode.Force); //Force to pull towards the surface
        }

        //Stop pulling
        if (Input.GetMouseButtonUp(0))
            pullToObject = false;   //Stop pulling
    }
}