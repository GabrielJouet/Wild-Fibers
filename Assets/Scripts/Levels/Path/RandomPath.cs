using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to store and generate new paths.
/// </summary>
public class RandomPath : MonoBehaviour
{
    /// <summary>
    /// First curve in game.
    /// </summary>
    [SerializeField]
    private List<BezierCurve> _firstBezierCurves;

    /// <summary>
    /// Second curve in game.
    /// </summary>
    [SerializeField]
    private List<BezierCurve> _secondBezierCurves;

    /// <summary>
    /// How many paths we want to generate.
    /// </summary>
    [SerializeField]
    private int _numberOfAlreadyGeneratedPaths;


    /// <summary>
    /// Already generated paths.
    /// </summary>
    private readonly List<List<Vector2>> _alreadyGeneratedPaths = new List<List<Vector2>>();


    /// <summary>
    /// One generated path given randomly.
    /// </summary>
    public List<Vector2> GeneratedPath { get => _alreadyGeneratedPaths[Random.Range(0, _numberOfAlreadyGeneratedPaths)]; }



    /// <summary>
    /// Start method, used for initialization.
    /// </summary>
    private void Start()
    {
        List<Vector3> firstPath = new List<Vector3>();
        List<Vector3> secondPath = new List<Vector3>();
        foreach (BezierCurve current in _firstBezierCurves)
            foreach (Vector3 currentVector in current.Curve)
                firstPath.Add(currentVector);

        foreach (BezierCurve current in _secondBezierCurves)
            foreach (Vector3 currentVector in current.Curve)
                secondPath.Add(currentVector);

        float ratio;
        List<Vector2> newPath;
        for (int i = 0; i < _numberOfAlreadyGeneratedPaths; i ++)
        {
            ratio = Random.Range(0, 100f);
            newPath = new List<Vector2>();

            for (int j = 0; j < firstPath.Count; j++)
                newPath.Add((ratio * firstPath[j] + (100f - ratio) * secondPath[j]) / 100f);

            _alreadyGeneratedPaths.Add(newPath);
        }
    }
}