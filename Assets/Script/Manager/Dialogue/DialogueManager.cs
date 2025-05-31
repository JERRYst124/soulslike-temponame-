
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
        Debug.Log("has add dialogue");
        GameEventsManager.Instance.dialogueEvents.onEnterDialogue += EnterDialogue;
        InputManager.Instance.playerInputAction.Player.Interact.performed += SubmitPressed;
    }
    private void OnDisable()
    {
        InputManager.Instance.playerInputAction.Player.Interact.performed -= SubmitPressed;
        GameEventsManager.Instance.dialogueEvents.onEnterDialogue -= EnterDialogue;
    }
    private void SubmitPressed(InputAction.CallbackContext cxt)
    {
        Debug.Log("Hi");
        if (!dialoguePlaying)
        {
            return;
        }
        //  ContinueOrExitStory();
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
            string dialogueLine = story.Continue();
            Debug.Log(dialogueLine);
            // dialoguePlaying = false;
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
        dialoguePlaying = false;
        story.ResetState();
    }
}
