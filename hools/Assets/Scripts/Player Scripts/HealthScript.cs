using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour
{

    private EnemyAnimator enemyAnim;
    private NavMeshAgent navAgent;
    private EnemyController EnemyController;

    public float health = 100f;
    public bool isPlayer, isCannibal;
    private bool isDead;

    private EnemyAudio enemyAudio;

    private PlayerAttack playerAttack;
    private PlayerStats playerStats;

    public GameObject deathMenu;
    public GameObject UI;
    public GameObject crosshair;

    void Awake ()
    {
        if ( isCannibal )
        {
            enemyAnim = GetComponent<EnemyAnimator> ();
            EnemyController = GetComponent<EnemyController> ();
            navAgent = GetComponent<NavMeshAgent> ();

            // get enemy audio
            enemyAudio = GetComponentInChildren<EnemyAudio> ();
        }

        if ( isPlayer )
        {
            playerStats = GetComponent<PlayerStats> ();
            playerAttack = GetComponent<PlayerAttack> ();
            enemyAudio = GetComponentInChildren<EnemyAudio> ();
        }
    }

    public void AddHealth ( float healthToAdd )
    {
        if ( isPlayer )
        {
            var healthSum = healthToAdd + health;
            Debug.Log ( healthSum );
            if ( healthSum >= 100.0f )
                health = 100.0f;
            else
                health += healthToAdd;

            playerStats.DisplayHealthStats ( health );
        }
    }

    public void ApplyDamage ( float damage )
    {

        if ( isDead )
            return;

        if ( isPlayer && playerAttack.is_Blocking && playerStats.stamina > damage )
        {
            playerStats.stamina -= damage;
        }
        else
        {
            health -= damage;
        }

        if ( isPlayer )
        {
            playerStats.DisplayHealthStats ( health );
            enemyAudio.PlayAttackSound ();
        }

        if ( isCannibal )
        {
            if ( EnemyController.Enemy_State == EnemyState.PATROL )
            {
                EnemyController.chaseDistance = 1000f;
            }
            Debug.Log ( "Enemy HP: " + health );
        }

        if ( health <= 0f )
        {
            PlayerDied ();
            isDead = true;
        }
    }

    void PlayerDied ()
    {
        if ( isCannibal )
        {
            navAgent.velocity = Vector3.zero;
            navAgent.isStopped = true;
            EnemyController.enabled = false;

            enemyAnim.Dead ();
            GetComponentInChildren<CapsuleCollider> ().enabled = false;
            //Start Couroutine
            StartCoroutine ( DeadSound () );
            //Enemy Manager spawn more enemies
            EnemyManager.instance.EnemyDied ( true );
            PlayerAttack.ammo_revolver++;
            PlayerAttack.ammo_rifle++;

        }

        if ( isPlayer )
        {
            GameObject [] enemies = GameObject.FindGameObjectsWithTag ( Tags.ENEMY_TAG );
            for ( int i = 0; i<enemies.Length; i++ )
            {
                enemies [i].GetComponent<EnemyController> ().enabled = false;
                // call enemy manager to stop spawning
            }
            GetComponent<PlayerMovement> ().enabled = false;
            GetComponent<PlayerAttack> ().enabled = false;
            GetComponent<WeaponManager> ().GetCurrentSelectedWeapon ().gameObject.SetActive ( false );
            EnemyManager.instance.EnemyDied ( true );
        }

        if ( tag == Tags.PLAYER_TAG )
        {
            UI.SetActive ( false );
            crosshair.SetActive ( false );
            Cursor.lockState = CursorLockMode.None;
            RestartGame ();
        }
        else
        {
            Invoke ( "TurnOffGameObject", 10f );
            ScoreScript.scoreValue++;
        }
    }

    void RestartGame ()
    {
        deathMenu.SetActive ( true );
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
    }

    void TurnOffGameObject ()
    {
        gameObject.SetActive ( false );
    }

    IEnumerator DeadSound ()
    {
        yield return new WaitForSeconds ( 0.3f );
        enemyAudio.PlayDeadSound ();
    }
}
