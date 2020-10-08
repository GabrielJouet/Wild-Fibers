using System.Collections.Generic;
using UnityEngine;

/*
 * Class used to create a curved line
 */
public class BezierCurve : MonoBehaviour
{
    [Header("Points")]
    //Start point for bezier curve
    [SerializeField]
    private Transform _startPoint;

    //Parameter start point for curve
    [SerializeField]
    private Transform _parameterStartPoint;

    //End point for bezier curve
    [SerializeField]
    private Transform _endPoint;

    //Parameter end point for curve
    [SerializeField]
    private Transform _parameterEndPoint;


    [Header("Precision")]
    //Precision of the curve
    [SerializeField]
    private int _numberOfIterations;


    //List of position in each curve
    private readonly List<Vector3> _curve = new List<Vector3>();



    //Awake method called when the object is awaked (before Start)
    private void Awake()
    {
        ComputePath();
    }


    //OnDrawGizmos method used to draw in Editor
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 lastPosition = _startPoint.position;
        Vector3 A = _startPoint.position;
        Vector3 D = _endPoint.position;
        Vector3 B = _parameterStartPoint.position;
        Vector3 C = _parameterEndPoint.position;

        for (int i = 1; i <= _numberOfIterations; i++)
        {
            float t = i * 0.01f;
            float oneMinusT = 1f - t;

            Vector3 Q = oneMinusT * A + t * B;
            Vector3 R = oneMinusT * B + t * C;
            Vector3 S = oneMinusT * C + t * D;

            Vector3 P = oneMinusT * Q + t * R;
            Vector3 T = oneMinusT * R + t * S;

            Gizmos.DrawLine(lastPosition, oneMinusT * P + t * T);

            lastPosition = oneMinusT * P + t * T;
        }
    }
    */


    //Method used to create a bezier curve with parameters points
    private void ComputePath()
    {
        Vector3 A = _startPoint.position;
        Vector3 D = _endPoint.position;
        Vector3 B = _parameterStartPoint.position;
        Vector3 C = _parameterEndPoint.position;

        for (int i = 1; i <= _numberOfIterations; i++)
        {
            float t = i * 0.01f;
            float oneMinusT = 1f - t;

            Vector3 Q = oneMinusT * A + t * B;
            Vector3 R = oneMinusT * B + t * C;
            Vector3 S = oneMinusT * C + t * D;

            Vector3 P = oneMinusT * Q + t * R;
            Vector3 T = oneMinusT * R + t * S;

            _curve.Add(oneMinusT * P + t * T);
        }
    }


    //Getter
    public List<Vector3> GetCurve() { return _curve; }
}