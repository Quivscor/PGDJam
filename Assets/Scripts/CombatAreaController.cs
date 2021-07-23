using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CombatAreaController : MonoBehaviour
{
    public List<GameObject> enemies;
    private List<IEnemy> m_Enemies;
    private Transform player;

    private CinemachineVirtualCamera camera;
    private CinemachineTargetGroup targetGroup;

    public void Awake()
    {
        player = FindObjectOfType<PlayerCombat>().transform;

        camera = GetComponentInChildren<CinemachineVirtualCamera>();
        targetGroup = GetComponentInChildren<CinemachineTargetGroup>();
        targetGroup.transform.parent = null;
        targetGroup.transform.position = this.transform.position;

        m_Enemies = new List<IEnemy>();
        foreach(GameObject go in enemies)
        {
            IEnemy e;
            if (go.TryGetComponent(out e))
                m_Enemies.Add(e);
        }

        foreach(IEnemy e in m_Enemies)
        {
            MonoBehaviour mb = e as MonoBehaviour;

            e.OnAggroPlayer += TryStartCombat;
            e.OnUnAggroPlayer += TryEndCombat;

            if(mb != null)
            {
                Character c;
                if(mb.TryGetComponent(out c))
                {
                    c.OnDeath += RemoveDeadEnemyFromGroup;
                }
            }
        }
    }

    public void TryStartCombat()
    {
        foreach(IEnemy e in m_Enemies)
        {
            if (e.Target == player)
                break;
        }

        targetGroup.AddMember(player, 5f, 10);
        foreach(IEnemy e in m_Enemies)
        {
            if(e is MonoBehaviour mb)
            {
                targetGroup.AddMember(mb.gameObject.transform, 2f, 10);
            }
        }
        camera.Priority = 30;
        MusicController.Instance.ToggleFightMusic(true);
    }

    public void TryEndCombat()
    {
        foreach (IEnemy e in m_Enemies)
        {
            if (e.Target == player)
                return;
        }
        MusicController.Instance.ToggleFightMusic(false);
        camera.Priority = 8;
    }

    public void RemoveDeadEnemyFromGroup(Character c)
    {
        targetGroup.RemoveMember(c.transform);
    }
}
