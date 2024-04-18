using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb; 
    
    Vector3 direction;

    [SerializeField]
    float multiplier = 3f;

    [SerializeField] 
    float jumpForce = 400f;

    bool onGround = false;
    bool canDoubleJump = false; 
    // Start is called before the first frame update
    void Start()
    {
        direction = Vector3.zero; //{0,0,0} 
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += multiplier * velocity * Time.deltaTime; 
        if (rb.velocity.magnitude < 10)
        {
            rb.AddForce(direction*multiplier);
        }
    }
    void OnMove(InputValue value) 
    {
        // {x, y}
        Vector2 input = value.Get<Vector2>();
        float movementX = input.x;
        float movementZ = input.y;

        //rb.velocity = new Vector3(movementX, 0, movementZ); 
        direction = new Vector3(movementX, 0, movementZ); 
    }
    void OnJump()
    {
        Debug.Log("Jumping!"); 

        if(onGround)
        {
            Vector3 jumpForceV = new Vector3(0, jumpForce, 0);
            rb.AddForce(jumpForceV);
            onGround = false;
            canDoubleJump = true;
        }
        
        else if (canDoubleJump)
        {
            Vector3 jumpForceV = new Vector3(0, jumpForce, 0);
            rb.AddForce(jumpForceV);
            canDoubleJump = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            onGround = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            Debug.Log("Enter");
            onGround = true;
            canDoubleJump = false;
        }
    }

    private void OnCollisionExit(Collision collision)

    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            Debug.Log("Exit");
            onGround = false;
        }
    }

}
