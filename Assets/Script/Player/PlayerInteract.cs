using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : Player
{
    private InputAction IAinteract;
    private InputAction IAmove;
    private PlayerData playerData;
    private Vector2 facingOffset;


    protected override void Awake()
    {
        base.Awake();
        playerData = GetComponent<PlayerData>();
        playerData.direction = Vector2.zero;
        IAinteract = playerInputAction.Player.Interact;
        IAmove = playerInputAction.Player.Move;
        IAinteract.performed += Interacting;
    }

    private void Interacting(InputAction.CallbackContext cxt)
    {

        Vector2 vector2pos = new Vector2(transform.position.x, transform.position.y);
        Vector2 InteractPos = vector2pos + facingOffset;
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
        if (playerData.direction != Vector2.zero) { facingOffset = playerData.direction; };
    }
}
