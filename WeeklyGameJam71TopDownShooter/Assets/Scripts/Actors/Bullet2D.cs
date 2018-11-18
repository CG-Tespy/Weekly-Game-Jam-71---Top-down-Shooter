using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Bullet2D : Actor2D
{
	[Tooltip("This bullet can damage objects on these layers.")]
	[SerializeField] LayerMask damageLayers;
	[Tooltip("This bullet is gone when colliding with anything within these layers.")]
	[SerializeField] LayerMask destroySelfOn;
	[SerializeField] float damage = 		5;

	public virtual Vector2 velocity 
	{
		get { return rigidbody.velocity; }
		set { rigidbody.velocity = value; }
	}

	protected override void HandleMovement()
	{
		velocity = 							velocity.normalized * moveSpeed;
	}

	protected override void OnCollisionEnter2D(Collision2D other)
	{
		base.OnCollisionEnter2D(other);

		if (damageLayers.ContainsLayer(other.gameObject.layer))
		{
			IDamageable<float> damageable = 		other.gameObject.GetComponent<IDamageable<float>>();

			if (damageable != null)
			{
				// Only the player gets invincibility frames when hit.
				bool damagingPlayer = 				other.gameObject.CompareTag("Player");
				damageable.TakeDamage(damage, damagingPlayer);
			}
		}

		if (destroySelfOn.ContainsLayer(other.gameObject.layer))
		{
			Destroy(this.gameObject);
		}
	}

}
