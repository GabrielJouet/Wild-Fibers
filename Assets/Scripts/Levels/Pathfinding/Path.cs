using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField]
    private List<BezierCurve> _curves;


    private List<Vector3> _finalPath = new List<Vector3>();


    private void Start()
    {
        CalculatePath();
    }


    private void CalculatePath()
    {
        foreach(BezierCurve current in _curves)
        {
            foreach (Vector3 currentVector in current.GetCurve())
            {
                if (!_finalPath.Contains(currentVector))
                    _finalPath.Add(currentVector);
            }
        }
    }


    public List<Vector3> GetPath() { return _finalPath; }

    public Vector3 GetPathPosition(int index) { return _finalPath[index]; }
}