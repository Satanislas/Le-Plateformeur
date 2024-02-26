using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MarioController : MonoBehaviour
{
    public float acceleration;
    public float deceleration;
    public float decelerationMidAir;
    private float maxSpeed;
    public float MaxSpeed;
    public float jumpForce;
    public float jumpHoldForce;
    public float jumpRaycastLength;
    public float headRaycastLength;
    private float horizontalMovementInput;
    private Rigidbody rb;
    private Collider col;
    private bool isGrounded;
    public GameObject Model;
    private Vector3 ModelStartPosition;
    private Quaternion ModelStartRotation;
    private Animator anim;
    private bool isDead;
    private float deathX;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        ModelStartPosition = Model.transform.localPosition;
        ModelStartRotation = Model.transform.localRotation;
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            transform.position = new Vector3(deathX, transform.position.y, transform.position.z);
            return;
        }

        if (GameManager.gm.GameTimer <= 0)
        {
            KillMario();
        }
        
        UpdateAnimations();
        Model.transform.localPosition = ModelStartPosition;
        Model.transform.localRotation = ModelStartRotation;
        //movement input update
        horizontalMovementInput = Input.GetAxis("Horizontal");

        //jump input
        Vector3 StartPoint = transform.position;
        isGrounded = Physics.Raycast(StartPoint,Vector3.down,col.bounds.extents.y + 0.3f);
        

        if (Input.GetKeyDown(KeyCode.Space))
        { 
            if (isGrounded)
            {
                Jump();
            }
        }

        if (Input.GetKey(KeyCode.Space) && !isGrounded && rb.velocity.y > 0)
        {
            HoldJump();
        }

        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            StopMario();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            maxSpeed = MaxSpeed;
        }
        else
        {
            maxSpeed = MaxSpeed / 2;
        }

        //raycasts
        Debug.DrawRay(transform.position + (col.bounds.extents.y + 0.03f) * Vector3.down, Vector3.down * jumpRaycastLength,Color.red);
        Debug.DrawRay(transform.position + (col.bounds.extents.y + 0.03f) * Vector3.up,Vector3.up * headRaycastLength, Color.magenta);

        RaycastHit hit;
        Ray ray = new Ray(transform.position + (col.bounds.extents.y + 0.03f) * Vector3.up,Vector3.up);
        if (Physics.Raycast(ray, out hit, headRaycastLength))
        {
            Block block = hit.collider.GetComponent<Block>();
            if (block is not null)
            {
                block.GetHit(gameObject);
            }
        }
    }

    private void UpdateAnimations()
    {
        anim.SetBool("jump", !isGrounded);
        anim.SetFloat("speed",Math.Abs(rb.velocity.x / MaxSpeed));
    }

    private void HoldJump()
    {
        rb.velocity += Vector3.up * jumpHoldForce;
    }

    private void StopMario()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x * deceleration, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector3(rb.velocity.x * decelerationMidAir, rb.velocity.y);
        }
    }

    private void FixedUpdate()
    {
        //updating horizontal movement
        rb.velocity += Vector3.right * (horizontalMovementInput * Time.fixedDeltaTime * acceleration);
        
        //Clamping the speed on the horizontal axis
        if (Math.Abs(rb.velocity.x) > maxSpeed)
        {
            Vector3 newVelocity = rb.velocity;
            newVelocity.x = maxSpeed * Math.Sign(rb.velocity.x);
            rb.velocity = newVelocity;
        }

        //rotate mario
        if (rb.velocity.x >= 0)
        {
            transform.rotation = new Quaternion(0,0,0,0);
        }
        else
        {
            transform.rotation = new Quaternion(0,180,0,0);
        }
    }

    private void Jump()
    {
        if (!isGrounded) return;

        rb.velocity = new Vector3(rb.velocity.x, jumpForce);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Killer"))
        {
            KillMario();
        }

        if (other.CompareTag("Goal"))
        {
            GameManager.Win();
        }
    }

    public void KillMario()
    {
        isDead = true;
        isGrounded = true;
        deathX = transform.position.x; // prevents a bug where mario just drag on the floor
        anim.SetBool("jump", false);
        anim.SetFloat("speed",0);
        anim.SetBool("Dead", true);
        GameManager.EndGame();
    }
}
