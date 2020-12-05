using System.Collections.Generic;
using UnityEngine;

public class RandomPath : MonoBehaviour
{
    [Header("Bezier Curves")]
    [SerializeField]
    private List<BezierCurve> _firstBezierCurves;

    [SerializeField]
    private List<BezierCurve> _secondBezierCurves;

    [Header("Generation variables")]
    [SerializeField]
    private int _numberOfAlreadyGeneratedPaths;


    private readonly List<Vector2> _firstPath = new List<Vector2>();
    private readonly List<Vector2> _secondPath = new List<Vector2>();

    private readonly List<List<Vector2>> _alreadyGeneratedPaths = new List<List<Vector2>>();


    //Start method called when the object is started
    private void Start()
    {
        CalculatePath(_firstPath, _firstBezierCurves);
        CalculatePath(_secondPath, _secondBezierCurves);
    }


    //Method used to sum each Bezier curve in order to create the final path
    private void CalculatePath(in List<Vector2> path, List<BezierCurve> newCurves)
    {
        foreach (BezierCurve current in newCurves)
        {
            foreach (Vector3 currentVector in current.Curve)
                path.Add(currentVector);
        }
    }


    //Method used to calculate a random path, if the number of already generated paths is too high
    //We pick one of these instead
    //
    //Returns the random path
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
