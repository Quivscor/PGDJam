using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCombat : MonoBehaviour
{
    public static PlayerCombat Instance;
    // Start is called before the first frame update

    public float projectileForce;
    private float projectileRespawnTimeCurrent;
    List<Projectile> projectiles;
    public Projectile projectilePrefab;
    public Transform projectileHolder;
    public AudioClip crowSpawnSound;
    public AudioSource crowSpawnSource;
    private PlayerVisuals visuals;

    public float rotateSpeed;
    private float rotateAngle;
    public float rotateRadius;

    public float invulnerabilityTime;
    private float invulnerabilityTimeCurrent;

    public Action<float> TakeDamage;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void AddForceForRavens(float value)
    {
        projectileForce += value;
    }

    void Start()
    {
        projectiles = new List<Projectile>();
        visuals = GetComponent<PlayerVisuals>();
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.deltaTime;
        invulnerabilityTimeCurrent -= time;
        if(projectiles.Count < StatsController.Instance.MaxCrowsNumber)
            projectileRespawnTimeCurrent += time;
        if(projectileRespawnTimeCurrent >= StatsController.Instance.TimeToSpawnCrow)
        {
            projectileRespawnTimeCurrent = 0;
            projectiles.Add(Instantiate(projectilePrefab, projectileHolder));
            crowSpawnSource.PlayOneShot(crowSpawnSound);
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
            projectile.GetComponent<Rigidbody2D>().AddForce((inputs.mousePos - (Vector2)projectile.transform.position).normalized * projectileForce);
            //first apply force, then construct, so that rotation can be applied
            projectile.Construct(StatsController.Instance.CrowDamage, this.tag);
        }
    }

    public void SendTakeDamageEvent(float damage)
    {
        if (invulnerabilityTimeCurrent >= 0)
            return;

        visuals.SetDmg();
        TakeDamage?.Invoke(damage);
        invulnerabilityTimeCurrent = invulnerabilityTime;
    }

    public bool IsInvulnerable()
    {
        return invulnerabilityTimeCurrent > 0;
    }
}
