using UnityEngine;

public class Level : MonoBehaviour
{
    #region Singleton

    private static Level _levelInstance;

    public static Level Instance
    {
        get
        {
            if (_levelInstance == null) _levelInstance = FindObjectOfType<Level>();
            return _levelInstance;
        }
    }

    #endregion

    public Vector2[] patrolRoute;
}
