using System.Collections.Generic;
using UnityEngine;

public class DropItems : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    
    private readonly List<GameObject> _items = new();
    
    private readonly float itemDistance = 0.5f;

    private bool droped;
    private float x;

    private void Update()
    {
        if (x < 1 && droped)
        {
            x += Time.deltaTime;
            for (var i = 0; i < _items.Count; i++)
            {
                if (_items[i] == null)
                    continue;

                _items[i].transform.position = CalculateItemCurrentPosition(CalculateItemTargetPosition(i), x);
            }
        }
    }

    private float GetYOffset(float x) => Mathf.Sin(x) * 2;

    private Vector3 CalculateItemCurrentPosition(Vector3 targetPosition, float currentTime)
    {
        return new Vector3(Mathf.Lerp(transform.position.x, targetPosition.x, x),
            Mathf.Lerp(transform.position.y, targetPosition.y, x) + GetYOffset(Mathf.PI * currentTime), 0);
    }

    private Vector3 CalculateItemTargetPosition(int itemIndex)
    {
        int direction = (itemIndex % 2 != 0) ? 1 : -1;

        float positionX = transform.position.x + itemDistance * itemIndex * direction;
        float positionY = transform.position.y + 1f;

        RaycastHit2D raycastHit2D = Physics2D.Raycast(new Vector2(positionX, positionY), Vector2.down);

        positionY = raycastHit2D.point.y + 0.2f;
        
            
        return new Vector3(positionX, positionY, 0f);
    }
    public void Drop()
    {
        if (droped)
            return;
        droped = true;
        foreach (var prefab in prefabs)
        {
            var item = Instantiate(prefab, transform.position, new Quaternion(0, 0, 0, 0));
            _items.Add(item);
        }
    }
}