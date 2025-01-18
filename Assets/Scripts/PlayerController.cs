using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject[] nextToShow;
    public GameObject[] nextToHide;

    public GameObject gameCamera;
    public AudioSource bgMusic;

    public float moveSpeed;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private bool isShownFirstTime = true;

    private bool isMoveFirstTime = true;
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

    


    private void FixedUpdate()
    {
        
        Move();
        if (!moveDirection.Equals(Vector2.zero))
        {
            if(!bgMusic.isPlaying)
            {
                bgMusic.Play();
            }

            Rotate();
        }
        
        
    }

    private void Rotate()
    {
        float angle =  Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    private void ProcessInput()
    {
        float moveX = Input.GetAxisRaw(Constants.Horizontal);
        float moveY = Input.GetAxisRaw(Constants.Vertical);
        moveDirection = new Vector3(moveX, moveY).normalized;
        if (isMoveFirstTime && !moveDirection.Equals(Vector3.zero))
        {
            isMoveFirstTime = false;

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

    void Move()
    {
        rb.velocity = moveDirection * moveSpeed;
    }
}
