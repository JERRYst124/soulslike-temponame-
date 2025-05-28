using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float tileSize;
    private bool isMoving;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        isMoving = false;
    }

    // Update is called once per frame
    void Movement()
    {

        Vector3 direction = Vector3.zero;
        Vector3 localscale = transform.localScale;
        if (Input.GetKey(KeyCode.W)) direction.y = 1;
        else
        if (Input.GetKey(KeyCode.S)) direction.y = -1;
        if (Input.GetKey(KeyCode.D)) direction.x = 1;
        else
        if (Input.GetKey(KeyCode.A)) direction.x = -1;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) localscale.x = direction.x;
        transform.localScale = localscale;

        if (direction != Vector3.zero)
        {
            Vector3 newtarget = transform.position + direction * tileSize;
            StartCoroutine(MoveToTiles(newtarget));
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
            Debug.Log((NewPos - transform.position).sqrMagnitude);
            transform.position = Vector2.MoveTowards(transform.position, NewPos, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = NewPos;
        isMoving = false;

    }
    void Update()
    {
        if (!isMoving) Movement();
    }
}
