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


    private readonly Path _firstPath = new Path();
    private readonly Path _secondPath = new Path();

    private readonly List<Path> _alreadyGeneratedPaths = new List<Path>();


    //Start method called when the object is started
    private void Start()
    {
        CalculatePath(_firstPath, _firstBezierCurves);
        CalculatePath(_secondPath, _secondBezierCurves);
    }


    //Method used to sum each Bezier curve in order to create the final path
    private void CalculatePath(in Path path, List<BezierCurve> newCurves)
    {
        foreach (BezierCurve current in newCurves)
        {
            foreach (Vector3 currentVector in current.GetCurve())
                path.AddPoint(currentVector);
        }
    }


    public Path CalculateRandomPath()
    {
        if(_alreadyGeneratedPaths.Count < _numberOfAlreadyGeneratedPaths)
        {
            float ratio = Random.Range(0, 100f);
            Path newPath = new Path();
            List<Vector3> firstPathBuffer = _firstPath.GetPath();
            List<Vector3> secondPathBuffer = _secondPath.GetPath();

            for (int i = 0; i < _firstPath.GetPathLength(); i++)
                newPath.AddPoint((ratio * firstPathBuffer[i] + (100f - ratio) * secondPathBuffer[i]) / 100f);

            _alreadyGeneratedPaths.Add(newPath);
            return newPath;
        }
        else
            return _alreadyGeneratedPaths[Random.Range(0, _alreadyGeneratedPaths.Count)];
    }
}
