using UnityEngine;

public class MultiplayerBotStats : ScriptableObject
{
	[Range(0f, 10f)]
    public float stoppingDistance;
    [Range(0f, 1f)]
    public float jumpiness;
    [Range(0f, .5f)]
    public float jumpInnaccuracy;
    [Range(0f, .6f)]
    public float jumpDelay;
    [Range(-.4f, .4f)]
    public float jumpDelayRangeMin;
    [Range(-.4f, 0f)]
    public float jumpDelayRangeMax;
    [Range(0f, 20f)]
    public float shootInnaccuracy;
    [Range(60f, 90f)]
    public float fieldOfView;
}