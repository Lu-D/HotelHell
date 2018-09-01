using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControlScript : MonoBehaviour
{
    public float moveSpeed;
    public int energy;
    public GameObject[] notAffected;
    public GameObject[] wayPoints;
    private bool isCaptured;
    private int nextWayPoint;

    // Use this for initialization
    void Start()
    {
        nextWayPoint = 0;
        isCaptured = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isCaptured);
    }

    public IEnumerator moveTowardsNext()
    {
        while (!isCaptured && Vector3.Distance(transform.position, wayPoints[nextWayPoint].transform.position) > 0.1)
        {
            transform.GetComponent<Rigidbody2D>().velocity = ((wayPoints[nextWayPoint].transform.position - transform.position).normalized * moveSpeed);
            //transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            //transform.position = Vector3.MoveTowards(transform.position, wayPoints[nextWayPoint].transform.position, moveSpeed);
            yield return null;
        }
    }

    public IEnumerator moveTowardsAtractor(Transform attractor)
    {
        while (Vector3.Distance(transform.position, attractor.position) > 0.5)
        {
            //transform.GetComponent<Rigidbody2D>().velocity = ((attractor.position - transform.position).normalized * moveSpeed);
            transform.position = Vector3.MoveTowards(transform.position, attractor.position, moveSpeed*Time.deltaTime);
            yield return null;

        }
    }

    public IEnumerator derez(float timeSpentIn)
    {
        transform.GetComponent<Rigidbody2D>().velocity = new Vector3(0,0,0);
        gameObject.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(timeSpentIn);
        gameObject.GetComponent<Renderer>().enabled = true;
        isCaptured = false;
        StartCoroutine(moveTowardsNext());
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.transform.tag == "Attraction Collider")
        {
            GameObject Attractor = other.transform.parent.gameObject;
            if(Attractor.GetComponent<BAttraction>().currCapacity < Attractor.GetComponent<BAttraction>().maxCapacity)
            {
                isCaptured = true;
                StartCoroutine(moveTowardsAtractor(Attractor.transform));
            } 
        }
        if(other.transform.tag == "Attraction")
        {
            StartCoroutine(derez(other.transform.gameObject.GetComponent<BAttraction>().timeSpentIn));
        }
        if (other.transform.tag == "Waypoint")
        {
            ++nextWayPoint;
            StartCoroutine(moveTowardsNext());
        }

    }
}
