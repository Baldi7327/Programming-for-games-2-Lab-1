using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private float horzIn, vertIn;
    private Movement move;
    private Rigidbody rb;
    private bool jump = false;
    private bool grounded = true;

    // Start is called before the first frame update
    void Start()
    {

        move = GetComponent<Movement>();
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude > speed) rb.velocity = rb.velocity.normalized * speed;
        horzIn = Input.GetAxisRaw("Horizontal");
        vertIn = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.Space)) jump = true;
        else jump = false;
        if (jump && grounded)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            grounded = false;
        }

    }

    private void FixedUpdate()
    {

        rb.AddForce((transform.forward * vertIn + transform.right * horzIn) * speed * 10f);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6) grounded = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 6) grounded = true;
    }
}
