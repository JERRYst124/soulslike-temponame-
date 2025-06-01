
using Ink.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;
public class DialogueManager : Singleton<DialogueManager>
{
    [Header("Ink Story")]
    [SerializeField] private TextAsset InkJson;
    private Story story;

    private bool dialoguePlaying = false;

    protected override void Awake()
    {
        base.Awake();
        story = new Story(InkJson.text);
    }
    private void OnEnable()
    {
        GameEventsManager.Instance.dialogueEvents.onEnterDialogue += EnterDialogue;
        GameEventsManager.Instance.dialogueEvents.onEnableDialogueInteract += EnableDialogueInteract;
        GameEventsManager.Instance.dialogueEvents.onDisableDialogueInteract += DisableDialogueInteract;
        InputManager.Instance.playerInputAction.DialogueInteract.Dialogue.performed += SubmitPressed;
    }
    private void OnDisable()
    {
        GameEventsManager.Instance.dialogueEvents.onEnableDialogueInteract -= EnableDialogueInteract;
        GameEventsManager.Instance.dialogueEvents.onDisableDialogueInteract -= DisableDialogueInteract;
        InputManager.Instance.playerInputAction.DialogueInteract.Dialogue.performed += SubmitPressed;
        GameEventsManager.Instance.dialogueEvents.onEnterDialogue -= EnterDialogue;
    }
    private void SubmitPressed(InputAction.CallbackContext cxt)
    {
        if (!dialoguePlaying)
        {
            return;
        }
        ContinueOrExitStory();
    }
    public void EnterDialogue(string knotName)
    {
        Debug.Log("dialoogue playing");
        if (dialoguePlaying)
        {
            return;
        }
        dialoguePlaying = true;
        GameEventsManager.Instance.playerEvents.FreezingPlayer();
        GameEventsManager.Instance.dialogueEvents.DialogueStarted();
        GameEventsManager.Instance.dialogueEvents.EnableDialogueInteract();
        if (!knotName.Equals(""))
        {
            story.ChoosePathString(knotName);
        }
        else
        {
            Debug.LogWarning("Knot name was the empty string");
        }
        ContinueOrExitStory();
    }
    private void ContinueOrExitStory()
    {
        if (story.canContinue)
        {
            string dialogueLine = story.Continue().Trim();
            string actorName = "";
            if (dialogueLine.Contains(":"))
            {
                int index = dialogueLine.IndexOf(":");
                actorName = dialogueLine.Substring(0, index).Trim();
                dialogueLine = dialogueLine.Substring(index + 1).Trim();
            }
            GameEventsManager.Instance.dialogueEvents.DisplayDialogue(actorName, dialogueLine);
        }
        else
        {
            ExitDialogue();
        }
    }
    private void ExitDialogue()
    {
        Debug.Log("Exit Dialogue");
        GameEventsManager.Instance.dialogueEvents.DialogueFinished();
        GameEventsManager.Instance.playerEvents.unFreezingPlayer();
        GameEventsManager.Instance.dialogueEvents.DisableDialogueInteract();
        dialoguePlaying = false;
        story.ResetState();
    }

    private void EnableDialogueInteract()
    {
        InputManager.Instance.playerInputAction.DialogueInteract.Enable();
    }
    private void DisableDialogueInteract()
    {
        InputManager.Instance.playerInputAction.DialogueInteract.Disable();
    }
}
