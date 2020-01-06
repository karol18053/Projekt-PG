using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour {

	public float damage = 2f;
	public float radius = 1f;
	public LayerMask layerMask;

	[SerializeField]
	private AudioSource audioSource;

	[SerializeField]
	private AudioClip[] sounds;

	void PlayHitSound() {
		audioSource.clip = sounds[Random.Range(0, sounds.Length)];
		audioSource.Play();
	}
	
	void Update () {
		
		Collider[] hits = Physics.OverlapSphere(transform.position, radius, layerMask);

		if(hits.Length > 0) 
		{
			//print("We touched: " + hits[0].gameObject.tag);
			if(hits[0].gameObject.tag == Tags.ENEMY_TAG)
				PlayHitSound();
			hits[0].gameObject.GetComponent<HealthScript>().ApplyDamage(damage);
			gameObject.SetActive(false);
		}
	}
}
