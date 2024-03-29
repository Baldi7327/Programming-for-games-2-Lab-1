using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float health;
    private float currentHealth;
    public MainHub Hub;

    void Start()
    {
        currentHealth = health;
        Hub = FindObjectOfType<MainHub>();
    }

    public void TakeDamage(float d)
    {
        if (currentHealth - d <= 0)
        {
            currentHealth = 0;
            Death();
        }else
        {
            currentHealth -= d;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            TakeDamage(1);
        }
    }

    private void Death()
    {
        Hub.Points ++;
        gameObject.SetActive(false);

    }

    public float hp
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

}
