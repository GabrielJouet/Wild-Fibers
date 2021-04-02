using UnityEngine;

public class Controller : MonoBehaviour
{
    public static Controller Instance { get; private set; } = null;

    public SaveController SaveControl { get; private set; }
    public SquadController SquadControl { get; private set; }
    public PoolController PoolControl { get; private set; }
    public EnemyController EnemyControl { get; private set; }


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