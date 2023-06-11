using UnityEngine;

public class AIBehaviorNode : MonoBehaviour
{
    private bool сompleted = false;
    
    public bool IsCompleted()
    {
        return сompleted;
    }

    public void ReloadTask()
    {
        сompleted = false;
    }
    protected void CompleteNode()
    {
        сompleted = true;
    }
    protected virtual bool CheckCondition()
    {
        return true;
    }
    public virtual void InProcess()
    {
        if (!CheckCondition())
            return;
    }
}
