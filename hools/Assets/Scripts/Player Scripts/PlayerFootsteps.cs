using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{

    private AudioSource _footstepSound;

    [SerializeField]
    public AudioClip [] footstepClip;

    private CharacterController _characterController;

    [HideInInspector]
    public float volumeMin, volumeMax;

    private float _accumulatedDistance;

    [HideInInspector]
    public float stepDistance;

    private void Awake ()
    {
        _footstepSound = GetComponent<AudioSource> ();
        _characterController = GetComponentInParent<CharacterController> ();
    }

    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {
        CheckToPlayFootstepSound ();
    }

    void CheckToPlayFootstepSound ()
    {
        if ( !_characterController.isGrounded )
            return;

        if ( _characterController.velocity.sqrMagnitude > 0 )
        {
            _accumulatedDistance += Time.deltaTime;

            if ( _accumulatedDistance > stepDistance )
            {
                _footstepSound.volume = Random.Range ( volumeMin, volumeMax );
                _footstepSound.clip = footstepClip [Random.Range ( 0, footstepClip.Length )];
                _footstepSound.Play ();

                _accumulatedDistance = 0f;

            }
        }
        else
        {
            _accumulatedDistance = 0f;
        }
    }
}
