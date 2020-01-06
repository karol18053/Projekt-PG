using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	public static EnemyManager instance;

	[SerializeField]
	private GameObject cannibalPrefab;

	[SerializeField]
	private int cannibalEnemyCount;

	private int initialCannibalCount;

	public Transform[] cannibalSpawnPoints;

	public float waitBeforeEnemySpawn = 10f;
	private int index = 0;

	void Awake () {
		MakeInstance();
	}

	void Start() {
		initialCannibalCount = cannibalEnemyCount;

		SpawnEnemies();

		StartCoroutine("CheckToSpawnEnemies");
	}

	void MakeInstance() {
		if(instance == null) {
			instance = this;
		}
	}

	void SpawnEnemies() {
		for (int i=0; i<cannibalEnemyCount; i++)
		{
			if (index >= cannibalSpawnPoints.Length) {
				index = 0;
			}
			Instantiate(cannibalPrefab, cannibalSpawnPoints[index].position, Quaternion.identity);
			index++;
		}
		cannibalEnemyCount = 0;
	}

	IEnumerator CheckToSpawnEnemies() {
		yield return new WaitForSeconds(waitBeforeEnemySpawn);
		SpawnEnemies();
		StartCoroutine("CheckToSpawnEnemies");
	}

	public void EnemyDied(bool cannibal) {
		if(cannibal) {
			cannibalEnemyCount++;
			if(cannibalEnemyCount > initialCannibalCount)
				cannibalEnemyCount = initialCannibalCount;
		}
	}

	public void StopSpawning(){
		StopCoroutine("CheckToSpawnEnemies");
	}
}
