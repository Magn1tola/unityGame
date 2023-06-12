using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class EntityPhysicItem : EntityItem
{
    private Rigidbody2D rigidbody2D;
    private BoxCollider2D boxCollider2D;
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
}
