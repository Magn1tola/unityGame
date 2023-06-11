using System.Collections.Generic;
using UnityEngine;

public class AIBehavior : MonoBehaviour
{
    [SerializeField] private List<AIBehaviorNode> nodeList;

    private void Update() {
        foreach (AIBehaviorNode node in nodeList)
        {
            if (node.IsCompleted())
                continue;
            node.InProcess();
            break;
        }
    }

}

