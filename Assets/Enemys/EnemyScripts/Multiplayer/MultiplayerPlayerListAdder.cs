using UnityEngine.AI;

public class MultiplayerPlayerListAdder : BotClass
{
    MultiplayerPlayerManager manager;

    private void Start()
    {
        manager = MultiplayerPlayerManager.instance;
        manager.players.Add(transform);
    }
}
