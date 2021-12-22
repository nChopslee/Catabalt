using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    //the max amount of time you can hold the jump button
    public float buttonTime;

    //how long the button has been held down
    public float jumpTime;

    //whether or not they are jumping.
    public bool jumping;

    //how high they can jump
    public float jumpAmount;

    //helpful for knowing if you are grounded or not.
    private float distanceToGround;

    Rigidbody2D rigidBody;

    public Animator anim;

    public GameObject jumpEffect;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("I'm a hero!");
        rigidBody = GetComponent<Rigidbody2D>();
        buttonTime = 0.3f;
        jumpAmount = 5f;
        jumping = false;
        jumpTime = 0;
        distanceToGround = GetComponent<Collider2D>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {


        if (IsGrounded())
        {
            
            anim.SetBool("Grounded", true);
            anim.SetBool("Jumping", false);
            anim.SetBool("Falling", false);
            //jumping = false;

        }

        //Let the player jump but only if they are grounded
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Debug.Log("space bar pressed!");
            jumping = true;
            jumpTime = 0;
            Instantiate(jumpEffect, new Vector2(transform.position.x, transform.position.y - 0.5f), jumpEffect.transform.rotation);
            anim.SetBool("Jumping", true);
            


            //float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rigidBody.gravityScale));
            //rigidBody.AddForce(transform.up * 100f, ForceMode2D.Impulse);
            //rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        //If the player is actively jumping,
        //keep the timer going
        if (jumping)
        {
            
            anim.SetBool("Grounded", false);
            anim.SetBool("Jumping", true);
            //change the velocity directly.
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpAmount);
            jumpTime += Time.deltaTime;
            
        }

        //If they let go of the jump button, or if they jump for as
        //long as they are allowed to.
        if(Input.GetKeyUp(KeyCode.Space) || jumpTime > buttonTime)
        {
            jumping = false;
            anim.SetBool("Jumping", false);
            //anim.SetBool("Falling", true);
        }
    }

    bool IsGrounded()
    {
        //hero is layer 8
        int layerMask = 1 << 8;

        //now layerMask is every layer BUT 8.
        layerMask = ~layerMask;

        //visualize the ray
        Debug.DrawRay(transform.position, new Vector3(0, -1 * (distanceToGround + 0.1f), 0), Color.green, 2, false);

        //See if we intersect anything that is immedietely below us (that specifically ISN'T us).
        return Physics2D.Raycast(transform.position, -Vector3.up, distanceToGround + 0.1f, layerMask);
    }

}
