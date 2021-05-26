using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 1.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f; 
    private static Animator anim;
    private bool isdrawn;
    private bool firstPerson;

    public GameObject fppCam, tppCam, aimCam, aimReticle, tpBrain;

    // Start is called before fthe first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>(); //gets objects CC
        anim = gameObject.GetComponent<Animator>(); //gets objects Animator
        isDrawn = false;
        firstPerson = false;
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

        // switch perspective
        if (Input.GetKeyDown(KeyCode.Tab)) {
            if(firstPerson == true) {
                firstPerson = false;
                tpBrain.SetActive(true);
                tppCam.SetActive(true);
                fppCam.SetActive(false);
                aimCam.SetActive(false);

            } else if (firstPerson == false) {
                firstPerson = true;
                tppCam.SetActive(false);
                fppCam.SetActive(true);
                aimCam.SetActive(false);
                tpBrain.SetActive(false);

            }
        }

        // draw/aim
        if (Input.GetMouseButtonDown(1))
        {
            if (isDrawn == false)
            {
                if(firstPerson == false)
                {
                    tppCam.SetActive(false);
                    aimCam.SetActive(true);
                }
                anim.SetBool("Draw", true);
                isDrawn = true;
            } else if (isDrawn == true) 
            {
                if (firstPerson == false)
                {
                    tppCam.SetActive(true);
                    aimCam.SetActive(false);
                }
                anim.SetBool("Draw", false);
                isDrawn = false;
            }
        }

        if(isDrawn == false)
        {
            aimCam.SetActive(false);
            aimReticle.SetActive(false); // deactivates crosshair

            // Third Person Activated
            if (firstPerson == false)
            {
                tpBrain.SetActive(true);
                tppCam.SetActive(true);
                fppCam.SetActive(false);
            }

            // first Person Activated
            else if (firstPerson == true)
            {
                tpBrain.SetActive(false);
                tppCam.SetActive(false);
                fppCam.SetActive(true);

            }
        } else if(isDrawn == true) {
            aimReticle.SetActive(true); // activates crosshair

            // first person
            if (firstPerson == false) {
                tpBrain.SetActive(true);
                tppCam.SetActive(false);
                fppCam.SetActive(false);
                aimCam.SetActive(true);
            }
            else if (firstPerson == true) {
                tpBrain.SetActive(false);
                tppCam.SetActive(false);
                aimCam.SetActive(false);
                fppCam.SetActive(true);

            }
            
        }

        //left and right
        if (Input.GetButton("Horizontal") && Input.GetKey(KeyCode.LeftShift))//running
        {
            anim.SetBool("Running", true);
            anim.SetBool("Draw", false);
            isDrawn = false;
            playerSpeed = 2f;
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
            playerSpeed = 1.0f;
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
        } else if (Input.GetButton("Vertical"))
        {
            anim.SetBool("Running", false);
            playerSpeed = 1.0f;
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