using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float speed = 3f;

    private Rigidbody2D _rigidbody2D;
    private bool canMoving;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        if (_target == null)
            _target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (canMoving)
            _rigidbody2D.velocity = (_target.transform.position - transform.position) * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != _target)
            return;

        canMoving = false;
        _rigidbody2D.velocity = new Vector2(0f, 0f);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == _target)
            canMoving = true;
    }
}