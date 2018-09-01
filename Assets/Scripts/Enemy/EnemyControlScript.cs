using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControlScript : MonoBehaviour
{
    public float moveSpeed;
    public int energy;
    public GameObject[] notAffected;
    public GameObject[] wayPoints;
    public bool isCaptured;
    public bool isLeaving;
    private int nextWayPoint;

    // Use this for initialization
    void Start()
    {
        nextWayPoint = 0;
        isCaptured = isLeaving = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator moveTowardsNext()
    {
        while (!isCaptured && Vector3.Distance(transform.position, wayPoints[nextWayPoint].transform.position) > 0.1)
        {
            transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            transform.position = Vector3.MoveTowards(transform.position, wayPoints[nextWayPoint].transform.position, moveSpeed);
            yield return null;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided");
        ++nextWayPoint;
        StartCoroutine(moveTowardsNext());
        Debug.Log("Called Coroutine");
        Debug.Log(nextWayPoint);
    }
}
