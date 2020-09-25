using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class MultiplayerPlayerManager : BotClass
{
    public static MultiplayerPlayerManager instance;

    public List<Transform> players = new List<Transform>();

    private void Awake() => instance = this;
}
