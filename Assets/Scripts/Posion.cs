using UnityEngine;

public class Posion : MonoBehaviour
{
    [SerializeField] private float heal;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out IHeal healeable))
            healeable.ApplyHeal(heal);
            Destroy(gameObject);
    }
}
