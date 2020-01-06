using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private Transform _playerRoot, _lookRoot;

    [SerializeField]
    private bool _invert;

    [SerializeField]
    private bool _canUnlock = true;

    [SerializeField]
    private float _sensivity = 5f;

    [SerializeField]
    private int _smoothSteps = 10;

    [SerializeField]
    private float _smoothWeight = 0.4f;

    [SerializeField]
    private float _rollAngle = 10f;

    [SerializeField]
    private float _rollSpeed = 3f;

    [SerializeField]
    private Vector2 _defaultLookLimits = new Vector2 ( -70f, 80f );

    private Vector2 _lookAngles;
    private Vector2 _currentMouseLook;
    private Vector2 _smoothMove;

    private float _currentRollAngle;

    private int _lastLookFrame;

    private void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update ()
    {
        LockAndUnlockCursor ();

        if ( Cursor.lockState == CursorLockMode.Locked )
            LookAround ();
    }

    void LockAndUnlockCursor ()
    {
        if ( Input.GetKeyDown ( KeyCode.Escape ) )
        {
            if ( Cursor.lockState== CursorLockMode.Locked )
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    void LookAround ()
    {
        _currentMouseLook = new Vector2 ( Input.GetAxis ( MouseAxis.MOUSE_Y ),
            Input.GetAxis ( MouseAxis.MOUSE_X ) );

        _lookAngles.x += _currentMouseLook.x * _sensivity*( _invert ? 1f : -1f );
        _lookAngles.y += _currentMouseLook.y * _sensivity;

        _lookAngles.x = Mathf.Clamp ( _lookAngles.x, _defaultLookLimits.x, _defaultLookLimits.y );

        _currentRollAngle = Mathf.Lerp ( _currentRollAngle,
            Input.GetAxisRaw ( MouseAxis.MOUSE_X )*_rollAngle,
            Time.deltaTime * _rollSpeed );

        _lookRoot.localRotation = Quaternion.Euler ( _lookAngles.x, 0f, _currentRollAngle );
        _playerRoot.localRotation = Quaternion.Euler ( 0f, _lookAngles.y, 0f );


    }
}
