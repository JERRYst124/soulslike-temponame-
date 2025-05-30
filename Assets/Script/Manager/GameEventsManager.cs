public class GameEventsManager : Singleton<GameEventsManager>
{
    public DialogueEvents dialogueEvents;
    protected override void Awake()
    {
        base.Awake();
        dialogueEvents = new DialogueEvents();
    }
}
