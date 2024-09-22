using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouch : MonoBehaviour
{
    private Vector2 touchStartPos;
    //private Vector2 touchEndPos;
    //public float swipeThreshold = 50f;  Adjust this threshold as needed.
    //public float moeSpeed = 5f;  Adjust the movement speed as needed.
    public GameObject player;
    //private Vector2 touchStartPos;
    private bool isDragging = false;

    float x, y, z;
    public bool blockX, blockY, blockZ;

    public bool canDrag = true;
    public bool collisionIsOnLeft = false, collisionIsOnRight = false;

    public int direction = 1;
    public float distance;
    public float playerSpeed;
    public float turnDirectionSpeed;
    public bool isRightdrag = false;
    public bool isJumping=false, isSlipping=false;
    Animator animator_player;
    BoxCollider2D collider2D_player;

    float timeCount_speed;
    public float plusSpeedNum, updateSpeedTime;
    public float originalPlayerSpeed;

    //chara characterController;

    private void Start()
    {

        animator_player = gameObject.GetComponent<Animator>();
        collider2D_player = gameObject.GetComponent<BoxCollider2D>();
        //characterController = gameObject.GetComponent<CharacterController>();
    }
    void Update()
    {
        if (Input.touchCount > 0 && canDrag)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // The touch has just begun.
                touchStartPos = touch.position;
                touchStartPos = Camera.main.ScreenToWorldPoint(touchStartPos);
                touchStartPos = new Vector2(gameObject.transform.position.x,gameObject.transform.position.y)-touchStartPos;
                isDragging = true; // Initialize the drag state.
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                // The touch is moving.
                //if (!isDragging)
                //{
                // This is the first frame of movement after the touch began.
                //  isDragging = true; // Set the drag state.
                //}
                //touchStartPos = touch.position;
                //touchStartPos = Camera.main.ScreenToWorldPoint(touchStartPos);
                Vector2 currentTouchPosition = touch.position;
                currentTouchPosition = Camera.main.ScreenToWorldPoint(currentTouchPosition);
                currentTouchPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) - currentTouchPosition;
                Vector2 diff = touchStartPos - currentTouchPosition;
                x = y = z = 0;

                if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y)+0.3f)
                {
                    if (diff.x > 0)
                    {
                        direction += 1;
                        isRightdrag = true;
                    }
                    else if (diff.x < 0)
                    {
                        direction -= 1;
                        isRightdrag = false;

                    }
                    direction = Mathf.Clamp(direction, 0, 2);
                    if (!blockX)
                    {
                        x = diff.x;
                    }
                    if (!blockY)
                    {
                        y = diff.y;
                    }
                    if (!blockZ)
                    {
                        z = transform.localPosition.z;
                    }
                    if (collisionIsOnLeft && x < 0)
                    {
                        x = 0;
                    }
                    else if (collisionIsOnRight && x > 0)
                    {
                        x = 0;
                    }
                    isDragging = false;
                    touch.phase = TouchPhase.Ended;
                    //canDrag = false;
                    //canDrag = true;
                    //*transform.position = transform.position + new Vector3(x, y, z);
                    //*touchStartPos = currentTouchPosition;
                    // canDestroy = true;

                    // You can perform actions while dragging here.
                    // For example, update an object's position or do other things.
                }
                else if (Mathf.Abs(diff.y) > Mathf.Abs(diff.x)+0.3f && diff.y>0)
                {
                    animator_player.SetBool("isJumping", true);
                    //collider2D_player.enabled=false;
                    isJumping = true;
                    isDragging = false;
                    touch.phase = TouchPhase.Ended;

                }
                else if (Mathf.Abs(diff.y) > Mathf.Abs(diff.x) + 0.3f && diff.y < 0)
                {
                    animator_player.SetBool("isSlipping", true);
                    //collider2D_player.enabled=false;
                    isSlipping = true;
                    isDragging = false;
                    touch.phase = TouchPhase.Ended;

                }
                else
                {
                    isDragging = true;
                }

            }
            else if (touch.phase == TouchPhase.Ended)
            {
                // The touch has ended.

                isDragging = false;
                // This means it was a tap (no movement).
                // Handle tap actions here.

            }
        }

        transform.position += MoveTo() * Time.deltaTime;
        if (direction == 0 && transform.position.x < distance * -1)
        {
            transform.position = new Vector3(distance * -1, transform.position.y, transform.position.z);
        }
        else if (direction == 2 && transform.position.x > distance)
        {
            transform.position = new Vector3(distance, transform.position.y, transform.position.z);
        }
        else if (direction == 1 && isRightdrag && transform.position.x > 0)
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }
        else if (direction == 1 && !isRightdrag && transform.position.x < 0)
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }

        UpdatePlayerSpeed();
    }

    Vector3 MoveTo()
    {
        Vector3 targetPosition = transform.position.y * Vector3.up;
        if (direction == 0 /*&& targetPosition.x - transform.position.x < 0*/)
        {
            targetPosition += Vector3.left * distance;
        }
        else if (direction == 2)
        {
            targetPosition += Vector3.right * distance;

        }

        Vector3 movement = Vector3.zero;

        movement.x = (targetPosition - transform.position).normalized.x * turnDirectionSpeed;
        movement.y = playerSpeed;
        movement.z = 0;
        return movement;
        //characterController.Move(movement * Time.deltaTime);
    }

    public void TurnToRunning()
    {
        if(isSlipping)
        {
            animator_player.SetBool("isSlipping", false);
            isSlipping = false;
        }
        else if(isJumping)
        {
            animator_player.SetBool("isJumping", false);
            //collider2D_player.enabled = true;
            isJumping = false;
        }
        
        /*animator_player.SetBool("isSlipping", false);
        isSlipping = false;*/

    }

   /* public void JumpingTurnToSlipping()
    {
        animator_player.SetBool("isJumping", false);
        isJumping = false;
    }
    public void SlippingTurnToJumping()
    {
        animator_player.SetBool("isSlipping", false);
        isSlipping = false;
    }*/

    public void UpdatePlayerSpeed()
    {
        if(canDrag)
        {
            timeCount_speed += Time.deltaTime;
            if(timeCount_speed>=updateSpeedTime)
            {
                playerSpeed += plusSpeedNum;
                timeCount_speed = 0;
            }
        }
    }
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision);
    }
    private void HandleCollision(Collision2D collision)
    {
        if (collision.transform.tag == "Wall")
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                Vector2 normal = contact.normal;

                if (normal == Vector2.left)
                {
                    collisionIsOnRight = true;

                }
                else if (normal == Vector2.right)
                {
                    collisionIsOnLeft = true;
                }
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collisionIsOnLeft = false;
        collisionIsOnRight = false;
    }*/
}




