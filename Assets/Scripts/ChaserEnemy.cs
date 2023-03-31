using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserEnemy : Enemy
{
    private Rigidbody rb;
    [SerializeField] private float chaserSpeed;
    private Player player;
    static Transform playerPos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<Player>();
        Hub = FindObjectOfType<MainHub>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        playerPos = player.transform;
        rb.AddForce((playerPos.position - transform.position).normalized * chaserSpeed * 10f);


    }
}
