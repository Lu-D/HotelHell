using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyControlScript : MonoBehaviour
{
    public float moveSpeed;
    public int maxEnergy;
    private int energy;
    public GameObject[] notAffected;
    public GameObject[] wayPoints;
    private bool isCaptured;
    private Transform capturedTransform;
    private bool isLeaving;
    private int nextWayPoint;
    private bool isActive = true;

    // Use this for initialization
    void Start()
    {

    }

    void Awake()
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

    public IEnumerator moveTowardsExit(Transform exit)
    {
        while (Vector3.Distance(transform.position, exit.position) > 0.1)
        {
            //transform.GetComponent<Rigidbody2D>().velocity = ((attractor.position - transform.position).normalized * moveSpeed);
            transform.position = Vector3.MoveTowards(transform.position, exit.position, moveSpeed * ((energy + moveSpeed) / (maxEnergy + moveSpeed)) * Time.deltaTime);
            yield return null;
        }
        isLeaving = false;
        StartCoroutine(moveTowardsNext());
    }

    public IEnumerator derez(float timeSpentIn, BAttraction Attractor)
    {
        transform.GetComponent<Rigidbody2D>().velocity = new Vector3(0,0,0);
        gameObject.GetComponent<Renderer>().enabled = false;
        Attractor.currCapacity++;

        ClickToBuild UIcontrol = GameObject.Find("PlayerController").GetComponent<ClickToBuild>();
        UIcontrol.currMoney += Attractor.moneyEarned;
        yield return new WaitForSeconds(timeSpentIn);
        Attractor.currCapacity--;
        gameObject.GetComponent<Renderer>().enabled = isLeaving = true;
        isCaptured = false;
        moveTowardsExit(capturedTransform);

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.transform.tag == "Attraction Collider" && energy > 0)
        {
            GameObject Attractor = other.transform.parent.gameObject;
            if(Attractor.GetComponent<BAttraction>().currCapacity < Attractor.GetComponent<BAttraction>().maxCapacity)
            {
                isCaptured = true;
                capturedTransform = transform;
                StartCoroutine(moveTowardsAttractor(Attractor.transform));
            } 
        }
        else if(other.transform.tag == "Attraction")
        {
            BAttraction Attractor = other.transform.gameObject.GetComponent<BAttraction>();
            StartCoroutine(derez(Attractor.timeSpentIn, Attractor));
            //StartCoroutine(Attractor.holdTime(Attractor.timeSpentIn));
            energy -= Attractor.energySubtraction;
        }
        else if (other.transform.tag == "Waypoint")
        {
            if(!isLeaving)
            {
                ++nextWayPoint;
                StartCoroutine(moveTowardsNext());
            }
        }
        else if(other.transform.tag=="Final")
        {
            if (energy <= 0)
                nextWayPoint += 2;
            else
                ++nextWayPoint;
            StartCoroutine(moveTowardsNext());
        }
        else if(other.transform.tag == "Town")
        {
            TownControlScript control = other.transform.gameObject.GetComponent <TownControlScript>();
            control.trashCount += energy * control.trashPerEnergy;
            if(control.trashCount > control.trashCapacity)
            {
                SceneManager.LoadScene("gameOverScene");
            }
            isActive = false;
            Destroy(this.gameObject);
        }
        else if(other.transform.tag == "Hotel")
        {
            isActive = false;
            Destroy(this.gameObject);
        }
    }
}
