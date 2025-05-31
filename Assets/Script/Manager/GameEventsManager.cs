public class GameEventsManager : Singleton<GameEventsManager>
{
    public DialogueEvents dialogueEvents;
    public PlayerEvents playerEvents;
    protected override void Awake()
    {
        base.Awake();
        playerEvents = new PlayerEvents();
        dialogueEvents = new DialogueEvents();
    }
}
