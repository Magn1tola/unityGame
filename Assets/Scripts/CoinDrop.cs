using UnityEngine;

public class CoinDrop : MonoBehaviour
{

    private Vector3 startPosition;
    [SerializeField] private Vector3 targetPosition;
    private float x = 0;
    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        if (x < 1)
        {
            transform.position = new Vector3( Mathf.Lerp(startPosition.x, targetPosition.x, x), targetPosition.y + GetYOffset(Mathf.PI * x) * Vector3.Distance(startPosition, targetPosition), 0);
            x += Time.deltaTime;
        }
    }
    
    public void SetTargetPosition(Vector3 position){
        targetPosition = position;
    }

    private float GetYOffset(float x)
    {
        return Mathf.Sin(x);
    }

}
