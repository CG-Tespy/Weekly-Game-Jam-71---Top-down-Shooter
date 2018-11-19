using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class RangedEnemy : EnemyController
{
	[Tooltip("How many shots are done per second.")]
	[SerializeField] float fireRate = 					1;
	[SerializeField] Bullet2D bulletPrefab;
	float firingTimer = 								0;
	public virtual bool canShoot 						{ get; set; }
	Vector2 moveDir = 									Vector2.right;

	protected override void Awake()
	{
		base.Awake();
		canShoot = 										true;
	}

	protected override void Update()
	{
		base.Update();
		HandleShooting();
		
	}

	protected override void HandleMovement()
	{
		// Just move horizontally.
		rigidbody.velocity = 			moveDir * moveSpeed;
	}

	protected virtual void HandleShooting()
	{
		if (bulletPrefab == null)
			return;

		if (firingTimer <= 0)
		{
			// Fire the shot.
			Vector2 shootDir = 			(target.position - transform.position).normalized;
			Vector2 spawnPos = 			rigidbody.position + shootDir;
			Bullet2D bullet = 			Instantiate<Bullet2D>(bulletPrefab, spawnPos, Quaternion.identity);

			// Make sure the bullet moves in the right direction, and it on the same layer as
			// this enemy.
			bullet.velocity = 			shootDir;
			bullet.gameObject.layer = 	this.gameObject.layer;

			firingTimer = 				fireRate;
		}
		else 
			firingTimer -= 				Time.deltaTime;
	}

	protected override void OnCollisionEnter2D(Collision2D other)
	{
		base.OnCollisionEnter2D(other);

		// Set the movement to be in the opposite direction it was before.
		moveDir = 			-moveDir;
	}
}
