using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _characterController;
    private HealthScript healthScript;
    private GameObject water;
    private Vector3 _moveDirection;

    private float _gravity = 4 * 9.81f;
    private float _verticalVelocity;

    public float speed = 5f;
    public float jumpForce = 10;

    float minSurviveFall = 0.8f;
    float damageForSeconds = 45f;
    float airTime = 0f;

    private void Awake ()
    {
        _characterController = GetComponent<CharacterController> ();
        healthScript = GetComponent<HealthScript> ();
    }

    void Update ()
    {
        MoveThePlayer ();
    }

    void MoveThePlayer ()
    {
        _moveDirection = new Vector3 ( Input.GetAxis ( Axis.HORIZONTAL ),
            0f,
            Input.GetAxis ( Axis.VERTICAL ) );

        _moveDirection = transform.TransformDirection ( _moveDirection );

        _moveDirection *= speed*Time.deltaTime;

        ApplyGravity ();

        _characterController.Move ( _moveDirection );
    }

    void ApplyGravity ()
    {
        if ( _characterController.isGrounded )
        {
            _verticalVelocity -= _gravity * Time.deltaTime;

            PlayerJump ();

            if ( airTime > minSurviveFall )
            {
                float damage = damageForSeconds * airTime;
                healthScript.ApplyDamage ( damage );
            }
            airTime = 0f;
        }
        else
        {
            _verticalVelocity -= _gravity * Time.deltaTime;
            airTime += Time.deltaTime;
        }

        _moveDirection.y = _verticalVelocity * Time.deltaTime;
    }

    void PlayerJump ()
    {
        if ( _characterController.isGrounded && Input.GetKeyDown ( KeyCode.Space ) )
        {
            _verticalVelocity = jumpForce;
        }
    }
}
