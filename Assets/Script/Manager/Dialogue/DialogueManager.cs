
using Ink.Runtime;
using UnityEngine;
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
    }
    private void OnDisable()
    {
        GameEventsManager.Instance.dialogueEvents.onEnterDialogue -= EnterDialogue;
    }
    public void EnterDialogue(string knotName)
    {
        if (dialoguePlaying)
        {
            return;
        }
        dialoguePlaying = true;

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
        }
        else
        {
            ExitDialogue();
        }
    }
    private void ExitDialogue()
    {
        Debug.Log("Exit Dialogue");
        dialoguePlaying = true;
        story.ResetState();
    }
}
