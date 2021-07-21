using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : PlatformerActor
{
    private Character character;
    public Transform target;

    public float damage;
    public float projectileForce;
    public Projectile projectile;
    public float attackCd;
    private float attackCdCurrent;

    private Vector2 oldPosition;
    private Vector2 moveTarget;
    private float moveTime;
    public float minMoveRadius;
    public float maxMoveRadius;

    private void Awake()
    {
        character = GetComponent<Character>();
        GetComponentInChildren<PlayerDetector>().OnPlayerDetected += AggroOnPlayer;
    }

    private void Update()
    {
        if(target != null)
        {
            float time = Time.deltaTime;
            //movement
            if(Vector2.Distance(moveTarget, (Vector2)this.transform.position) < .2f)
            {
                oldPosition = moveTarget;
                moveTarget = Random.insideUnitCircle * Random.Range(minMoveRadius, maxMoveRadius);
                moveTime = 0;
            }
            moveTime += time;
            transform.position = Vector2.Lerp(oldPosition, moveTarget, moveTime);

            //attacking
            attackCdCurrent += time;
            if(attackCdCurrent >= attackCd)
            {
                attackCdCurrent = 0;
                Attack();
            }
        }
        else
        {
            //movement
        }
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
}
