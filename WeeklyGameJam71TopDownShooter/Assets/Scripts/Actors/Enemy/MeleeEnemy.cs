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

		// TODO: When the enemy gets close enough to the target, have it play its attacking animation.
	}

	protected override void OnCollisionEnter2D(Collision2D other)
	{
		base.OnCollisionEnter2D(other);
		
		// If this collided with the target, have it bounce back away from the target.
		bool hitTarget = 					other.transform == target;

		if (hitTarget)
		{
			Vector2 bounceDir = 			(transform.position - target.position).normalized;
			rigidbody.AddForce(bounceDir * bouncebackForce);

			// Let damaging the player be handled by DamageOnContact
			
		}
	}


}
