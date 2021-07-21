using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : PlatformerActor
{
    private Character character;
    public Transform target;
    public float moveSpeed;

    public float damage;
    public float projectileForce;
    public Projectile projectile;
    public float attackCd;
    private float attackCdCurrent;

    private Vector2 originPoint;
    private bool isMoving = true;
    public float lockTime = 1.0f;
    private Vector2 oldPosition;
    private Vector2 moveTarget;
    public float minMoveRadius;
    public float maxMoveRadius;

    protected override void Awake()
    {
        base.Awake();

        character = GetComponent<Character>();
        GetComponentInChildren<PlayerDetector>().OnPlayerDetected += AggroOnPlayer;

        originPoint = (Vector2)this.transform.position;
        moveTarget = originPoint + Random.insideUnitCircle * Random.Range(minMoveRadius, maxMoveRadius);
    }

    private void Update()
    {
        if(target != null)
        {
            float time = Time.deltaTime;

            //attacking
            attackCdCurrent += time;
            if(attackCdCurrent >= attackCd)
            {
                attackCdCurrent = 0;
                Attack();
                isMoving = false;
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
        if (Vector2.Distance(moveTarget, (Vector2)this.transform.position) < .2f)
        {
            oldPosition = moveTarget;
            moveTarget = originPoint + Random.insideUnitCircle * Random.Range(minMoveRadius, maxMoveRadius);
        }
        Move((moveTarget - (Vector2)this.transform.position).normalized * moveSpeed * Time.deltaTime);
    }

    private void MoveAroundPlayer()
    {

    }

    public void AggroOnPlayer(Transform player)
    {
        target = player;
    }

    private void Attack()
    {
        Projectile p = Instantiate(projectile, this.transform.position, Quaternion.identity, null);
        p.Construct(damage, this.tag);
        p.GetComponent<Rigidbody2D>().AddForce((target.transform.position - this.transform.position).normalized * projectileForce);
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
