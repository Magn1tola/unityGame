using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItems : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private List<GameObject> items;

    private bool droped = false;
    private float x = 0;
    private float itemDistance = 0.5f;
    private void Update()
    {
        if (x < 1 && droped)
        {
            float direction = 1;
            int iteration = 0;
            x += Time.deltaTime;
            for (int i = 0; i < items.Count; i++)
            {
                direction *= -1;
                iteration++;
                if (items[i] == null)
                    continue;
                Vector3 position = new Vector3(transform.position.x + iteration * direction * itemDistance, transform.position.y + 0.7f, 0);
                items[i].transform.position = new Vector3(Mathf.Lerp(transform.position.x, position.x, x), position.y + GetYOffset(Mathf.PI * x), 0);
            }
        }
    }

    private float GetYOffset(float x)
    {
        return Mathf.Sin(x);
    }

    public void Drop()
    {
        if (droped)
            return;
        droped = true;
        foreach (GameObject prefab in prefabs)
        {
            GameObject item = Instantiate(prefab, transform.position, new Quaternion(0, 0, 0, 0));
            items.Add(item);
        }
    }


}
