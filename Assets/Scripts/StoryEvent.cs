using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class StoryEvent : MonoBehaviour
{
    public GameObject eventHolder;
    public GameObject spritesHolder;
    public EventRewards[] rewards;
    public TextMeshProUGUI eventName;

    public bool isContinuation;
    public bool wasPlayed = false;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponentInChildren<AudioSource>();
    }
    public void StartEvent()
    {
        PlayerInput.BlockPlayerInput(true);
        CinematicController.Instance.StartCinematic();
        eventHolder.SetActive(true);
        spritesHolder.SetActive(false);
    }

    public void EndEvent()
    {
        CinematicController.Instance.EndCinematic();
        wasPlayed = true;
        eventHolder.SetActive(false);
        PlayerInput.BlockPlayerInput(false);
        
        audioSource.Play();

        if (isContinuation)
            EventsController.Instance.SetEventInSanctuarium(false);
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

        if (rewards[option].eventContinuation != null && !EventsController.Instance.IsEventInSanctuarium())
        {
            rewards[option].eventContinuation.SetActive(true);
            EventsController.Instance.SetEventInSanctuarium(true);
        }
     
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
    public GameObject eventContinuation;

}