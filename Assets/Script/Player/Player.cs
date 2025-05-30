using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] protected PlayerInputAction playerInputAction;

    protected virtual void OnEnable()
    {
        playerInputAction.Enable();
    }
    protected virtual void OnDisable()
    {
        playerInputAction.Disable();
    }
    protected virtual void Awake()
    {
        playerInputAction = new PlayerInputAction();
    }
    protected Vector2 RoundVector(ref Vector2 vector2)
    {
        vector2.x = Mathf.RoundToInt(vector2.x);
        vector2.y = Mathf.RoundToInt(vector2.y);
        return vector2;
    }
    protected Vector2 RoundVector(Vector2 vector2)
    {
        vector2.x = Mathf.RoundToInt(vector2.x);
        vector2.y = Mathf.RoundToInt(vector2.y);
        return vector2;
    }
}
