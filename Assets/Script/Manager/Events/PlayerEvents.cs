using System;
public class PlayerEvents
{
    public Action FreezingPlayer;
    public Action unFreezingPlayer;
    public void onFreezingPlayer()
    {
        if (FreezingPlayer != null)
        {
            FreezingPlayer?.Invoke();
        }
    }
    public void onunFreezingPlayer()
    {
        if (unFreezingPlayer != null)
        {
            unFreezingPlayer?.Invoke();
        }
    }
}
