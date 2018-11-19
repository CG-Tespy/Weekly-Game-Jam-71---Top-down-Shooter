using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public abstract class MortalActor2D : Actor2D, IDamageable<float>
{
	[SerializeField] FloatReference _health;
	[SerializeField] 
	[Tooltip("How long (in seconds) this stays invincible when its invincibility is triggered.")]
	float invinTime = 0;
	float invinTimer;
	public UnityEvent TookDamage 				{ get; set; }
	public UnityEvent Died 						{ get; set; }
	public FloatReference health
	{
		get 									{ return _health; }
		protected set 							{ _health = value; }
	}

	public virtual bool isInvincible 			{ get; set; }

	protected override void Awake()
	{
		base.Awake();
		isInvincible = 							false;
		TookDamage =	 						new UnityEvent();
		Died = 									new UnityEvent();
		Died.AddListener(Die);
	}

	protected override void Update()
	{
		base.Update();
		InvincibilityCountdown();
	}

	#region IDamageable Methods
	public bool TakeDamage(float damage, bool triggerInvin = false)
	{
		if (health.value > 0 && !isInvincible)
		{
			health.value -= 					damage;
			TookDamage.Invoke();

			if (health.value <= 0)
				Died.Invoke();

			if (triggerInvin)
			{
				// Grant invincibility, let the countdown begin.
				invinTimer = 					invinTime;
				isInvincible = 					true;
			}
			
			return true;
		}

		return false;
	}


	#endregion

	protected virtual void InvincibilityCountdown()
	{
		if (invinTimer <= 0)
		{
			isInvincible = 					false;
			spriteRenderer.enabled = 		true;
		}
		else 
		{
			invinTimer -= 					Time.deltaTime;
			spriteRenderer.enabled = 		!spriteRenderer.enabled;
			// ^ Flickering effect.
		}
	}

	public abstract void Die();
}
