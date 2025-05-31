using System;
public class DialogueEvents
{
    public event Action<string> onEnterDialogue;

    public void EnterDialogue(string dialogue)
    {
        onEnterDialogue?.Invoke(dialogue);
    }

    public event Action onDialogueStarted;

    public void DialogueStarted()
    {
        if (onDialogueStarted != null) onDialogueStarted?.Invoke();
    }

    public event Action onDialogueFinished;
    public void DialogueFinished()
    {
        if (onDialogueFinished != null) onDialogueFinished?.Invoke();
    }

    public event Action<string> onDisplayDialogue;
    public void DisplayDialogue(string dialogue)
    {
        if (onDisplayDialogue != null)
            onDisplayDialogue?.Invoke(dialogue);
    }
}
