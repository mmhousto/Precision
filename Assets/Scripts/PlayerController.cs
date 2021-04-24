using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 2.0f;
    private float gravityValue = -9.81f; 
    private static Animator anim;
    private bool isdrawn;

    // Start is called before fthe first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>(); //gets objects CC
        anim = gameObject.GetComponent<Animator>(); //gets objects Animator
        isDrawn = false;
    }

    public static bool isDrawn{ get; set; }

    public static void setShot1()
    {
        anim.SetInteger("Shot", 1);
    }

    public static void setShot0()
    {
        anim.SetInteger("Shot", 0);
    }



    // Update is called once per frame
    void Update()
    {
        //if (anim.GetInteger("Shot") == 1)
        //{
         //   anim.SetInteger("Shot", 0);
        //}
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        anim.SetFloat("vert", v); //sets Float values to players horizontal and vertical input.
        anim.SetFloat("horz", h);

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            anim.SetBool("inAir", false);
            playerVelocity.y = -1f;
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (isDrawn == false)
            {
                anim.SetBool("Draw", true);
                isDrawn = true;
            } else if (isDrawn == true) 
            {
                anim.SetBool("Draw", false);
                isDrawn = false;
            }
        }


        //left and right
        if (Input.GetButton("Horizontal") && Input.GetKey(KeyCode.LeftShift))//running
        {
            anim.SetBool("Running", true);
            anim.SetBool("Draw", false);
            isDrawn = false;
            Debug.Log(h);
            playerSpeed = 5f;
            Vector3 move = new Vector3(h, 0, 0);
            if (h > 0)
            {
                move = transform.TransformDirection(Vector3.right) * playerSpeed;//moves player to right/left in direction he is facing
            }else if (h < 0)
            {
                move = transform.TransformDirection(-Vector3.right) * playerSpeed;//moves player to right/left in direction he is facing
            }
            
            controller.Move(move * Time.deltaTime);
        } else if (Input.GetButton("Horizontal"))
        {
            anim.SetBool("Running", false);
            playerSpeed = 2.0f;
            Debug.Log(h);
            Vector3 move = new Vector3(h, 0, 0);
            if (h > 0)
            {
                move = transform.TransformDirection(Vector3.right) * playerSpeed;//moves player to right/left in direction he is facing
            }
            else if (h < 0)
            {
                move = transform.TransformDirection(-Vector3.right) * playerSpeed;//moves player to right/left in direction he is facing
            }

            controller.Move(move * Time.deltaTime);
        }

        //forward/back
        if (Input.GetButton("Vertical") && Input.GetKey(KeyCode.LeftShift))//running
        {
            anim.SetBool("Running", true);
            anim.SetBool("Draw", false);
            isDrawn = false;
            Debug.Log(v);
            playerSpeed = 5.0f;
            Vector3 move = new Vector3(0, 0, v);
            if (v > 0)
            {
                move = transform.TransformDirection(Vector3.forward) * playerSpeed;//moves player forward in direction he is facing

            }
            else if (v < 0)
            {
                move = transform.TransformDirection(-Vector3.forward) * playerSpeed;//moves player back in direction he is facing
            }
            controller.Move(move * Time.deltaTime); //moves player
            //gameObject.transform.forward = move;
        } else if (Input.GetButton("Vertical"))
        {
            anim.SetBool("Running", false);
            Debug.Log(v);
            playerSpeed = 2.0f;
            Vector3 move = new Vector3(0, 0, v);
            if (v > 0)
            {
                move = transform.TransformDirection(Vector3.forward) * playerSpeed;//moves player forward in direction he is facing

            }
            else if (v < 0)
            {
                move = transform.TransformDirection(-Vector3.forward) * playerSpeed;//moves player back in direction he is facing
            }
            controller.Move(move * Time.deltaTime); //moves player
            //gameObject.transform.forward = move;
        }

        if(Input.GetKeyUp("left shift"))
        {
            anim.SetBool("Running", false);
        }



        /*if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }*/

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            anim.SetBool("inAir", true);
        }
        
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}