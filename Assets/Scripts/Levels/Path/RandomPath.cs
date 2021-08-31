using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to store and generate new paths.
/// </summary>
public class RandomPath : MonoBehaviour
{
    [Header("Bezier Curves")]

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


    [Header("Generation variables")]

    /// <summary>
    /// How many paths we want to generate.
    /// </summary>
    [SerializeField]
    private int _numberOfAlreadyGeneratedPaths;


    /// <summary>
    /// First path divided in points.
    /// </summary>
    private readonly List<Vector2> _firstPath = new List<Vector2>();

    /// <summary>
    /// Second path divided in points.
    /// </summary>
    private readonly List<Vector2> _secondPath = new List<Vector2>();


    /// <summary>
    /// Already generated paths.
    /// </summary>
    private readonly List<List<Vector2>> _alreadyGeneratedPaths = new List<List<Vector2>>();


    /// <summary>
    /// Awake method, used for initialization.
    /// </summary>
    private void Start()
    {
        CalculatePath(_firstPath, _firstBezierCurves);
        CalculatePath(_secondPath, _secondBezierCurves);
    }


    /// <summary>
    /// Method used to calculate the first two paths.
    /// </summary>
    /// <param name="path">The current path wanted</param>
    /// <param name="newCurves">The related curve</param>
    private void CalculatePath(in List<Vector2> path, List<BezierCurve> newCurves)
    {
        foreach (BezierCurve current in newCurves)
        {
            foreach (Vector3 currentVector in current.Curve)
                path.Add(currentVector);
        }
    }


    /// <summary>
    /// Method used to calculate and returns a random path.
    /// </summary>
    /// <returns>Returns a random path generated</returns>
    public List<Vector2> CalculateRandomPath()
    {
        if(_alreadyGeneratedPaths.Count < _numberOfAlreadyGeneratedPaths)
        {
            float ratio = Random.Range(0, 100f);
            List<Vector2> newPath = new List<Vector2>();

            for (int i = 0; i < _firstPath.Count; i++)
                newPath.Add((ratio * _firstPath[i] + (100f - ratio) * _secondPath[i]) / 100f);

            _alreadyGeneratedPaths.Add(newPath);
            return newPath;
        }
        else
            return _alreadyGeneratedPaths[Random.Range(0, _alreadyGeneratedPaths.Count)];
    }
}
