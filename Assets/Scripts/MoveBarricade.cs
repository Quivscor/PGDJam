using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBarricade : MapEvent
{
    public Transform[] barricades;
    public Transform[] startPositions;
    public Transform[] endPositions;

    public float lerpTime;

    public bool start = false;
    public bool end = false;

    private float time;

    private void Awake()
    {
        for(int i = 0; i < barricades.Length; i++)
        {
            barricades[i].position = startPositions[i].position;
        }
    }

    private void Update()
    {
        float t = Time.deltaTime;
        if (start)
        {
            time += t;
            for(int i = 0; i < barricades.Length; i++)
            {
                barricades[i].position = Vector3.Lerp(startPositions[i].position, endPositions[i].position, time / lerpTime);
            }
            if (time / lerpTime >= 1f)
                start = false;
        }
        if (end)
        {
            time += t;
            for (int i = 0; i < barricades.Length; i++)
            {
                barricades[i].position = Vector3.Lerp(endPositions[i].position, startPositions[i].position, time / lerpTime);
            }
            if (time / lerpTime >= 1f)
                end = false;
        }
    }

    public override void StartEvent()
    {
        start = true;
        time = 0;
    }

    public override void StopEvent()
    {
        end = true;
        time = 0;
    }
}
