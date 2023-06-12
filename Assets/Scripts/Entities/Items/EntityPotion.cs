using UnityEngine;

public abstract class EntityPotion : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.TryGetComponent(out EntityPlayer player))
		{
			AddEffect(player);
			
			Destroy(gameObject);
		}
	}

	protected abstract void AddEffect(EntityPlayer player);
}