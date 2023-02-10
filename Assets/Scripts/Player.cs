using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //player movement
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private float horzIn, vertIn;
    private Movement move;
    private Rigidbody rb;
    private bool jump = false;
    private bool grounded = true;

    //bullet stuff
    [SerializeField] private float bulletSpeed;
    private GameObject bulletSpawn;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletPos;
    private Quaternion playerLook;
    private PlayerCam playerCam; 
    private bool shoot = false;


    // Start is called before the first frame update
    void Start()
    {
        playerCam = FindObjectOfType<PlayerCam>();
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

        if (Input.GetKey(KeyCode.Mouse0))
        {
            shoot = true;
        }
    }

    private void FixedUpdate()
    {

        rb.AddForce((transform.forward * vertIn + transform.right * horzIn) * speed * 10f);
        
        if (shoot == true)
        {
            Shoot();
            shoot = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6) grounded = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 6) grounded = true;
    }

    private void Shoot()
    {
        GameObject bulletSpawn = Instantiate(bullet, bulletPos.transform.position, Quaternion.identity);
        bulletSpawn.GetComponent<Rigidbody>().velocity = playerCam.transform.forward * bulletSpeed;
        Destroy(bulletSpawn,3);
    }
}
