using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControlScript : MonoBehaviour {
    public float moveSpeed;
    public int energy;
    public GameObject[] notAffected;
    public GameObject[] wayPoints;
    public bool isCaptured;
    public bool isLeaving;
    private int nextWayPoint;

	// Use this for initialization
	void Start () {
        isCaptured = isLeaving = false;
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(moveTowardsNext());
	}

    public IEnumerator moveTowardsNext()
    {
        while (Vector3.Distance(transform.position, wayPoints[nextWayPoint].transform.position) > 0.1)
        {
            transform.position = Vector3.MoveTowards(transform.position, wayPoints[nextWayPoint].transform.position, moveSpeed*Time.deltaTime);
        }

        yield return null;
    }
}
