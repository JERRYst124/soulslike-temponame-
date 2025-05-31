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

    public event Action<string, string> onDisplayDialogue;
    public void DisplayDialogue(string actorName, string dialogue)
    {
        if (onDisplayDialogue != null)
            onDisplayDialogue?.Invoke(actorName, dialogue);
    }

    public event Action onEnableDialogueInteract;
    public void EnableDialogueInteract()
    {
        if (onEnableDialogueInteract != null)
            onEnableDialogueInteract?.Invoke();
    }
    public event Action onDisableDialogueInteract;
    public void DisableDialogueInteract()
    {
        if (onDisableDialogueInteract != null)
            onDisableDialogueInteract?.Invoke();
    }
}
