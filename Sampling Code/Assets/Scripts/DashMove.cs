using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMove : MonoBehaviour
{
    private Rigidbody2D rb;
    public float dashSpeedVertical;
    public float dashSpeedHorizontal;
    private float dashTime;
    public float startDashTime;
    private int direction;
    public float normalGravity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
    }

    void Update()
    {
        if (direction == 0){
            if (Input.GetKeyDown(KeyCode.G)){
                direction = 1;
            }
            else if (Input.GetKeyDown(KeyCode.J)){
                direction = 2;
            }
            else if (Input.GetKeyDown(KeyCode.Y)){
                direction = 3;
            }
            else if (Input.GetKeyDown(KeyCode.H)){
                direction = 4;
            }
        }else{
            if (dashTime <= 0){
                direction = 0;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
            } else {
                dashTime -= Time.deltaTime;
                //also what we can do here is camera shake and particles system
                //moving the character in the desired direction///////issue here 
                //also i you dont want the character to fly the the pannels use the contiuous collision detection on the rigid body
                if (direction == 1){
                    rb.gravityScale = 0;
                    transform.Translate(-Vector3.right * dashSpeedHorizontal);
                    rb.gravityScale = normalGravity;
                }
                else if (direction == 2){
                    rb.gravityScale = 0;
                    transform.Translate(Vector3.right*dashSpeedHorizontal);
                    rb.gravityScale = normalGravity;
                }
                else if (direction == 3){
                    rb.gravityScale = 0;
                    rb.velocity = Vector2.up * dashSpeedVertical;
                    rb.gravityScale = normalGravity;
                }else if (direction == 4){
                    rb.gravityScale = 0;
                    rb.velocity = Vector2.down * dashSpeedVertical;
                    rb.gravityScale = normalGravity;
                }
            }
        }
    }
}
