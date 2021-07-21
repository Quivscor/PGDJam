using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class StoryEvent : MonoBehaviour
{
    public GameObject eventObject;
    public EventRewards[] rewards;
    public TextMeshProUGUI eventName;

    public bool wasPlayed = false;
    public void StartEvent()
    {
        CinematicController.Instance.StartCinematic();
        eventObject.SetActive(true);
    }

    public void EndEvent()
    {
        CinematicController.Instance.EndCinematic();
        wasPlayed = true;
        eventObject.SetActive(false);
    }

    public void ChosenOption(int option)
    {
        if(option >= rewards.Length)
        {
            Debug.LogError("MISSING REWARDS IN EVENT" + eventName.text);
            return;
        }

        StatsController.Instance.AddFaith(rewards[option].faith);
        StatsController.Instance.AddUpgradePoints(rewards[option].upgradePoints);
        StatsController.Instance.AddHp(rewards[option].hp);
        StatsController.Instance.AddMana(rewards[option].mana);

        EndEvent();
    }
}

[Serializable]
public struct EventRewards
{
    public float faith;
    public int upgradePoints;
    public float hp;
    public float mana;

}