using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpHeight = 5;
    [SerializeField] private float fallingThreshold = -0;

    [SerializeField] private int maxHp = 3;

    private int curHp;

    private bool isFalling = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        curHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {

        isFalling = (rb.velocity.y  < fallingThreshold);
        anim.SetBool("isFalling", isFalling);


        if (Input.GetKeyDown(KeyCode.Space))
            jump();
        if (Input.GetAxis("Horizontal") != 0)
            moveX(Input.GetAxis("Horizontal"));
    }

    void FixedUpdate()
    {
    }

    void moveX(float direction) {
        rb.velocity = new Vector2( direction * speed, rb.velocity.y);
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        flip();
    }


    void jump()
    {
        rb.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
        anim.SetTrigger("Jump");
    }

    void flip()
    {
        if (rb.velocity.x > 0)
            sr.flipX = false;
        if (rb.velocity.x < 0)
            sr.flipX = true;
    }
}
