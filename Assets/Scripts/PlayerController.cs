using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Joystick joystick;
    public GameObject[] nextToShow;
    public GameObject[] nextToHide;

    public GameObject gameCamera;
    public AudioSource bgMusic;

    public float moveSpeed;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private bool isShownFirstTime = true;

    private bool isMoveFirstTime = true;
    private bool isTargetSet;


   private Vector3 moveToPoint; 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    


    //private void FixedUpdate()
    //{
        
    //    Move();
    //    if (!moveDirection.Equals(Vector2.zero))
    //    {
    //        if(!bgMusic.isPlaying)
    //        {
    //            bgMusic.Play();
    //        }

    //        Rotate();
    //    }
        
        
    //}

    private void Rotate()
    {
        float angle =  Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    private void Rotate(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    private void MoveToPosition(Vector3 targetPosition)
    {
        // Ensure the rocket stays on the correct Z plane
        targetPosition.z = 0;  // Adjust this based on your game's 2D plane
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) <= 0.1f)
        {
            isTargetSet = false;
        }
    }

    private void ProcessInput()
    {

        float moveX = 0;
        float moveY = 0;
        // joystick
        //moveX = joystick.Horizontal;
        //moveY = joystick.Vertical;

        // touch
        if (Input.touchCount > 0)
        {
            moveToPoint = MainCameraReference.MainCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
            isTargetSet = true;
        }

        if(isTargetSet)
        {
            Vector3 direction = (moveToPoint - transform.position).normalized;
            print(direction);
            moveX = direction.x;
            moveY = direction.y;

            MoveToPosition(moveToPoint);
            Rotate(direction);
        }
            
        
        


        // keyboard
        // float moveX = Input.GetAxisRaw(Constants.Horizontal);
        // float moveY = Input.GetAxisRaw(Constants.Vertical);


        
        if (isMoveFirstTime && Input.touchCount > 0)
        {
            isMoveFirstTime = false;
            if (!bgMusic.isPlaying)
            {
                bgMusic.Play();
            }

                if (nextToHide != null)
            {
                for (int i = 0; i < nextToHide.Length; i++)
                {
                    nextToHide[i].SetActive(false);
                }
            }

            if (nextToShow != null)
            {
                for (int i = 0; i < nextToShow.Length; i++)
                {
                    nextToShow[i].SetActive(true);
                }
            }
            isShownFirstTime = false;


        }
        
    }

    //void Move()
    //{
    //    rb.velocity = moveDirection * moveSpeed;
    //}
}
