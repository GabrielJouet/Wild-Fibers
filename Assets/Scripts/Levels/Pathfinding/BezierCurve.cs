using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    [Header("Points")]
    [SerializeField]
    private Transform _startPoint;

    [SerializeField]
    private Transform _parameterStartPoint;

    [SerializeField]
    private Transform _endPoint;

    [SerializeField]
    private Transform _parameterEndPoint;


    [Header("Precision")]
    [SerializeField]
    private int _numberOfIterations;


    private List<Vector3> _curve = new List<Vector3>();


    private void Awake()
    {
        ComputePath();
    }

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


    public List<Vector3> GetCurve() { return _curve; }
}