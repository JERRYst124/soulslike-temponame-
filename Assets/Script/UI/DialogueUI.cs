using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueFrame;
    [SerializeField] private TextMeshProUGUI contentText;
    [SerializeField] private TextMeshProUGUI actorText;
    private void Awake()
    {
        dialogueFrame.SetActive(false);
        ResetPanel();
    }
    private void OnEnable()
    {
        GameEventsManager.Instance.dialogueEvents.onDialogueStarted += DialogueStarted;
        GameEventsManager.Instance.dialogueEvents.onDialogueFinished += DialogueFinished;
        GameEventsManager.Instance.dialogueEvents.onDisplayDialogue += DisplayDialogue;
    }
    private void OnDisable()
    {
        GameEventsManager.Instance.dialogueEvents.onDialogueStarted -= DialogueStarted;
        GameEventsManager.Instance.dialogueEvents.onDialogueFinished -= DialogueFinished;
        GameEventsManager.Instance.dialogueEvents.onDisplayDialogue -= DisplayDialogue;
    }
    private void DialogueStarted()
    {
        dialogueFrame.SetActive(true);
    }
    private void DialogueFinished()
    {
        dialogueFrame.SetActive(false);
    }
    private void DisplayDialogue(string actorName, string text)
    {
        contentText.text = text;
        actorText.text = actorName;
    }
    private void ResetPanel()
    {
        contentText.text = "";
        actorText.text = "";
    }
}
