using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour {

	private AudioSource audioSource;

	[SerializeField]
	private AudioClip [] dieClip;

    [SerializeField]
    private AudioClip [] screamClip;
    
    [SerializeField]
	private AudioClip[] attackClip;

	// Use this for initialization
	void Awake () {
		audioSource = GetComponent<AudioSource>();
	}
	
	public void PlayScreamSound(float distanceToEnemy) {
        float normalizeVolume = SoundMapper ( distanceToEnemy );
        //print ( distanceToEnemy );
        audioSource.clip = screamClip[Random.Range ( 0, screamClip.Length )];
        audioSource.volume = normalizeVolume;
        audioSource.Play();
	}

	public void PlayDeadSound() {
		audioSource.clip = dieClip[Random.Range(0, attackClip.Length)];
		audioSource.Play();
	}

	public void PlayAttackSound() {
		audioSource.clip = attackClip[Random.Range(0, attackClip.Length)];
		audioSource.Play();
	}

    private float SoundMapper(float distance )
    {
        if ( distance >= 4 && distance < 18 )
            return 1.0f;
        else if ( distance >= 18 && distance < 21 )
            return 0.85f;
        else if ( distance >= 21 && distance < 34 )
            return 0.70f;
        if ( distance >= 34 && distance < 49 )
            return 0.55f;
        else
            return 0.35f;
    }
}
