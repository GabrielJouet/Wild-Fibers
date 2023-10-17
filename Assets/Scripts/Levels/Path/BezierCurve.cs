using System.Collections.Generic;
using UnityEngine;

namespace Levels.Path
{
    /// <summary>
    /// Class used to create a bezier curve.
    /// </summary>
    public class BezierCurve : MonoBehaviour
    {
        /// <summary>
        /// Number of iterations for each curve.
        /// </summary>
        [SerializeField]
        private int numberOfIterations;

        /// <summary>
        /// Did we need debug display?
        /// </summary>
        [SerializeField]
        private bool activateGizmos;


        [Header("Points")]

        /// <summary>
        /// Start point of the curve.
        /// </summary>
        [SerializeField]
        private Transform startPoint;

        /// <summary>
        /// Parameter point of the start point.
        /// </summary>
        [SerializeField]
        private Transform parameterStartPoint;

        /// <summary>
        /// End point of the curve.
        /// </summary>
        [SerializeField]
        private Transform endPoint;

        /// <summary>
        /// Parameter point of the end point.
        /// </summary>
        [SerializeField]
        private Transform parameterEndPoint;


        /// <summary>
        /// List of point creating the curve.
        /// </summary>
        public List<Vector3> Curve { get; } = new();



        /// <summary>
        /// Awake method in order to initialize.
        /// </summary>
        private void Awake()
        {
            ComputePath();
        }


    
        /// <summary>
        /// On draw Gizmos is used to display curve in editor.
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            if (activateGizmos)
            {
                Curve.Clear();
                ComputePath();

                for (int i = 0; i < Curve.Count - 1; i ++)
                    Gizmos.DrawLine(Curve[i], Curve[i + 1]);
            }
        }
    


        /// <summary>
        /// Method used to compute the bezier curve.
        /// </summary>
        private void ComputePath()
        {
            Vector3 a = startPoint.position;
            Vector3 d = endPoint.position;
            Vector3 b = parameterStartPoint.position;
            Vector3 c = parameterEndPoint.position;

            for (int i = 1; i <= numberOfIterations; i++)
            {
                float time = i * 0.01f;
                float oneMinusT = 1f - time;

                Vector3 q = oneMinusT * a + time * b;
                Vector3 r = oneMinusT * b + time * c;
                Vector3 s = oneMinusT * c + time * d;

                Vector3 p = oneMinusT * q + time * r;
                Vector3 t = oneMinusT * r + time * s;

                Curve.Add(oneMinusT * p + time * t);
            }
        }
    }
}