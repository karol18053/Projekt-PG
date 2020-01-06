using System;
using System.Timers;
using UnityEngine;

public class ArtifactScript : MonoBehaviour
{

    private bool isEnable = true;
    public float cooldownSeconds = 4.0f;
    private float cooldown = 0.0f;

    public ArtefactType artefactType;

    private HealthScript healthScript;
    private PlayerStats playerStats;
    private PlayerAttack playerAttack;
    private Collider meshCollider;

    float speed = 5f;
    float height = 0.03f;


    private void Awake ()
    {
        var player_obj = GameObject.Find ( Tags.PLAYER_TAG );

        healthScript = player_obj.GetComponent<HealthScript> ();
        playerStats = player_obj.GetComponent<PlayerStats> ();
        playerAttack = player_obj.GetComponent<PlayerAttack> ();

        meshCollider = GetComponent<Collider> ();

        isEnable = true;
    }

    void OnTriggerEnter ( Collider meshCollider )
    {
        var tag = meshCollider.gameObject.tag;
        if ( tag == "Player" )
        {
            switch ( artefactType )
            {
                case ArtefactType.HealthPotion:
                    healthScript.AddHealth ( UnityEngine.Random.Range ( 10, 25 ) );
                    break;
                case ArtefactType.StaminaPotion:
                    playerStats.AddStamina ( UnityEngine.Random.Range ( 10, 25 ) );
                    break;
                case ArtefactType.RevolverAmmo:
                    playerAttack.AddAmmo ( (int) ArtefactType.RevolverAmmo );
                    break;
                case ArtefactType.CarabinAmmo:
                    playerAttack.AddAmmo ( (int) ArtefactType.CarabinAmmo );
                    break;
                default:
                    break;
            }

            isEnable = false;
            
            transform.GetChild(0).gameObject.SetActive ( false );
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if ( isEnable )
        {
            //Debug.Log ( Time.deltaTime );
            transform.RotateAround ( transform.position, Vector3.up, 30 * Time.deltaTime );
            Vector3 pos = transform.position;
            float newY = Mathf.Sin ( Time.time * speed );

            transform.position = new Vector3 ( pos.x, pos.y+newY*height, pos.z );
        }
        else
        {
            if ( cooldown <= cooldownSeconds )
                cooldown += Time.deltaTime;
           
            else
            {
                cooldown = 0.0f;
                isEnable = true;
                transform.GetChild ( 0 ).gameObject.SetActive ( true );
            }
        }
    }

    public enum ArtefactType
    {
        HealthPotion = 0,
        StaminaPotion = 1,
        RevolverAmmo = 2,
        CarabinAmmo = 3
    }
}
