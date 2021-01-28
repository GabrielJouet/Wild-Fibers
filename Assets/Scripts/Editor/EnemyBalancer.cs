using UnityEditor;
using UnityEngine;

public class EnemyBalancer : EditorWindow
{
    private Enemy _loadedEnemy;
    private Enemy _previousEnemy;

    private float _newHealth;

    private float _newArmor;

    private float _newSpeed;

    private int _newNumberOfLives;

    private bool _newFlying;


    private readonly int _spaceBetweenCategories = 10;
    private readonly int _spaceBetweenLines = 5;


    [MenuItem("Wild Fibers / Enemy Balancer")]
    public static void ShowWindow()
    {
        GetWindow(typeof(EnemyBalancer));
    }


    private void OnGUI()
    {
        _loadedEnemy = (Enemy)EditorGUILayout.ObjectField(_loadedEnemy, typeof(Enemy), false);

        if (_previousEnemy != _loadedEnemy && _loadedEnemy != null)
        {
            _previousEnemy = _loadedEnemy;

            _newHealth = _loadedEnemy.HealthMax;
            _newArmor = _loadedEnemy.ArmorMax;
            _newSpeed = _loadedEnemy.Speed;
            _newNumberOfLives = _loadedEnemy.LivesTaken;
            _newFlying = _loadedEnemy.Flying;
        }

        if (_loadedEnemy != null)
        {
            GUILayout.Space(_spaceBetweenCategories);
            GUILayout.Label("Gold gained", EditorStyles.boldLabel);

            int finalGold = Mathf.FloorToInt((_newHealth + (_newHealth * (_newArmor * 2 / 100)) * _newSpeed * 3 * _newNumberOfLives * (1 + (_newFlying ? 0.5f : 0))) / 15);
            EditorGUILayout.IntField(finalGold);


            GUILayout.Space(_spaceBetweenCategories);
            GUILayout.Label("Behavior", EditorStyles.boldLabel);
            GUILayout.Label("Health");
            _newHealth = EditorGUILayout.FloatField(_newHealth);

            GUILayout.Space(_spaceBetweenLines);
            GUILayout.Label("Armor");
            _newArmor = EditorGUILayout.FloatField(_newArmor);

            GUILayout.Space(_spaceBetweenLines);
            GUILayout.Label("Speed");
            _newSpeed = EditorGUILayout.FloatField(_newSpeed);

            GUILayout.Space(_spaceBetweenLines);
            GUILayout.Label("Number of lives");
            _newNumberOfLives = EditorGUILayout.IntField(_newNumberOfLives);

            GUILayout.Space(_spaceBetweenLines);
            GUILayout.Label("Flying");
            _newFlying = GUILayout.Toggle(_newFlying, "");
        }
    }
}
