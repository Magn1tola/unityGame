using UnityEngine;

public class Poison : MonoBehaviour
{
	[SerializeField] private float heal;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.TryGetComponent(out IHeal healeable))
		{
			healeable.ApplyHeal(heal);
			Destroy(gameObject);
		}
	}
}