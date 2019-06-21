using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    public Rigidbody2D rb;
    public float velocity;
    public float jumpForce;
    public float radius;
    public bool isOnGround;
    public LayerMask layerMask;
    public ConstantForce2D constantForce2D;

    void Start()
    {

    }

    void Update()
    {
        isOnGround = Physics2D.OverlapCircle(transform.position, radius, layerMask);

        if (isOnGround && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, (rb.velocity.y + 1) * jumpForce);
        }
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            ApplyForce();
        }
    }

    void ApplyForce()
    {
        float yVelocity = rb.velocity.y;
        float xInput = Input.GetAxis("Horizontal");
        float xForce = xInput * velocity * Time.deltaTime;

        if (xInput != 0)
        {
            Vector2 force = new Vector2(xForce, 0);
            rb.AddForce(force, ForceMode2D.Force);
        }

        if (isOnGround)
        {
            if(xInput > 0)
            {
                constantForce2D.torque = -1;
            }
            else if (xInput < 0)
            {
                constantForce2D.torque = 1;
            }
            else
            {
                constantForce2D.torque = 0;
            }
        }
        else
        {
            if(xInput > 0)
            {
                constantForce2D.torque = -8;
            }
            else if (xInput < 0)
            {
                constantForce2D.torque = 8;
            }
            else
            {
                constantForce2D.torque = 0;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
