using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ShopController : MonoBehaviour
{
    private static readonly int GlowLettersAnimation = Animator.StringToHash("isGlowLetters");

    [SerializeField] private ShopMenu menu;

    private Animator _animator;

    private EntityPlayer _player;

    private bool _staying;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<EntityPlayer>();
    }

    private void Update()
    {
        if (_staying)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                menu.Show();
                _player.RigidBody2D.velocity = new Vector2(0, _player.RigidBody2D.velocity.y);
            }
            else if (Input.GetKeyDown(KeyCode.Escape)) menu.Hide();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) _animator.SetBool(GlowLettersAnimation, true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) _animator.SetBool(GlowLettersAnimation, false);
    }

    private void OnTriggerStay2D(Collider2D other) => _staying = other.CompareTag("Player");
}