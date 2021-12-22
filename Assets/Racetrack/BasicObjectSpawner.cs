using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicObjectSpawner : MonoBehaviour, ITriggerListener
{
    public GameObject objectPrefab;

    public Trigger trigger;

    private IRacetrack _racetrack;

    // Start is called before the first frame update
    void Start()
    {
        _racetrack = GetComponent<IRacetrack>();
        trigger.AddListener(this);
    }

    public void OnTriggered()
    {
        var position = new Vector3(Random.Range(_racetrack.Left, _racetrack.Right), _racetrack.Y, 0);
        Instantiate(objectPrefab, position, Quaternion.identity);
    }
}
