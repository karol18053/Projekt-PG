using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{
    private PlayerMovement _playerMovement;

    public float sprintSpeed = 10f;
    public float moveSpeed = 5f;
    public float crouchSpeed = 2f;

    private Transform _lookRoot;

    private float _standHeight = 1.6f;
    private float _crouchHeight = 1f;

    private bool _isCrouching;

    private PlayerFootsteps _playerFootsteps;

    private float _sprintVolume = 1f;
    private float _crouchVolume = 0.1f;
    private float _walkVolumeMin = 0.2f, _walkVolumeMax = 0.6f;

    private float _walkStepDistance = 0.4f;
    private float _sprintStepDistnace = 0.25f;
    private float _crouchStepDistance = 0.5f;

    private PlayerStats playerStats;
    public float sprintThreshold = 50f;

    private void Awake ()
    {
        _playerMovement = GetComponent<PlayerMovement> ();
        _lookRoot = transform.GetChild ( 0 );

        _playerFootsteps = GetComponentInChildren<PlayerFootsteps> ();
        playerStats = GetComponent<PlayerStats>();
    }

    private void Start ()
    {
        _playerFootsteps.volumeMin = _walkVolumeMin;
        _playerFootsteps.volumeMax = _walkVolumeMax;
        _playerFootsteps.stepDistance = _walkStepDistance;
    }

    // Update is called once per frame
    void Update ()
    {
        Sprint ();
        Crouch ();
    }

    void Sprint ()
    {
        if(playerStats.stamina > 0f) {
            if ( Input.GetKeyDown ( KeyCode.LeftShift ) && !_isCrouching )
            {
                _playerMovement.speed = sprintSpeed;

                _playerFootsteps.stepDistance = _sprintStepDistnace;
                _playerFootsteps.volumeMin = _sprintVolume;
                _playerFootsteps.volumeMax = _sprintVolume;
            }
        }

        if ( Input.GetKeyUp ( KeyCode.LeftShift ) && !_isCrouching )
        {
            _playerMovement.speed = moveSpeed;

            _playerFootsteps.stepDistance = _walkStepDistance;
            _playerFootsteps.volumeMin = _walkVolumeMin;
            _playerFootsteps.volumeMax = _walkVolumeMax;
        }

        if(Input.GetKey(KeyCode.LeftShift) && !_isCrouching) 
        {
            playerStats.stamina -= sprintThreshold * Time.deltaTime;
            if(playerStats.stamina <= 0f) 
            {
                playerStats.stamina = 0f;
                _playerMovement.speed = moveSpeed;
                _playerFootsteps.stepDistance = _walkStepDistance;
                _playerFootsteps.volumeMin = _walkVolumeMin;
                _playerFootsteps.volumeMax = _walkVolumeMax;
            }
            playerStats.DisplayStaminaStats();
        }
        else
        {
            if(playerStats.stamina != 100f) {
                playerStats.stamina += sprintThreshold / 3f * Time.deltaTime;
                playerStats.DisplayStaminaStats();

                if(playerStats.stamina > 100f)
                    playerStats.stamina = 100f;
            }
        }

    }

    void Crouch ()
    {
        if ( Input.GetKeyDown ( KeyCode.C ) )
        {
            if ( _isCrouching )
            {
                _lookRoot.localPosition = new Vector3 ( 0f, _standHeight, 0f );
                _playerMovement.speed = moveSpeed;

                _playerFootsteps.volumeMin = _walkVolumeMin;
                _playerFootsteps.volumeMax = _walkVolumeMax;
                _playerFootsteps.stepDistance = _walkStepDistance;

                _isCrouching = false;
            }
            else
            {
                _lookRoot.localPosition = new Vector3 ( 0f, _crouchHeight, 0f );
                _playerMovement.speed = crouchSpeed;

                _playerFootsteps.stepDistance = _crouchStepDistance;
                _playerFootsteps.volumeMin = _crouchVolume;
                _playerFootsteps.volumeMax = _crouchVolume;

                _isCrouching = true;
            }
        }
    }
}