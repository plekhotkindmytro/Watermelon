using UnityEngine;

public class RandomMovement : MonoBehaviour
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
        screenBounds = new Vector2 (cameraMain.orthographicSize*cameraMain.aspect, cameraMain.orthographicSize);

        print("x: " + screenBounds.x);
        print("y: " + screenBounds.y);
        // Initialize target position
        UpdateTargetPosition();
    }

     /*private float CalculateAngle(Vector2 point1, Vector2 point2)
    {
        Vector2 direction = point2 - point1;

        // Calculate the angle in radians
        float angleRad = Mathf.Atan2(direction.y, direction.x);

        // Convert radians to degrees
        float angleDeg = angleRad * Mathf.Rad2Deg;

        // Ensure the angle is positive
        if (angleDeg < 0)
        {
            angleDeg += 360f;
        }

        return angleDeg - 90;
    }*/
    

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
            restTimer += Time.deltaTime;
            if(CanContinueMoving()) {
                UpdateTargetPosition();
                restTimer = 0;
                moveTimer = 0;
            }
        } else {
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
        }
    }

    private bool CanContinueMoving() {
        bool restTimeOver = restTimer >= restTime;
        return restTimeOver;
    }

    private bool CanRest() {
        bool isAtTargetPos = Vector2.Distance(transform.position, targetPosition) < 0.5f;
        bool isMoveTimeOver = moveTimer >= changeDirectionInterval;

        return isAtTargetPos || isMoveTimeOver;


    }

    // Function to get a random position on the screen
    public void UpdateTargetPosition()
    {
        screenBounds = new Vector2 (cameraMain.orthographicSize*cameraMain.aspect, cameraMain.orthographicSize);
        float randomX = Random.Range(-screenBounds.x, screenBounds.x);
        float randomY = Random.Range(-screenBounds.y, screenBounds.y);
        targetPosition =  new Vector2(randomX, randomY);
        // transform.GetComponent<SpriteRenderer>().flipX = targetPosition.x < transform.position.x;
        bool isNotRotatable = transform.GetComponent<Rotatable>() == null;
        if(isNotRotatable) {
            transform.rotation = targetPosition.x < transform.position.x ? Quaternion.Euler(Vector3.up*180): Quaternion.identity;
        }
        
        
    }
}
