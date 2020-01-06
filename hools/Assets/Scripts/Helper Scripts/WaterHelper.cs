using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Helper_Scripts
{
    class WaterHelper: MonoBehaviour
    {
        HealthScript healthScript;
        Collider meshCollider;

        private void Awake ()
        {
            var player_obj = GameObject.Find ( Tags.PLAYER_TAG ) ;

            healthScript = player_obj.GetComponent<HealthScript>();
            meshCollider = GetComponent<Collider> ();

        }

        void OnTriggerEnter ( Collider meshCollider )
        {
            var tag = meshCollider.gameObject.tag;
            if ( tag == "Player" || tag == "Cannibal" )
                healthScript.ApplyDamage ( 150 );
        }
    }
}
