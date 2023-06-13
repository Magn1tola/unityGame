using System.Collections.Generic;
using UnityEngine;

public class DropItems : MonoBehaviour
{
    private const float MinItemDistance = 0.1f;
    private const float MaxItemDistance = 1f;

    private const float DropPower = 2f;

    [SerializeField] private GameObject[] prefabs;

    private readonly List<GameObject> _items = new();
    private readonly List<Vector3> _targetPositions = new();

    private bool _isDropped;
    private float _x;

    private void Update()
    {
        if (_x > 1 || !_isDropped) return;

        _x += Time.deltaTime;

        for (var i = 0; i < _items.Count; i++)
        {
            if (!_items[i]) continue;

            _items[i].transform.position = CalculateItemCurrentPosition(_targetPositions[i], _x);
        }
    }

    private float GetYOffset(float xPosition) => Mathf.Sin(xPosition) * DropPower;

    private Vector3 CalculateItemCurrentPosition(Vector3 targetPosition, float currentTime) => new(
        Mathf.Lerp(transform.position.x, targetPosition.x, currentTime),
        Mathf.Lerp(transform.position.y, targetPosition.y, currentTime) + GetYOffset(Mathf.PI * currentTime),
        0
    );

    private Vector3 CalculateItemTargetPosition(int itemIndex)
    {
        var direction = itemIndex % 2 != 0 ? 1 : -1;

        var positionX = transform.position.x + Random.Range(MinItemDistance, MaxItemDistance) * itemIndex * direction;

        var raycastHit2D = Physics2D.Raycast(
            new Vector2(positionX, transform.position.y + 1f),
            Vector2.down, 1000f,
            LayerMask.GetMask("Ground")
        );

        var itemColliderSize = GetComponent<Collider2D>().bounds.size.y;
        var positionY = raycastHit2D.point.y + itemColliderSize / 2;

        return new Vector3(positionX, positionY, 0f);
    }

    public void Drop()
    {
        if (_isDropped) return;

        _isDropped = true;
        for (var i = 0; i < prefabs.Length; i++)
        {
            var item = Instantiate(prefabs[i], transform.position, new Quaternion(0, 0, 0, 0));
            _items.Add(item);
            _targetPositions.Add(CalculateItemTargetPosition(i));
        }
    }
}