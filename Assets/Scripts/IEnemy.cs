using UnityEngine;

public delegate void CombatControllerEvent();

public interface IEnemy
{
    Transform Target { get; set; }

    event CombatControllerEvent OnAggroPlayer;
    event CombatControllerEvent OnUnAggroPlayer;
}