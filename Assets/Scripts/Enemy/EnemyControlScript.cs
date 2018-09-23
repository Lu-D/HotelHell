using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//EnemyControlScript
//Sets enemy behavior and modifies dependant scripts
public class EnemyControlScript : MonoBehaviour
{
    public float moveSpeed;
    public GameObject[] notAffected;
    public GameObject[] wayPoints;
    public bool isCaptured;
    public bool isLeaving;
    public int hotelSpace;
    public bool isLast;
    private int nextWayPoint;
    private int waveEntered;
    private bool isActive = true;
    private Vector3 capturedTransform;
    private float cardDegrees;
    private Animator anim;

    public AudioClip cash;
    public AudioClip bell;
    private AudioSource audioSource;

    //Awake
    //set necessary booleans and find dependant scripts
    void Awake()
    {
        nextWayPoint = 0;
        isCaptured = false;
        isLast = false;
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    //updateAnim
    //updates animator with the direction of movement for animation
    void updateAnim(Vector3 origin, Vector3 target)
    {
        anim.SetBool("Up", false);
        anim.SetBool("Down", false);
        anim.SetBool("Left", false);
        anim.SetBool("Right", false);
        Vector3 direction = target - origin;
        if(Vector3.Angle(Vector3.up, direction) <= 45)
        {
            anim.SetBool("Up", true);
        }
        else if(Vector3.Angle(Vector3.down, direction) <= 45)
        {
            anim.SetBool("Down", true);
        }
        else if (Vector3.Angle(Vector3.right, direction) < 45)
        {
            anim.SetBool("Right", true);
        }
        else if (Vector3.Angle(Vector3.left, direction) < 45)
        {
            anim.SetBool("Left", true);
        }
    }

    //moveTowardsNext
    //gives enemy a velocity towards next waypoint
    public void moveTowardsNext()
    {
        updateAnim(transform.position, wayPoints[nextWayPoint].transform.position);
        transform.GetComponent<Rigidbody2D>().velocity = ((wayPoints[nextWayPoint].transform.position - transform.position).normalized * moveSpeed);
    }

    //moveTowardsAttractor
    //moves enemy towards atraction using moveTowards
    public IEnumerator moveTowardsAttractor(Transform attractor)
    {
        audioSource.clip = cash;
        audioSource.Play();
        updateAnim(transform.position, attractor.position);
        Debug.Log("movetowards Attractor");
        while (isCaptured && Vector3.Distance(transform.position, attractor.position) > 0.4)
        {
            transform.position = Vector3.MoveTowards(transform.position, attractor.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    //moveTowardsExit
    //moves enemy away from attraction using moveTowards
    public IEnumerator moveTowardsExit(Vector3 exit)
    {
        audioSource.clip = bell;
        audioSource.Play();
        updateAnim(transform.position, exit);
        while (Vector3.Distance(transform.position, exit) > 0.01)
        {
            transform.position = Vector3.MoveTowards(transform.position, exit, moveSpeed * Time.deltaTime);
            yield return null;
        }

        isLeaving = false;

        moveTowardsNext();
    }

    //derez
    //tracks enemy for duration it spends inside of an attraction
    //increments money and capacity of attraction
    public IEnumerator derez(float timeSpentIn, BAttraction Attractor)
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        Attractor.currCapacity++;

        ClickToBuild UIcontrol = GameObject.Find("PlayerController").GetComponent<ClickToBuild>();
        UIcontrol.currMoney += Attractor.moneyEarned;

        yield return new WaitForSeconds(timeSpentIn);
        
      
        Attractor.currCapacity--;
        gameObject.GetComponent<Renderer>().enabled = true;
        isLeaving = true;
        isCaptured = false;
        yield return moveTowardsExit(capturedTransform);
    }

    //OnTriggerEnter2D
    //Handles collisions with all objects
    void OnTriggerEnter2D(Collider2D other)
    {
        //When an enemy walks into field of an attraction
        if (other.transform.tag == "Attraction Collider" )
        {
            GameObject Attractor = other.transform.parent.gameObject;

            waveEntered = GameObject.Find("SpawnPoint").GetComponent<SpawnControlScript>().waveNumber;

            if (Attractor.GetComponent<BAttraction>().currCapacity < Attractor.GetComponent<BAttraction>().maxCapacity)
            {
                transform.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
                isCaptured = true;
                capturedTransform = transform.position;
                StartCoroutine(moveTowardsAttractor(Attractor.transform));
            } 
        }
        //When an enemy walks into attraction
        else if(other.transform.tag == "Attraction" )
        {
            BAttraction Attractor = other.transform.gameObject.GetComponent<BAttraction>();
            StartCoroutine(derez(Attractor.timeSpentIn, Attractor));

        }
        //waypoint system for determining path enemies should follow
        else if (other.transform.tag == "Waypoint")
        {
            if(!isLeaving)
            {
                ++nextWayPoint;
                moveTowardsNext();
            }
        }
        //move off screen 
        else if (other.transform.tag == "Final")
        {
            ++nextWayPoint;
            moveTowardsNext();
        }
        //adjust variables if enemy makes it into town
        else if(other.transform.tag == "Town")
        {
            //Debug.Log("good1");
            if (isLast)
            {
                GameObject.Find("StartButton").GetComponent<Button>().interactable = true;
                isLast = false;
            }

            TownControlScript control = other.transform.gameObject.GetComponent <TownControlScript>();
            control.trashCount += hotelSpace;
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
