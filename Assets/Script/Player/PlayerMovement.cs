using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float tileSize;
    private bool isMoving;
    private Animator animator;
    private InputAction IAmove;
    private PlayerData playerData;
    protected void Awake()
    {
        playerData = GetComponent<PlayerData>();
        animator = GetComponent<Animator>();
        isMoving = false;
        IAmove = InputManager.Instance.playerInputAction.Player.Move;
        GameEventsManager.Instance.playerEvents.FreezingPlayer += DisableMovement;
        GameEventsManager.Instance.playerEvents.unFreezingPlayer += EnableMovement;
    }
    void DisableMovement()
    {
        InputManager.Instance.playerInputAction.Player.Disable();
    }
    void EnableMovement()
    {
        InputManager.Instance.playerInputAction.Player.Enable();
    }
    // Update is called once per frame
    void Movement()
    {
        Vector3 localscale = transform.localScale;
        playerData.direction = IAmove.ReadValue<Vector2>();
        playerData.direction = UtilitiesMath.Instance.RoundVector(playerData.direction);
        if (playerData.direction.x != 0) localscale.x = playerData.direction.x;
        if (playerData.direction != Vector2.zero)
        {
            transform.localScale = localscale;
            TryToMove();
        }
        else animator.SetBool("IsMoving", isMoving);
    }
    IEnumerator MoveToTiles(Vector3 NewPos)
    {
        isMoving = true;
        animator.SetBool("IsMoving", isMoving);
        while ((NewPos - transform.position).sqrMagnitude > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, NewPos, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = NewPos;
        isMoving = false;
    }
    private void TryToMove()
    {
        Vector3 newtarget = transform.position;
        Vector3 direction = new Vector3(playerData.direction.x, 0, 0);
        if (!CheckIsHasBlock(transform.position + direction))
            newtarget += direction;

        direction = new Vector3(0, playerData.direction.y, 0);
        if (!CheckIsHasBlock(transform.position + direction))
            newtarget += direction;

        Vector3 totalDirection = new Vector3(playerData.direction.x, playerData.direction.y, 0);
        if (newtarget != transform.position && !CheckIsHasBlock(transform.position + totalDirection))
            StartCoroutine(MoveToTiles(newtarget));
        else
            animator.SetBool("IsMoving", isMoving);
    }
    private bool CheckIsHasBlock(Vector2 newTarget)
    {
        // Debug.Log("new target: " + newTarget.x + " " + newTarget.y);

        Collider2D hit = Physics2D.OverlapPoint(newTarget);
        if (hit != null)
        {
            //   Debug.Log("hit is: " + hit.name);
            return true;
        }
        else return false;
    }
    void Update()
    {
        if (!isMoving) Movement();
    }
}
