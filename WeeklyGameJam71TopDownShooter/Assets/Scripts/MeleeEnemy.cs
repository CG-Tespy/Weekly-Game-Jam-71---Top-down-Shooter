using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// This enemy just runs up to the player and causes damage through mere contact.
/// </summary>
public class MeleeEnemy : EnemyController
{	
	[SerializeField] float bouncebackForce = 	300;
	[SerializeField] float damage = 			10;

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void HandleMovement()
	{
		// Just keep following the target, if there is one.
		if (target == null || !canMove)
			return;

		Vector2 moveDir = 					(target.position - transform.position).normalized;
		Vector2 movement = 					moveDir * moveSpeed * Time.deltaTime;
		Vector2 newPos = 					rigidbody.position + movement;

		rigidbody.MovePosition(newPos);
	}

	protected override void OnCollisionEnter2D(Collision2D other)
	{
		// If this collided with the target, have it bounce back.
		bool hitTarget = 					other.transform == target;

		if (hitTarget)
		{
			Vector2 bounceDir = 			(transform.position - target.position).normalized;
			rigidbody.AddForce(bounceDir * bouncebackForce);

			IDamageable<float> damageable = other.gameObject.GetComponent<IDamageable<float>>();

			if (damageable != null)
			{
				damageable.TakeDamage(damage, triggerInvin: true);
				this.TakeDamage(damage, false); // Just for testing.
			}
		}
	}


}
