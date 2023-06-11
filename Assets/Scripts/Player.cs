using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Wallet))]
public class Player : BaseCharacter
{
	private Animator animator;

	private bool isFalling;
	private Wallet wallet;

	protected override void Start()
	{
		base.Start();
		animator = GetComponent<Animator>();
		wallet = GetComponent<Wallet>();
	}

	private void Update()
	{
		isFalling = !IsGrounded();
		animator.SetBool("isFalling", isFalling);

		if (Input.GetKeyDown(KeyCode.Space)) Attack();
	}

	public override bool IsGrounded() => Mathf.Abs(rigidBody2D.velocity.y) < 0.05F;

	protected override void ApplyDamage(float damage, GameObject instigator)
	{
		base.ApplyDamage(damage, instigator);
	}

	protected override void Attack()
	{
		base.Attack();
		animator.SetTrigger("Attack");
	}

	protected override void Dead()
	{
		base.Dead();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
	}
}