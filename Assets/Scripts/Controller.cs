using Enemies;
using Save;
using UnityEngine;

/// <summary>
/// Class that handle every controllers.
/// </summary>
[RequireComponent(typeof(SaveController))]
[RequireComponent(typeof(SquadController))]
[RequireComponent(typeof(EnemyController))]
public class Controller : MonoBehaviour
{
    /// <summary>
    /// Instance of itself.
    /// </summary>
    public static Controller Instance { get; private set; }

    /// <summary>
    /// Save controller component.
    /// </summary>
    public SaveController SaveController { get; private set; }

    /// <summary>
    /// Squad controller component.
    /// </summary>
    public SquadController SquadController { get; private set; }

    /// <summary>
    /// Enemy controller component.
    /// </summary>
    public EnemyController EnemyController { get; private set; }



    /// <summary>
    /// Awake method for initialization.
    /// </summary>
    private void Awake()
    {
        if (FindObjectsOfType<Controller>().Length > 1)
            Destroy(gameObject);
    }


    /// <summary>
    /// Start is called after Awake.
    /// </summary>
    private void Start()
    {
        Application.targetFrameRate = 60;
        Instance = this;
        DontDestroyOnLoad(gameObject);

        SaveController = GetComponent<SaveController>();
        SquadController = GetComponent<SquadController>();
        EnemyController = GetComponent<EnemyController>();
    }
}