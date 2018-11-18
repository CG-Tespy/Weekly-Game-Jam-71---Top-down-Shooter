using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public abstract class EnemyController : MortalActor2D
{
	[SerializeField] Transform _target;

	public virtual Transform target 
	{
		get { return _target; }
		set { _target = value; }
	}

	public override void Die()
	{
		gameObject.SetActive(false);
	}
}
