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

    public MapEvent[] mapEvent;

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
        for(int i = 0; i < m_Enemies.Count; i++)
        {
            if (enemies[i] == null)
                continue;
            if (m_Enemies[i].Target == player)
                break;
        }
        if (mapEvent.Length > 0)
            foreach(MapEvent ev in mapEvent)
                ev.StartEvent();

        for (int i = 0; i < m_Enemies.Count; i++)
        {
            if (enemies[i] == null)
                continue;
            m_Enemies[i].Target = player;
        }

        targetGroup.AddMember(player, 5f, 10);
        for(int i = 0; i < m_Enemies.Count; i++)
        {
            if (enemies == null)
                continue;
            if (m_Enemies[i] is MonoBehaviour mb)
            {
                if (mb == null)
                    break;
                targetGroup.AddMember(mb.gameObject.transform, 4f, 10);
            }
        }
        camera.Priority = 30;
        MusicController.Instance.ToggleFightMusic(true);
    }

    public void TryEndCombat()
    {
        for (int i = 0; i < m_Enemies.Count; i++)
        {
            if (enemies[i] == null)
                continue;
            if (m_Enemies[i].Target == player)
                return;
        }
        MusicController.Instance.ToggleFightMusic(false);
        camera.Priority = 8;
        if (mapEvent.Length > 0)
            foreach (MapEvent ev in mapEvent)
                ev.StopEvent();
    }

    public void RemoveDeadEnemyFromGroup(Character c)
    {
        targetGroup.RemoveMember(c.transform);
        enemies.Remove(c.gameObject);
        m_Enemies.Remove(c.GetComponent<IEnemy>());
    }
}
