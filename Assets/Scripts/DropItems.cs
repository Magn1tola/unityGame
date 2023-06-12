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
            float direction = 1;
            var iteration = 0;
            x += Time.deltaTime;
            for (var i = 0; i < _items.Count; i++)
            {
                direction *= -1;
                iteration++;
                if (_items[i] == null)
                    continue;
                var position = new Vector3(transform.position.x + iteration * direction * itemDistance,
                    transform.position.y + 0.7f, 0);
                _items[i].transform.position = new Vector3(Mathf.Lerp(transform.position.x, position.x, x),
                    position.y + GetYOffset(Mathf.PI * x), 0);
            }
        }
    }

    private float GetYOffset(float x) => Mathf.Sin(x);

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