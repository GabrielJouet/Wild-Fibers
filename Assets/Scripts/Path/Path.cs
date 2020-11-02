using System.Collections.Generic;
using UnityEngine;

/*
 * Class used to compute bezier path into a path and save data
 */
public class Path
{
    private List<Vector3> _path = new List<Vector3>();


    //Getters
    public List<Vector3> GetPath() { return _path; }

    public int GetPathLength() { return _path.Count; }

    public void SetPath(List<Vector3> newPath) { _path = newPath; }

    public void AddPoint(Vector3 newPoint)
    {
        if (!_path.Contains(newPoint))
            _path.Add(newPoint);
    }

    public Vector3 GetPathPosition(int index) { return _path[index]; }
}