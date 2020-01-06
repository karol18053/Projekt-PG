using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState {
	PATROL,
	CHASE,
	ATTACK,
	DEATH
}

public class EnemyController : MonoBehaviour {

	private EnemyAnimator enemyAnim;
	private NavMeshAgent navAgent;

	private EnemyState enemyState;

	public float walkSpeed = 0.45f;
	public float runSpeed = 4f;
	public float chaseDistance = 7f;
	private float currentChaseDistance;
	public float attackDistance = 2.2f;
	public float chaseAfterAttack = 2f;
	public float patrolRadiusMin = 20f, patrolRadiusMax = 60f;
	public float patrolForThisTime = 15f;
	private float patrolTimer;
	public float waitBeforeAttack = 2f;
	private float attackTimer;

	private Transform target;

	public GameObject attackPoint;

	private EnemyAudio enemyAudio;

	void Awake()
	{
		enemyAnim = GetComponent<EnemyAnimator>();
		navAgent = GetComponent<NavMeshAgent>();

		target = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;
		enemyAudio = GetComponentInChildren<EnemyAudio>();
	}

	// Use this for initialization
	void Start () 
	{
		 enemyState = EnemyState.PATROL;
		 patrolTimer = patrolForThisTime;
		 attackTimer = waitBeforeAttack;
		 currentChaseDistance = chaseDistance;
	}
	
	// Update is called once per frame
	void Update () {
		if(enemyState == EnemyState.PATROL)
		{
			Patrol();
		}

		if(enemyState == EnemyState.CHASE)
		{
			Chase();
		}

		if(enemyState == EnemyState.ATTACK)
		{
			Attack();
		}

		if(enemyState == EnemyState.DEATH)
		{
			Death();
		}
		
	}

	void Patrol() 
	{
		navAgent.isStopped = false;
		navAgent.speed = walkSpeed;

		patrolTimer += Time.deltaTime;

		if(patrolTimer > patrolForThisTime)
		{
			SetRandomDestination();
			patrolTimer = 0f;
		}

		if(navAgent.velocity.sqrMagnitude > 0)
		{
			enemyAnim.Walk(true);
		} else {
			enemyAnim.Walk(false);
		}
        var distance = Vector3.Distance ( transform.position, target.position );

        if ( distance <= chaseDistance )
        {
            enemyAnim.Walk ( false );
            enemyState = EnemyState.CHASE;

            enemyAudio.PlayScreamSound ( distance );
        }
    }

    void Chase()
	{
		navAgent.isStopped = false;
		navAgent.speed = runSpeed;
		// player position is destination
		navAgent.SetDestination(target.position);

		if(navAgent.velocity.sqrMagnitude > 0)
		{
			enemyAnim.Run(true);
		} else {
			enemyAnim.Run(false);
		}

		if (Vector3.Distance(transform.position, target.position) <= attackDistance)
		{
			enemyAnim.Run(false);
			enemyAnim.Walk(false);
			enemyState = EnemyState.ATTACK;

			// reset to previous value
			if (chaseDistance != currentChaseDistance)
			{
				chaseDistance = currentChaseDistance;
			}
		}
		else if (Vector3.Distance(transform.position, target.position) > chaseDistance)
		{
			// player running away from enemy
			enemyAnim.Run(false);
			enemyState = EnemyState.PATROL;
			patrolTimer = patrolForThisTime;

			if (chaseDistance != currentChaseDistance)
			{
				chaseDistance = currentChaseDistance;
			}
		}
	}

	void Attack()
	{
		navAgent.velocity = Vector3.zero;
		navAgent.isStopped = true;

		attackTimer += Time.deltaTime;

		if(attackTimer > waitBeforeAttack)
		{
			enemyAnim.Attack();
			attackTimer = 0f;
			//enemyAudio.PlayAttackSound();
		}

		if (Vector3.Distance(transform.position, target.position) > attackDistance + chaseAfterAttack)
		{
			enemyState = EnemyState.CHASE;
		}
	}

	void Death()
	{
		// trigger death
		enemyAnim.Dead();
	}

	void SetRandomDestination()
	{
		float randRadius = Random.Range(patrolRadiusMin, patrolRadiusMax);
		Vector3 randDir = Random.insideUnitSphere * randRadius;
		randDir += transform.position;

		NavMeshHit navHit;
		NavMesh.SamplePosition(randDir, out navHit, randRadius, -1);

		navAgent.SetDestination(navHit.position);
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

    public EnemyState Enemy_State
    {
    	get; set;
    }
}
