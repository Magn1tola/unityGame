using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject target;

    [SerializeField] private Vector2 bounds = new Vector2(5f,5f);

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;
        float deltaX = target.transform.position.x - transform.position.x;
        if (deltaX > bounds.x || deltaX < -bounds.x)
        {
            if (transform.position.x < target.transform.position.x)
                delta.x = deltaX - bounds.x;
            else
                delta.x = deltaX + bounds.x;
        }
        
        float deltaY = target.transform.position.y - transform.position.y;
        if (deltaY > bounds.y || deltaY < -bounds.y)
        {
            if (transform.position.y < target.transform.position.y)
                delta.y = deltaY - bounds.y;
            else
                delta.y = deltaY + bounds.y;
        }

        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}