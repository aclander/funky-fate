﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private float attackRangeHorizontal;
    [SerializeField] private float attackRangeVertical;
    private GameSetupController gsc;
    [SerializeField] private Animator _anim;
    [SerializeField] private string button = "Attack";

    // Start is called before the first frame update
    void Start()
    {
        gsc = GameObject.Find("GameSetupController").GetComponent<GameSetupController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton(button))
        {
            Attack();
            _anim.SetBool("isAttacking", true);
        }
        if(Input.GetButtonUp(button))
        {
            _anim.SetBool("isAttacking", false);
        }
    }

    private void Attack()
    {
        List<Rigidbody2D> enemies = gsc.GetEnemies();
        List<Rigidbody2D> enemiesWithinRange = new List<Rigidbody2D>();

        float dirFacing = gameObject.transform.rotation.y;
        float ownX = gameObject.transform.position.x;
        float ownY = gameObject.transform.position.y;

        bool facingLeft = false;

        if (dirFacing == -1)
        {
            facingLeft = true;
        }

        // Find enemies within attack range
        foreach(Rigidbody2D enemy in enemies)
        {
            float enemyX= enemy.transform.position.x;
            float enemyY = enemy.transform.position.y;

            float dX = ownX - enemyX;
            float dY = ownY - enemyY;

            if (facingLeft)
            {
                if (dX > 0 && dX < attackRangeHorizontal && dY <= 0 && Math.Abs(dY) < attackRangeVertical)
                {
                    enemiesWithinRange.Add(enemy);
                }

            }
            else
            {
                if (dX < 0 && Math.Abs(dX) < attackRangeHorizontal && dY <= 0 && Math.Abs(dY) < attackRangeVertical)
                {
                    enemiesWithinRange.Add(enemy);
                }
            }
        }

        // Apply damage to enemies
        foreach(Rigidbody2D enemy in enemiesWithinRange)
        {
            enemy.GetComponent<Enemy>().TakeDamage(1, facingLeft);
        }
    }
}
