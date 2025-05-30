using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : Player
{
    private InputAction IAinteract;
    private InputAction IAmove;
    protected override void Awake()
    {
        base.Awake();
        IAinteract = playerInputAction.Player.Interact;
        IAmove = playerInputAction.Player.Move;
        IAinteract.performed += Interacting;
    }

    private void Interacting(InputAction.CallbackContext cxt)
    {

        Vector2 vector2pos = new Vector2(transform.position.x, transform.position.y);

        Vector2 InteractPos = vector2pos + RoundVector(IAmove.ReadValue<Vector2>());

        Debug.Log($"interacts to:{InteractPos.x} {InteractPos.y}");
        Collider2D hit = Physics2D.OverlapPoint(InteractPos, LayerMask.GetMask("Object"));
        if (hit != null)
        {
            Debug.Log("interact hit is: " + hit.name);
            hit.TryGetComponent<IIteractable>(out IIteractable other);
            {
                Debug.Log("interact");
                other.Interact();
            }
        }
    }

    private void OnDestroy()
    {
        IAinteract.performed -= Interacting;
    }
    void Update()
    {

    }
}
