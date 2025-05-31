using UnityEngine;

public class Lever : MonoBehaviour, IIteractable
{
    [SerializeField] private string knotName;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void IIteractable.Interact()
    {
        // Debug.Log("interact");
        GameEventsManager.Instance.dialogueEvents.EnterDialogue(knotName);
    }
}
