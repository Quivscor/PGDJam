﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // Start is called before the first frame update

    public int maxProjectileCount;
    public float projectileForce;
    public float projectileRespawnTime;
    private float projectileRespawnTimeCurrent;
    List<Projectile> projectiles;
    public Projectile projectilePrefab;
    public Transform projectileHolder;

    public float rotateSpeed;
    private float rotateAngle;
    public float rotateRadius;

    void Start()
    {
        projectiles = new List<Projectile>();
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.deltaTime;
        projectileRespawnTimeCurrent += time;
        if(projectileRespawnTimeCurrent >= projectileRespawnTime && projectiles.Count < maxProjectileCount)
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
            projectile.Construct(0);
            projectile.GetComponent<Rigidbody2D>().AddForce((inputs.mousePos - (Vector2)this.transform.position).normalized * projectileForce);
        }
    }
}
