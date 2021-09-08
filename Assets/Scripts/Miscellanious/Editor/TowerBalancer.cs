using UnityEditor;
using UnityEngine;

public class TowerBalancer : EditorWindow
{
    private TowerData _loadedData;
    private TowerData _previousData;

    private float _newTimeBetweenShots;

    private int _newDamage;

    private float _newArmorThrough;

    private float _newSpeed;

    private int _newNumberOfShots;

    private float _newRange;

    private bool _newCanHitFlying;

    private bool _newShotsRandomly;

    private float _newDotArmor;

    private float _newDot;

    private float _newDotDuration;

    private int _newPotentialEnemies;


    private readonly int _spaceBetweenCategories = 10;
    private readonly int _spaceBetweenLines = 5;


    [MenuItem("Wild Fibers / Tower Balancer")]
    public static void ShowWindow()
    {
        GetWindow(typeof(TowerBalancer));
    }


    private void OnGUI()
    {
        _loadedData = (TowerData)EditorGUILayout.ObjectField(_loadedData, typeof(TowerData), false);

        if (_previousData != _loadedData && _loadedData != null)
        {
            _previousData = _loadedData;

            _newTimeBetweenShots = _loadedData.TimeShots;
            _newDamage = _loadedData.Damage;
            _newArmorThrough = _loadedData.ArmorThrough;
            _newNumberOfShots = _loadedData.Shots;
            _newRange = _loadedData.Range;
            _newCanHitFlying = _loadedData.HitFlying;
            _newShotsRandomly = _loadedData.ShotsRandomly;
            _newPotentialEnemies = _loadedData.Shots;

            _newDotArmor = _loadedData.ArmorThroughMalus;
            _newDot = _loadedData.Dot;
            _newDotDuration = _loadedData.DotDuration;
        }

        if(_loadedData != null)
        {
            GUILayout.Space(_spaceBetweenCategories);
            GUILayout.Label("Price", EditorStyles.boldLabel);

            int numberOfShots = (_newPotentialEnemies > _newNumberOfShots ? _newPotentialEnemies : _newNumberOfShots);
            float dps = (_newDamage + _newDamage * _newArmorThrough / 100) / _newTimeBetweenShots;
            float dotDps = (_newDot * _newDotDuration / _newTimeBetweenShots + (_newDotArmor / 100 * _newDotDuration / _newTimeBetweenShots) * 4) * 2;
            float bonusMultiplier = 1 + (_newCanHitFlying ? 0.45f : 0) + (!_newShotsRandomly ? 0.125f : 0);
            int finalPrice = Mathf.FloorToInt((dps + dotDps) * _newRange * numberOfShots * bonusMultiplier * 5);
            EditorGUILayout.IntField(finalPrice - finalPrice % 5);


            GUILayout.Space(_spaceBetweenCategories);
            GUILayout.Label("Behavior", EditorStyles.boldLabel);
            GUILayout.Label("Time between shots");
            _newTimeBetweenShots = EditorGUILayout.FloatField(_newTimeBetweenShots);

            GUILayout.Space(_spaceBetweenLines);
            GUILayout.Label("Damage");
            _newDamage = EditorGUILayout.IntField(_newDamage);

            GUILayout.Space(_spaceBetweenLines);
            GUILayout.Label("Armor Through");
            _newArmorThrough = EditorGUILayout.FloatField(_newArmorThrough);

            GUILayout.Space(_spaceBetweenLines);
            GUILayout.Label("Projectile Speed");
            _newSpeed = EditorGUILayout.FloatField(_newSpeed);

            GUILayout.Space(_spaceBetweenLines);
            GUILayout.Label("Number Of Shots");
            _newNumberOfShots = EditorGUILayout.IntField(_newNumberOfShots);

            GUILayout.Space(_spaceBetweenLines);
            GUILayout.Label("Range");
            _newRange = EditorGUILayout.FloatField(_newRange);

            GUILayout.Space(_spaceBetweenLines);
            GUILayout.Label("Number of potentials enemies");
            _newPotentialEnemies = EditorGUILayout.IntField(_newPotentialEnemies);

            GUILayout.Space(_spaceBetweenLines);
            GUILayout.Label("Can Hit Flying");
            _newCanHitFlying = GUILayout.Toggle(_newCanHitFlying, "");

            GUILayout.Space(_spaceBetweenLines);
            GUILayout.Label("Shots Randomly");
            _newShotsRandomly = GUILayout.Toggle(_newShotsRandomly, "");


            GUILayout.Space(_spaceBetweenCategories);
            GUILayout.Label("Dot", EditorStyles.boldLabel);
            GUILayout.Label("Armor Through Malus");
            _newDotArmor = EditorGUILayout.FloatField(_newDotArmor);

            GUILayout.Space(_spaceBetweenLines);
            GUILayout.Label("Dot");
            _newDot = EditorGUILayout.FloatField(_newDot);

            GUILayout.Space(_spaceBetweenLines);
            GUILayout.Label("Dot duration");
            _newDotDuration = EditorGUILayout.FloatField(_newDotDuration);
        }
    }
}
