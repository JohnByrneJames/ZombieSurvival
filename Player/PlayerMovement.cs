using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 6f; // Controls how fast the player is [f = Float variable]

    Vector3 movement;  // Store Movement want to apply to player
    Animator anim; // Reference to animator component
    Rigidbody playerRigidbody; // Reference to Rigidbody component

    int floorMask; // For floor quad tells raycast we only want to hit that floor - it stores it the floor mask as a integer
    private float camRayLength = 100f; // The length of ray we cast from the camera [f = Float variable]


    // Use this for initialization
    void Awake ()
    {
        floorMask = LayerMask.GetMask("Floor"); // Set up floor mask gets the layer mask = "floor" we created and added to the floor quad
        anim = GetComponent<Animator>(); // Set up and get references to animator, the <> denotes the type we are looking for <animator>
        playerRigidbody = GetComponent<Rigidbody>(); // Set up and get references to rigidbody so it is explicity linked to the rigidbody of the player
    }


    void FixedUpdate () {
        /* Not the standard input (-1 to 1) , raw input = will only have a value of -1, 0 or 1 (no variation) Rather than the character slowly accelerating
         * towards full speed, it will immediately snap to full speed - making it more responsive.
         */
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Called Every Physics step
        Move(h, v);
        Turning();
        Animating(h, v);
    }

    void Move(float h, float v)
    {
        // Set the movement vector based on the axis input.
        movement.Set(h, 0f, v);

        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            playerRigidbody.MoveRotation(newRotatation);
        }
    }

    void Animating(float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.
        anim.SetBool("IsWalking", walking);
    }
}
