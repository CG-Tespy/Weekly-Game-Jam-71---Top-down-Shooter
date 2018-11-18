using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public abstract class Actor2D : MonoBehaviour2D
{

	[SerializeField] float _moveSpeed = 			3;

	public virtual float moveSpeed 
	{
		get { return _moveSpeed; }
		set { _moveSpeed = value; }
	}

	public virtual bool canMove 
	{
		get; set;
	}

	protected override void Awake()
	{
		base.Awake();
		canMove = 					true;
	}

	protected override void Update()
	{
		base.Update();
		HandleMovement();
	}

	protected abstract void HandleMovement();
}
