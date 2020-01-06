using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    [SerializeField]
    private WeaponHandler [] _weaponHandlers;

    private int _currentWeapoinIndex;


    // Use this for initialization
    void Start ()
    {
        _currentWeapoinIndex = 0;
        _weaponHandlers [_currentWeapoinIndex].gameObject.SetActive ( true );
    }

    // Update is called once per frame
    void Update ()
    {
        if ( Input.GetKeyDown ( KeyCode.Alpha1 ) )
        {
            TurnOnSelectedWeapon ( 0 ); //Fists
        }

        if ( Input.GetKeyDown ( KeyCode.Alpha2 ) )
        {
            TurnOnSelectedWeapon ( 1 ); //Axe
        }

        if ( Input.GetKeyDown ( KeyCode.Alpha3 ) )
        {
            TurnOnSelectedWeapon ( 2 ); //Revolver
        }

        if ( Input.GetKeyDown ( KeyCode.Alpha4 ) )
        {
            TurnOnSelectedWeapon ( 3 ); //assault rif
        }

        if ( Input.GetKeyDown ( KeyCode.Alpha5 ) )
        {
            TurnOnSelectedWeapon ( 4 ); //machete
        }
    }

    void TurnOnSelectedWeapon ( int weaponIndex )
    {
        if ( _currentWeapoinIndex != weaponIndex )
        {
            _weaponHandlers [_currentWeapoinIndex].gameObject.SetActive ( false );

            _weaponHandlers [weaponIndex].gameObject.SetActive ( true );

            _currentWeapoinIndex = weaponIndex;
        }
    }

    public WeaponHandler GetCurrentSelectedWeapon ()
    {
        return _weaponHandlers [_currentWeapoinIndex];
    }
}
