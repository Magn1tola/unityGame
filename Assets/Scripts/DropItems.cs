using System.Collections.Generic;
using UnityEngine;

public class DropItems : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    
    private readonly List<GameObject> _items = new();
    private readonly List<Vector3> _targetPositions = new();

    private const float MinItemDistance = 0.1f;
    private const float MaxItemDistance = 1f;
    private const float DropPower = 2f;

    private bool dropped;
    private float x;

    private void Update()
    {
        if (x > 1 || !dropped) return;
            
        x += Time.deltaTime;
        
        for (var i = 0; i < _items.Count; i++) 
        {
                if (_items[i] == null) continue;
                
                _items[i].transform.position = CalculateItemCurrentPosition(_targetPositions[i], x);
        }
    }

    private float GetYOffset(float xPosition) => Mathf.Sin(xPosition) * DropPower;

    private Vector3 CalculateItemCurrentPosition(Vector3 targetPosition, float currentTime)
    {
        return new Vector3(
            Mathf.Lerp(transform.position.x, targetPosition.x, x),
            Mathf.Lerp(transform.position.y, targetPosition.y, x) + GetYOffset(Mathf.PI * currentTime),
            0);
    }

    private Vector3 CalculateItemTargetPosition(int itemIndex)
    {
        int direction = (itemIndex % 2 != 0) ? 1 : -1;

        float positionX = transform.position.x + Random.Range(MinItemDistance, MaxItemDistance) * itemIndex * direction;

        RaycastHit2D raycastHit2D = Physics2D.Raycast(
            new Vector2(positionX, transform.position.y + 1f), 
            Vector2.down, 1000f, 
            LayerMask.GetMask("Ground"));


        float itemColliderSize = GetComponent<Collider2D>().bounds.size.y;
        float positionY = raycastHit2D.point.y + (itemColliderSize / 2);
        
        return new Vector3(positionX, positionY, 0f);
    }
    public void Drop()
    {
        if (dropped) return;
        
        dropped = true;
        for (var i = 0; i < prefabs.Length; i++)
        {
            var item = Instantiate(prefabs[i], transform.position, new Quaternion(0, 0, 0, 0));
            _items.Add(item);
            _targetPositions.Add(CalculateItemTargetPosition(i));
        }
    }
}