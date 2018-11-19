using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EnemyWave : MonoBehaviour 
{
	public UnityEvent AllEnemiesDead 						{ get; protected set; }
	public virtual int enemyCount 							{ get; protected set; }
	public virtual List<EnemyController> enemiesWithin 		{ get; protected set; }
	public bool activated 									{ get; protected set; }
	int enemiesDead = 										0;


	protected virtual void Awake()
	{
		enemiesWithin = 									new List<EnemyController>();
		AllEnemiesDead = 									new UnityEvent();
		activated = 										false;
		UpdateCachedValues();
	}

	protected virtual void Start()
	{
		WatchForEnemyDeaths();
	}

	protected virtual void Update()
	{
		UpdateCachedValues();
	}

	public void Activate()
	{
		foreach (EnemyController enemy in enemiesWithin)
		{
			enemy.health.value = 						enemy.health.maxValue;
			enemy.gameObject.SetActive(true);
		}

		enemiesDead = 									0;
		activated = 									true;
	}

	public void Deactivate()
	{
		foreach (EnemyController enemy in enemiesWithin)
			enemy.gameObject.SetActive(false);

		activated = 									false;
	}

	void UpdateCachedValues()
	{
		// The enemy count and array, specifically.
		//enemiesWithin = 								new List<EnemyController>(gameObject.GetComponentsInChildren<EnemyController>());
		// ^Won't work, despite how it should get the same results as the below algorithm.
		
		enemiesWithin.Clear();
		EnemyController enemy = 						null;
		foreach (Transform child in transform)
		{
			enemy = 									child.GetComponent<EnemyController>();
			if (enemy != null)
				enemiesWithin.Add(enemy);
		}
		
		enemyCount = 									enemiesWithin.Count;
	}

	void WatchForEnemyDeaths()
	{
		foreach (EnemyController enemy in enemiesWithin)
			enemy.Died.AddListener(OnEnemyDeath);
		
	}

	void OnEnemyDeath()
	{
		enemiesDead++;

		if (enemiesDead == enemiesWithin.Count)
			AllEnemiesDead.Invoke();

	}
}
