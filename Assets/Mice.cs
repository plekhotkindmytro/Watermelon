using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mice : MonoBehaviour
{

    public float moveSpeed = 5f; // Speed of the object's movement
    public float changeDirectionInterval = 2f; // Time interval to change direction
    public float restTime = 1f;
    public bool faceDirection = true;
    public bool ball = false;

    private Vector2 screenBounds; // Screen bounds for limiting movement
    private Vector2 targetPosition; // Current target position on the screen
    private float moveTimer;
    private float restTimer;
    private Vector2 previousDirection;
    private Camera cameraMain;

    void Start()
    {
        cameraMain = Camera.main;
        // Calculate screen bounds
        screenBounds = new Vector2(cameraMain.orthographicSize * cameraMain.aspect, cameraMain.orthographicSize);
        targetPosition = new Vector2(transform.position.x, transform.position.y * -1);
    }



    void Update()
    {
        // Move the object towards the target position
        Vector2 direction = ((Vector2)targetPosition - (Vector2)transform.position).normalized;


        if (faceDirection && direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        //  float angle = CalculateAngle(transform.position, targetPosition);
        //  transform.rotation = Quaternion.Euler(Vector3.forward * angle);



        // Update the timer
        moveTimer += Time.deltaTime;

        // Change target position at the set interval
        if (CanRest())
        {
            Destroy(gameObject);
        }
        else
        {
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb)
        {
            
            rb.simulated = false;
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
            collision.gameObject.transform.SetParent(this.transform);
            targetPosition = new Vector2(targetPosition.x, targetPosition.y * -1);
        }
    }


    private bool CanContinueMoving()
    {
        bool restTimeOver = restTimer >= restTime;
        return restTimeOver;
    }

    private bool CanRest()
    {
        bool isAtTargetPos = Vector2.Distance(transform.position, targetPosition) < 0.5f;
        bool isMoveTimeOver = moveTimer >= changeDirectionInterval;

        return isAtTargetPos || isMoveTimeOver;


    }

    // Function to get a random position on the screen
    public void UpdateTargetPosition()
    {
        screenBounds = new Vector2(cameraMain.orthographicSize * cameraMain.aspect, cameraMain.orthographicSize);
        float randomX = Random.Range(-screenBounds.x, screenBounds.x);
        float randomY = Random.Range(-screenBounds.y, screenBounds.y);
        targetPosition = new Vector2(randomX, randomY);
        // transform.GetComponent<SpriteRenderer>().flipX = targetPosition.x < transform.position.x;
        bool isNotRotatable = transform.GetComponent<Rotatable>() == null;
        if (isNotRotatable)
        {
            transform.rotation = targetPosition.x < transform.position.x ? Quaternion.Euler(Vector3.up * 180) : Quaternion.identity;
        }


    }
}
