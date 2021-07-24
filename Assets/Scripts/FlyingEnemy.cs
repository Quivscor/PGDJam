using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : PlatformerActor, IEnemy
{
    private Character character;
    public Transform Target { get; set; }
    public float moveSpeed;

    public float damage;
    public float projectileForce;
    public Projectile projectile;
    public float attackCd;
    private float attackCdCurrent;

    private Vector2 originPoint;
    private bool isMoving = true;
    public float lockTime = 1.0f;
    private float timeSinceLastMove;
    private float randomTime;
    private Vector2 moveTarget;
    public float minMoveRadius;
    public float maxMoveRadius;

    public event CombatControllerEvent OnAggroPlayer;
    public event CombatControllerEvent OnUnAggroPlayer;

    public PlayerDetector Detector { get; set; }

    protected override void Awake()
    {
        base.Awake();

        Detector = GetComponentInChildren<PlayerDetector>();

        character = GetComponent<Character>();
        Detector.OnPlayerDetected += AggroOnPlayer;
        Detector.OnPlayerLeaveAggroRange += UnAggro;

        originPoint = (Vector2)this.transform.position;
        moveTarget = originPoint + Random.insideUnitCircle * Random.Range(minMoveRadius, maxMoveRadius);
        randomTime = Random.Range(0, attackCd / 2f);
    }

    private void Update()
    {
        if(Target != null)
        {
            float time = Time.deltaTime;

            //attacking
            timeSinceLastMove += time;
            attackCdCurrent += time;
            if(attackCdCurrent >= attackCd)
            {
                attackCdCurrent = 0;
                Attack();
                isMoving = false;
                randomTime = Random.Range(0, attackCd/2f);
                StartCoroutine(UnlockMovement());
            }
            //movement
            else if (isMoving)
            {
                DecideAndMove();
            }
        }
        else
        {
            //movement
            DecideAndMove();
        }
    }

    private void DecideAndMove()
    {
        if (timeSinceLastMove >= randomTime || Vector2.Distance(moveTarget, transform.position) < .1f)
        {
            moveTarget = originPoint + Random.insideUnitCircle * Random.Range(minMoveRadius, maxMoveRadius);
            timeSinceLastMove = 0;
        }
        Move((moveTarget - (Vector2)this.transform.position).normalized * moveSpeed * Time.deltaTime);
    }

    public void AggroOnPlayer(Transform player)
    {
        Target = player;
        Detector.GetComponent<CircleCollider2D>().radius *= 2;
        OnAggroPlayer?.Invoke();
    }

    public void UnAggro()
    {
        Target = null;
        Detector.GetComponent<CircleCollider2D>().radius /= 2;
        OnUnAggroPlayer?.Invoke();
    }

    private void Attack()
    {
        Projectile p = Instantiate(projectile, this.transform.position, Quaternion.identity, null);
        p.Construct(damage, this.tag);
        p.GetComponent<Rigidbody2D>().AddForce((Target.transform.position - this.transform.position).normalized * projectileForce);
    }

    private IEnumerator UnlockMovement()
    {
        yield return new WaitForSeconds(lockTime);
        isMoving = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(moveTarget, .3f);
    }
}
