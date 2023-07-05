using UnityEngine;

public class EntitySpittle : EntityItem
{
    [SerializeField] private float speed = 1;
    
    private EntityPlayer _player;
    
    private float _direction;
    private float _timeToDestroy = 5f;

    private void Awake()
    {
        _player = FindObjectOfType<EntityPlayer>();
        _direction = (_player.transform.position.x > transform.position.x) ? 1f : -1f;
    }

    private void Update()
    {
        if (_timeToDestroy <= 0) Destroy(gameObject);
        UpdatePosition();
        _timeToDestroy -= Time.deltaTime;
    }

    private void UpdatePosition()
    {
        var position = transform.position;
        var newPosition = new Vector3(
            position.x + speed * Time.deltaTime * _direction,
            position.y,
            0);
        transform.position = newPosition ;
    }

    protected override void OnCollide(EntityPlayer player)
    {
        player.effectsController.AddEffect(new RatePoisonEffect());
        Destroy(gameObject);
    }
}
