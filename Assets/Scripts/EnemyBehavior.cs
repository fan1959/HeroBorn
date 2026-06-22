using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Player;
    public Transform PatrolRoute;
    public List<Transform> Locations;

    private int _locationIndex = 0;
    private NavMeshAgent _agent;

    private int _lives= 3;
    public int EnemtLives{
        get{return _lives;} 
        private set
        {
            _lives=value;
            if(_lives<=0)
            {
                Destroy(this.gameObject);
                Debug.Log("Enemy Down");
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            _agent.destination = Player.position;
            Debug.Log("Enemy detected!");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            
        }
    }
    void Start()
    {
        _agent=GetComponent<NavMeshAgent>();
        Player=GameObject.Find("Player").transform;
        InitializePatrolRoute();

        MoveToNextPatrolLocation();
    }

    // Update is called once per frame
    void InitializePatrolRoute()
    {
        foreach (Transform child in PatrolRoute)
        {
            Locations.Add(child);
        }
    }

    void MoveToNextPatrolLocation()
    {
        if (Locations.Count == 0)
            return;

        _agent.destination = Locations[_locationIndex].position;
        _locationIndex=(_locationIndex+1)%Locations.Count;
    }
    void Update()
    {
        if (_agent.remainingDistance < 0.2f && !_agent.pathPending)
        {
            MoveToNextPatrolLocation();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bullet")
        {
            EnemtLives -= 1;
            Debug.Log("Criticalhit!");
        }
    }
}

