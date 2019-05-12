﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int MaxHealth = 5;
    public GameObject Particles;
    public GameObject BloodStain;
    public int Damage = 1;

    int currentHealth;
    bool isAlive=true;
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    Vector2 facingTo;

    void Start()
    {
        currentHealth = MaxHealth;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        if(isAlive) FollowPlayer();
        anim.SetFloat("LookX", facingTo.x);
        anim.SetFloat("LookY", facingTo.y);
    }
    private void FollowPlayer()
    {
        Vector2 position = rb.position;
        Vector2 newPosition = Vector2.MoveTowards(position, PlayerController.instance.gameObject.transform.position, 5 * Time.deltaTime);
        facingTo = (newPosition- position).normalized;

        rb.MovePosition(newPosition);
    }
    public void takeDamage(int n, Vector2 dir)
    {
        //knockback
        Vector2 pos = transform.position;
        Vector2 newPos = pos + dir;
        transform.position = newPos;
        
        //hp substraction
        currentHealth -= n;
        Instantiate(Particles, transform.position, Quaternion.identity);
        Instantiate(BloodStain, transform.position, Quaternion.identity);
        if (currentHealth == 0) Die(dir);
    }
    private void Die(Vector2 dir)
    {
        isAlive = false;
        rb.simulated = false;
        sr.flipX = (facingTo.x > 0)? true: false;
        sr.sortingOrder = -1;
        anim.Play("SkeletonDead");
    }
    public void Death()
    {
        anim.Play("SkeletonCorpse");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            PlayerController.instance.TakeDamage(Damage, facingTo);
    }
}
