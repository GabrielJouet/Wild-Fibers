using UnityEditor;
using UnityEngine;

public class TowerBalancer : EditorWindow
{
    private TowerData _loadedData;
    private TowerData _previousData;

    private float _newPrice;

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

            _newPrice = _loadedData.Price;
            _newTimeBetweenShots = _loadedData.TimeShots;
            _newDamage = _loadedData.Damage;
            _newArmorThrough = _loadedData.ArmorThrough;
            _newSpeed = _loadedData.ProjectileSpeed;
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
            GUILayout.Space(10);
            GUILayout.Label("Price", EditorStyles.boldLabel);

            int numberOfShots = (_newPotentialEnemies > _newNumberOfShots ? _newPotentialEnemies : _newNumberOfShots);
            float dps = (_newDamage + _newDamage * _newArmorThrough / 100) / _newTimeBetweenShots;
            float dotDps = _newDot * _newDotDuration / _newTimeBetweenShots + (_newDotArmor / 100 * _newDotDuration / _newTimeBetweenShots) * 4;
            float bonusMultiplier = 1 + (_newCanHitFlying ? 0.45f : 0) + (!_newShotsRandomly ? 0.125f : 0);
            _newPrice = float.Parse(GUILayout.TextField(((dps + dotDps) * _newRange * numberOfShots * bonusMultiplier * 5).ToString()));


            GUILayout.Space(15);
            GUILayout.Label("Behavior", EditorStyles.boldLabel);
            GUILayout.Label("Time between shots");
            _newTimeBetweenShots = float.Parse(GUILayout.TextField(_newTimeBetweenShots.ToString()));

            GUILayout.Space(10);
            GUILayout.Label("Damage");
            _newDamage = int.Parse(GUILayout.TextField(_newDamage.ToString()));

            GUILayout.Space(10);
            GUILayout.Label("Armor Through");
            _newArmorThrough = float.Parse(GUILayout.TextField(_newArmorThrough.ToString()));

            GUILayout.Space(10);
            GUILayout.Label("Projectile Speed");
            _newSpeed = float.Parse(GUILayout.TextField(_newSpeed.ToString()));

            GUILayout.Space(10);
            GUILayout.Label("Number Of Shots");
            _newNumberOfShots = int.Parse(GUILayout.TextField(_newNumberOfShots.ToString()));

            GUILayout.Space(10);
            GUILayout.Label("Range");
            _newRange = float.Parse(GUILayout.TextField(_newRange.ToString()));

            GUILayout.Space(10);
            GUILayout.Label("Number of potentials enemies");
            _newPotentialEnemies = int.Parse(GUILayout.TextField(_newPotentialEnemies.ToString()));

            GUILayout.Space(10);
            GUILayout.Label("Can Hit Flying");
            _newCanHitFlying = GUILayout.Toggle(_newCanHitFlying, "");

            GUILayout.Space(10);
            GUILayout.Label("Shots Randomly");
            _newShotsRandomly = GUILayout.Toggle(_newShotsRandomly, "");


            GUILayout.Space(15);
            GUILayout.Label("Dot", EditorStyles.boldLabel);
            GUILayout.Label("Armor Through Malus");
            _newDotArmor = float.Parse(GUILayout.TextField(_newDotArmor.ToString()));

            GUILayout.Space(10);
            GUILayout.Label("Dot");
            _newDot = float.Parse(GUILayout.TextField(_newDot.ToString()));

            GUILayout.Space(10);
            GUILayout.Label("Dot duration");
            _newDotDuration = float.Parse(GUILayout.TextField(_newDotDuration.ToString()));
        }

        if(GUILayout.Button("Apply all changes"))
        {

        }
    }
}
