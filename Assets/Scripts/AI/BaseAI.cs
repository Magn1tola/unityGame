using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIBehavior))]
public class BaseAI : BaseCharacter
{
    private AIBehavior aiBehavior;
    private void Awake() {
        aiBehavior = GetComponent<AIBehavior>();
    }
}
