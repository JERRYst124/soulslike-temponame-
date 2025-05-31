using UnityEngine;

public class UtilitiesMath : Singleton<UtilitiesMath>
{
    public Vector2 RoundVector(ref Vector2 vector2)
    {
        vector2.x = Mathf.RoundToInt(vector2.x);
        vector2.y = Mathf.RoundToInt(vector2.y);
        return vector2;
    }
    public Vector2 RoundVector(Vector2 vector2)
    {
        vector2.x = Mathf.RoundToInt(vector2.x);
        vector2.y = Mathf.RoundToInt(vector2.y);
        return vector2;
    }
}
