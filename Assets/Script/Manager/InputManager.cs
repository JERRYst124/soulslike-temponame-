public class InputManager : Singleton<InputManager>
{
    public PlayerInputAction playerInputAction;
    protected override void Awake()
    {
        base.Awake();
        playerInputAction = new PlayerInputAction();
    }
    private void OnEnable()
    {
        playerInputAction.Enable();
    }
    private void OnDisable()
    {
        playerInputAction.Disable();
    }
    void Update()
    {
    }
}
