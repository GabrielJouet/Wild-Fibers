using System.Collections;
using UnityEngine;

public class Fly : MonoBehaviour
{
    [SerializeField]
    private float _movementMax;


    private Vector3 _goalPosition;

    private float _speed;


    private IEnumerator Start()
    {
        while (true)
        {
            _speed = Random.Range(0.01f, 0.15f);
            _goalPosition = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * _movementMax;
            yield return new WaitUntil(() => transform.position == _goalPosition);
        }
    }


    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _goalPosition, Time.deltaTime * _speed);
    }
}