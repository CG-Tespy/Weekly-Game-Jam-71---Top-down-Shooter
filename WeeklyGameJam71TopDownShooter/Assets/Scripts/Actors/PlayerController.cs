using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerController : MortalActor2D
{
	public bool canShoot 						{ get; set; }

	protected override void Awake()
	{
		base.Awake();
		canShoot = 								false;
	}

	protected override void Update()
	{
		base.Update();
		HandleMovement();
	}

	protected override void HandleMovement()
	{
		// Simple 2d movement.
		Vector2 horizontal = 					Vector2.right * Input.GetAxis("Horizontal");
		Vector2 vertical = 						Vector2.up * Input.GetAxis("Vertical");

		Vector2 movement = 						(horizontal + vertical) * moveSpeed * Time.deltaTime;
		Vector3 newPos = 						rigidbody.position + movement;

		rigidbody.MovePosition(newPos);
	}

	public override void Die()	
	{
		canMove = 					false;
		canShoot = 					false;
	}
}
