using System;
public class DialogueEvents
{
    public Action<string> onEnterDialogue;
    public void EnterDialogue(string dialogue)
    {
        onEnterDialogue?.Invoke(dialogue);
    }
}
