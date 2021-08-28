using UnityEngine;

/// <summary>
/// Class that handle every controllers.
/// </summary>
public class Controller : MonoBehaviour
{
    /// <summary>
    /// Instance of itself.
    /// </summary>
    public static Controller Instance { get; private set; } = null;

    /// <summary>
    /// Save controller component.
    /// </summary>
    public SaveController SaveControl { get; private set; }

    /// <summary>
    /// Squad controller component.
    /// </summary>
    public SquadController SquadControl { get; private set; }

    /// <summary>
    /// Pool controller component.
    /// </summary>
    public PoolController PoolControl { get; private set; }

    /// <summary>
    /// Enemy controller component.
    /// </summary>
    public EnemyController EnemyControl { get; private set; }



    /// <summary>
    /// Awake method for initialization.
    /// </summary>
    private void Awake()
    {
        if (FindObjectsOfType<Controller>().Length > 1)
            Destroy(gameObject);

        Application.targetFrameRate = 60;
        Instance = this;
        DontDestroyOnLoad(gameObject);

        SaveControl = FindObjectOfType<SaveController>();
        SquadControl = FindObjectOfType<SquadController>();
        PoolControl = FindObjectOfType<PoolController>();
        EnemyControl = FindObjectOfType<EnemyController>();
    }
}