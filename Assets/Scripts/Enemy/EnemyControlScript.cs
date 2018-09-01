using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControlScript : MonoBehaviour
{
    public float moveSpeed;
    public int maxEnergy;
    private int energy;
    public GameObject[] notAffected;
    public GameObject[] wayPoints;
    private bool isCaptured;
    private int nextWayPoint;
    private bool isActive = true;

    // Use this for initialization
    void Start()
    {
        nextWayPoint = 0;
        isCaptured = false;
        energy = maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator moveTowardsNext()
    {
        while (!isCaptured && isActive && Vector3.Distance(transform.position, wayPoints[nextWayPoint].transform.position) > 0.1)
        {
            transform.GetComponent<Rigidbody2D>().velocity = ((wayPoints[nextWayPoint].transform.position - transform.position).normalized * moveSpeed *((energy +moveSpeed)/(maxEnergy + moveSpeed)));
            //transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            //transform.position = Vector3.MoveTowards(transform.position, wayPoints[nextWayPoint].transform.position, moveSpeed);
            yield return null;
        }
    }

    public IEnumerator moveTowardsAttractor(Transform attractor)
    {
        while (Vector3.Distance(transform.position, attractor.position) > 0.5)
        {
            //transform.GetComponent<Rigidbody2D>().velocity = ((attractor.position - transform.position).normalized * moveSpeed);
            transform.position = Vector3.MoveTowards(transform.position, attractor.position, moveSpeed * ((energy + moveSpeed) / (maxEnergy + moveSpeed)) * Time.deltaTime);
            yield return null;
        }
    }

    public IEnumerator derez(float timeSpentIn, BAttraction Attractor)
    {
        transform.GetComponent<Rigidbody2D>().velocity = new Vector3(0,0,0);
        gameObject.GetComponent<Renderer>().enabled = false;
        Attractor.currCapacity++;
        yield return new WaitForSeconds(timeSpentIn);
        Attractor.currCapacity--;
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
                StartCoroutine(moveTowardsAttractor(Attractor.transform));
            } 
        }
        if(other.transform.tag == "Attraction")
        {
            Debug.Log("Uh oh");
            BAttraction Attractor = other.transform.gameObject.GetComponent<BAttraction>();
            StartCoroutine(derez(Attractor.timeSpentIn, Attractor));
            //StartCoroutine(Attractor.holdTime(Attractor.timeSpentIn));
            energy -= Attractor.energySubtraction;
        }
        if (other.transform.tag == "Waypoint")
        {
            ++nextWayPoint;
            StartCoroutine(moveTowardsNext());
        }
        if(other.transform.tag=="Final")
        {
            if (energy <= 0)
                nextWayPoint += 2;
            else
                ++nextWayPoint;
            StartCoroutine(moveTowardsNext());
        }
        if(other.transform.tag == "Town" || other.transform.tag == "Hotel")
        {
            isActive = false;
            Destroy(this.gameObject);
        }
    }
}
