using System.Collections;
using UnityEngine;

/// <summary>
/// Class used to emits a small light in dark environnements.
/// </summary>
public class Fly : MonoBehaviour
{
    /// <summary>
    /// Movement max of the fly.
    /// </summary>
    [SerializeField]
    private float _movementMax;


    /// <summary>
    /// Goal position.
    /// </summary>
    private Vector3 _goalPosition;

    /// <summary>
    /// Speed of the fly, computed each move.
    /// </summary>
    private float _speed;


    /// <summary>
    /// Start used as coroutine, each time a fly travel it computes a new distance and speed.
    /// </summary>
    private IEnumerator Start()
    {
        while (true)
        {
            _speed = Random.Range(0.01f, 0.15f);
            _goalPosition = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * _movementMax;
            yield return new WaitUntil(() => transform.position == _goalPosition);
        }
    }


    /// <summary>
    /// Update is called each frame.
    /// </summary>
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _goalPosition, Time.deltaTime * _speed);
    }
}