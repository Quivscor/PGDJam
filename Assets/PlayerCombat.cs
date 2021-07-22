using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCombat : MonoBehaviour
{
    // Start is called before the first frame update

    public float projectileForce;
    private float projectileRespawnTimeCurrent;
    List<Projectile> projectiles;
    public Projectile projectilePrefab;
    public Transform projectileHolder;

    public float rotateSpeed;
    private float rotateAngle;
    public float rotateRadius;

    public Action<float> TakeDamage;

    void Start()
    {
        projectiles = new List<Projectile>();
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.deltaTime;
        if(projectiles.Count < StatsController.Instance.MaxCrowsNumber)
            projectileRespawnTimeCurrent += time;
        if(projectileRespawnTimeCurrent >= StatsController.Instance.TimeToSpawnCrow)
        {
            projectileRespawnTimeCurrent = 0;
            projectiles.Add(Instantiate(projectilePrefab, projectileHolder));
        }

        rotateAngle += rotateSpeed * time;
        for (int i = 0; i < projectiles.Count; i++)
        {
            Vector3 offset = new Vector3(Mathf.Sin(rotateAngle + i), Mathf.Cos(rotateAngle + i), 0) * rotateRadius;
            projectiles[i].transform.position = projectileHolder.position + offset;
        }

        RegisteredInputs inputs = PlayerInput.GetPlayerInput();

        if(inputs.fire)
        {
            if (projectiles.Count == 0)
                return;

            Projectile projectile = projectiles[0];
            projectiles.RemoveAt(0);
            projectile.transform.parent = null;
            projectile.Construct(StatsController.Instance.CrowDamage, this.tag);
            projectile.GetComponent<Rigidbody2D>().AddForce((inputs.mousePos - (Vector2)this.transform.position).normalized * projectileForce);
        }
    }

    public void SendTakeDamageEvent(float damage)
    {
        TakeDamage?.Invoke(damage);
    }
}
