using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponAim
{
    NONE,
    SELF_AIM,
    AIM
}

public enum WeaponFireType
{
    SINGLE,
    MULTIPLE
}

public enum WeaponBulletType
{
    BULLET,
    ARROW,
    NONE
}

public enum WeaponMeleeType
{
    AXE,
    FISTS
}

public class WeaponHandler : MonoBehaviour
{

    private Animator _anim;

    [SerializeField]
    private GameObject _muzzleFlash;

    [SerializeField]
    private AudioSource _shootSound, _reloadSound;

    public WeaponAim weaponAim;
    public WeaponFireType fireType;
    public WeaponBulletType bulletType;
    public WeaponMeleeType meleeType;
    public GameObject attackPoint;
    public GameObject attackPoint2;

    void Awake ()
    {
        _anim = GetComponent<Animator> ();
    }

    public void ShootAnimation ()
    {
        _anim.SetTrigger ( AnimationTags.SHOOT_TRIGGER );
    }

    public void Aim ( bool isPossibleToAim )
    {
        _anim.SetBool ( AnimationTags.AIM_PARAMETER, isPossibleToAim );
    }

    public bool IsIdle()
    {
        return _anim.GetCurrentAnimatorStateInfo(0).IsName("Idle");
    }

    void TurnOnMuzzleFlash ()
    {
        _muzzleFlash.SetActive ( true );
    }

    void TurnOffMuzzleFlash ()
    {
        _muzzleFlash.SetActive ( false );
    }

    void PlayShootSound ()
    {
        _shootSound.Play ();
    }

    void PlayReload ()
    {
        _reloadSound.Play ();
    }

    void TurnOnAttackPoint ()
    {
        attackPoint.SetActive ( true );
    }

    void TurnOffAttackPoint ()
    {
        if ( attackPoint.activeInHierarchy )
            attackPoint.SetActive ( false );
    }

        void TurnOnAttackPoint2 ()
    {
        attackPoint2.SetActive ( true );
    }

    void TurnOffAttackPoint2 ()
    {
        if ( attackPoint2.activeInHierarchy )
            attackPoint2.SetActive ( false );
    }
    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {

    }
}
