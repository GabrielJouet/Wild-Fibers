using System.Collections.Generic;
using UnityEngine;

namespace Levels.Path
{
    /// <summary>
    /// Class used to store and generate new paths.
    /// </summary>
    public class RandomPath : MonoBehaviour
    {
        /// <summary>
        /// First curve in game.
        /// </summary>
        [SerializeField]
        private List<BezierCurve> firstBezierCurves;

        /// <summary>
        /// Second curve in game.
        /// </summary>
        [SerializeField]
        private List<BezierCurve> secondBezierCurves;

        /// <summary>
        /// How many paths we want to generate.
        /// </summary>
        [SerializeField]
        private int numberOfAlreadyGeneratedPaths;


        /// <summary>
        /// Already generated paths.
        /// </summary>
        private readonly List<List<Vector2>> _alreadyGeneratedPaths = new();


        /// <summary>
        /// One generated path given randomly.
        /// </summary>
        public List<Vector2> GeneratedPath => _alreadyGeneratedPaths[Random.Range(0, numberOfAlreadyGeneratedPaths)];


        /// <summary>
        /// Start method, used for initialization.
        /// </summary>
        private void Start()
        {
            List<Vector3> firstPath = new List<Vector3>();
            List<Vector3> secondPath = new List<Vector3>();
            
            foreach (BezierCurve current in firstBezierCurves)
                foreach (Vector3 currentVector in current.Curve)
                    firstPath.Add(currentVector);

            foreach (BezierCurve current in secondBezierCurves)
                foreach (Vector3 currentVector in current.Curve)
                    secondPath.Add(currentVector);

            for (int i = 0; i < numberOfAlreadyGeneratedPaths; i ++)
            {
                var ratio = Random.Range(0, 100f);
                var newPath = new List<Vector2>();

                for (int j = 0; j < firstPath.Count; j++)
                    newPath.Add((ratio * firstPath[j] + (100f - ratio) * secondPath[j]) / 100f);

                _alreadyGeneratedPaths.Add(newPath);
            }
        }
    }
}