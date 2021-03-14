﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note: MonoBehaviour
{
    public int damage = 20;
    public float speed = 10f;
    public Rigidbody2D rb;
    public GameObject Damaged;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        gameObject.layer = LayerMask.NameToLayer("Weapon");
    }
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Instantiate(Damaged, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
