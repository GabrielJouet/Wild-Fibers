using System.Collections.Generic;
using UnityEngine;

/*
 * Class used to compute bezier path into a path and save data
 */
public class Path : MonoBehaviour
{
    [Header("Beziers Curves")]
    //List of all bezier curves in a single path
    [SerializeField]
    private List<BezierCurve> _curves;
    

    //List of position in a path 
    private readonly List<Vector3> _finalPath = new List<Vector3>();



    //Start method called when the object is started
    private void Start()
    {
        CalculatePath();
    }


    //Method used to sum each Bezier curve in order to create the final path
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



    //Getters
    public List<Vector3> GetPath() { return _finalPath; }

    public Vector3 GetPathPosition(int index) { return _finalPath[index]; }
}