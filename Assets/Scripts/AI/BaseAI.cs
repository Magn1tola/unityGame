using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAI : BaseCharacter
{
    protected GameObject player;
    [SerializeField] protected float moveSpeed = 10f;
    [SerializeField] protected float cooldownTime = 10f;
    protected float currentCooldown = 0;

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected virtual void Update()
    {
        UpdateCooldown();
    }

    protected virtual void Cooldown()
    {
        currentCooldown = cooldownTime;
    }
    protected virtual void UpdateCooldown()
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }
        if (currentCooldown < 0)
        {
            currentCooldown = 0f;
        }
    }
    protected virtual void MoveToTargetPosition(Vector2 position)
    {
        float directionX = (position.x > transform.position.x) ? 1f : -1f;
        rigidBody2D.velocity = new Vector2(moveSpeed * directionX, rigidBody2D.velocity.y);
        Flip();
    }

    protected override void Attack()
    {
        base.Attack();
        Cooldown();
    }
}
