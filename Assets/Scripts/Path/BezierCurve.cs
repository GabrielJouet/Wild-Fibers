using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to create a bezier curve.
/// </summary>
public class BezierCurve : MonoBehaviour
{
    [Header("Points")]

    /// <summary>
    /// Start point of the curve.
    /// </summary>
    [SerializeField]
    private Transform _startPoint;

    /// <summary>
    /// Parameter point of the start point.
    /// </summary>
    [SerializeField]
    private Transform _parameterStartPoint;

    /// <summary>
    /// End point of the curve.
    /// </summary>
    [SerializeField]
    private Transform _endPoint;

    /// <summary>
    /// Parameter point of the end point.
    /// </summary>
    [SerializeField]
    private Transform _parameterEndPoint;


    [Header("Precision")]

    /// <summary>
    /// Number of iterations for each curve.
    /// </summary>
    [SerializeField]
    private int _numberOfIterations;


    [SerializeField]
    private bool _activateGizmos;


    /// <summary>
    /// List of point creating the curve.
    /// </summary>
    public List<Vector3> Curve { get; private set; } = new List<Vector3>();



    /// <summary>
    /// Awake method in order to initialize.
    /// </summary>
    private void Awake()
    {
        /*for (int i = 0; i < transform.childCount; i ++)
            transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;
        */
        ComputePath();
    }


    
    /// <summary>
    /// On draw Gizmos is used to display curve in editor.
    /// </summary>
    private void OnDrawGizmos()
    {
        if (_activateGizmos)
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
    }
    


    /// <summary>
    /// Method used to compute the bezier curve.
    /// </summary>
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

            Curve.Add(oneMinusT * P + t * T);
        }
    }
}