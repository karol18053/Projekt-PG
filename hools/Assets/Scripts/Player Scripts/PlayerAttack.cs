using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{

    private WeaponManager _weaponManager;

    public static int ammo_revolver = 6;
    public static int ammo_rifle = 10;

    public float damage_axe = 40f;
    public float damage_fist = 10f;
    public float damage;

    private float nextTimeToFire;

    private Animator zoomCameraAnim;
    private bool zoomed;

    private Camera mainCam;

    private GameObject crosshair;

    private bool is_Aiming;
    public bool is_Blocking;

    private PlayerStats playerStats;
    public GameObject ammoObject;
    Text currentAmmo;

    // Use this for initialization
    void Awake ()
    {
        _weaponManager = GetComponent<WeaponManager> ();
        playerStats = GetComponent<PlayerStats> ();

        zoomCameraAnim = transform.Find ( Tags.LOOK_ROOT ).transform.Find ( Tags.ZOOM_CAMERA ).GetComponent<Animator> ();
        crosshair = GameObject.FindWithTag ( Tags.CROSSHAIR );
        mainCam = Camera.main;
        currentAmmo = ammoObject.GetComponentInChildren<Text> ();
    }

    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {
        ShowAmmo ();
        WeaponShoot ();
        ZoomInAndOut ();
    }

    public void AddAmmo ( int ammoType )
    {
        switch ( ammoType )
        {
            case 2:
                ammo_revolver += (int) Random.Range ( 1, 4 );
                break;
            case 3:
                ammo_rifle += (int) Random.Range ( 1, 4 );
                break;
            default:
                break;
        }
    }

    void WeaponShoot ()
    {
        if ( _weaponManager.GetCurrentSelectedWeapon ().fireType == WeaponFireType.SINGLE )
        {
            if ( Input.GetMouseButtonDown ( 0 ) && _weaponManager.GetCurrentSelectedWeapon ().IsIdle () )
            {
                if ( _weaponManager.GetCurrentSelectedWeapon ().tag == Tags.AXE_TAG )
                {
                    _weaponManager.GetCurrentSelectedWeapon ().ShootAnimation ();
                }

                if ( _weaponManager.GetCurrentSelectedWeapon ().bulletType == WeaponBulletType.BULLET )
                {
                    if ( ammo_revolver > 0 )
                    {
                        _weaponManager.GetCurrentSelectedWeapon ().ShootAnimation ();
                        BulletFired ();
                        ammo_revolver--;
                    }
                }
                else
                {
                    if ( is_Aiming )
                    {
                        _weaponManager.GetCurrentSelectedWeapon ().ShootAnimation ();
                        if ( _weaponManager.GetCurrentSelectedWeapon ().bulletType == WeaponBulletType.ARROW )
                        {
                            // throw arrow
                        }
                        else
                        {
                            // throw spead
                        }
                    }
                    //spear or bow
                }
            }
        }
        else
        {
            if ( Input.GetMouseButton ( 0 ) && Time.time > nextTimeToFire )
            {
                if ( ammo_rifle > 0 )
                {
                    _weaponManager.GetCurrentSelectedWeapon ().ShootAnimation ();
                    BulletFired ();
                    ammo_rifle--;
                    nextTimeToFire = Time.time + 0.2f;
                }
            }
        }
    } // weapon shoot

    void ZoomInAndOut ()
    {
        if ( _weaponManager.GetCurrentSelectedWeapon ().weaponAim == WeaponAim.AIM )
        {
            if ( Input.GetMouseButtonDown ( 1 ) )
            {
                if ( _weaponManager.GetCurrentSelectedWeapon ().fireType == WeaponFireType.SINGLE )
                    zoomCameraAnim.Play ( AnimationTags.ZOOM_IN_ANIM );
                else
                    zoomCameraAnim.Play ( "ZoomInRifle" );

                crosshair.SetActive ( false );
            }
            if ( Input.GetMouseButtonUp ( 1 ) )
            {
                if ( _weaponManager.GetCurrentSelectedWeapon ().fireType == WeaponFireType.SINGLE )
                    zoomCameraAnim.Play ( AnimationTags.ZOOM_OUT_ANIM );
                else
                    zoomCameraAnim.Play ( "ZoomOutRifle" );
                crosshair.SetActive ( true );
            }
        }

        if ( _weaponManager.GetCurrentSelectedWeapon ().weaponAim == WeaponAim.SELF_AIM )
        {
            if ( Input.GetMouseButtonDown ( 1 ) && playerStats.stamina > 0f )
            {
                _weaponManager.GetCurrentSelectedWeapon ().Aim ( true );
                is_Aiming = true;
                is_Blocking = true;
            }

            if ( Input.GetMouseButtonUp ( 1 ) )
            {
                _weaponManager.GetCurrentSelectedWeapon ().Aim ( false );
                is_Aiming = false;
                is_Blocking = false;
            }

            if ( Input.GetMouseButton ( 1 ) )
            {
                playerStats.stamina -= 10 * Time.deltaTime;
                if ( playerStats.stamina <= 0f )
                {
                    playerStats.stamina = 0f;
                    _weaponManager.GetCurrentSelectedWeapon ().Aim ( false );
                    is_Aiming = false;
                    is_Blocking = false;
                }
                else
                {
                    _weaponManager.GetCurrentSelectedWeapon ().Aim ( true );
                    is_Aiming = true;
                    is_Blocking = true;
                }
            }
            playerStats.DisplayStaminaStats ();
        }
    }

    void BulletFired ()
    {

        RaycastHit hit;

        if ( Physics.Raycast ( mainCam.transform.position, mainCam.transform.forward, out hit ) )
        {
            if ( hit.transform.tag == Tags.ENEMY_TAG )
            {
                hit.transform.GetComponent<HealthScript> ().ApplyDamage ( damage );
            }
        }
    }

    void ShowAmmo ()
    {
        if ( _weaponManager.GetCurrentSelectedWeapon ().bulletType == WeaponBulletType.BULLET )
        {
            if ( _weaponManager.GetCurrentSelectedWeapon ().fireType == WeaponFireType.SINGLE )
            {
                currentAmmo.text = ammo_revolver.ToString ();
            }
            else
            {
                currentAmmo.text = ammo_rifle.ToString ();
            }
            ammoObject.SetActive ( true );
        }
        else
            ammoObject.SetActive ( false );
    }
}
