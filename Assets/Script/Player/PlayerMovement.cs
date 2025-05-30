using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Player
{
    [SerializeField] private float speed;
    [SerializeField] private float tileSize;
    private bool isMoving;
    private Animator animator;
    private InputAction IAmove;
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        isMoving = false;
        IAmove = playerInputAction.Player.Move;
    }
    // Update is called once per frame
    void Movement()
    {
        Vector3 direction = Vector3.zero;
        Vector3 localscale = transform.localScale;
        direction = IAmove.ReadValue<Vector2>();
        direction.x = Mathf.RoundToInt(direction.x);
        direction.y = Mathf.RoundToInt(direction.y);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) localscale.x = Mathf.RoundToInt(direction.x);
        transform.localScale = localscale;
        if (direction != Vector3.zero)
        {
            Vector3 newtarget = transform.position + direction * tileSize;
            if (CheckCanMove(newtarget)) StartCoroutine(MoveToTiles(newtarget));
        }
        else
        {
            direction = Vector3.zero;
            animator.SetBool("IsMoving", isMoving);
        }

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
    private bool CheckCanMove(Vector2 newTarget)
    {
        Debug.Log(newTarget.x + " " + newTarget.y);
        Collider2D hit = Physics2D.OverlapPoint(newTarget);
        if (hit != null)
        {
            Debug.Log("hit is: " + hit.name);
            return false;
        }
        else return true;
    }
    void Update()
    {
        if (!isMoving) Movement();
    }
}
