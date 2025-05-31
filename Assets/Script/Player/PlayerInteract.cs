using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    private InputAction IAinteract;
    private InputAction IAmove;
    private PlayerData playerData;
    private Vector2 facingOffset;


    protected void Awake()
    {
        playerData = GetComponent<PlayerData>();
        playerData.direction = Vector2.zero;
        IAinteract = InputManager.Instance.playerInputAction.Player.Interact;
        IAmove = InputManager.Instance.playerInputAction.Player.Move;
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
