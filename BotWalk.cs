using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace bot
{
    public class BotWalk : MonoBehaviour
    {
        public GameObject player;
        public static NavMeshAgent agent;
        private float waitTime;
        private float startWaitTime;
        public Transform[] moveSport;
        private int randomSpot;
        public Transform Home;
        private bool isGoHome;
        private Transform DistanationTars;

        void Start()
        {
            player.tag = "Player";
            player.AddComponent<NavMeshAgent>();
            player.AddComponent<Rigidbody>();
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            player.AddComponent<BoxCollider>().size = new Vector3(2, 2, 2);
            player.GetComponent<BoxCollider>().isTrigger = true;
            isGoHome = true;
            agent = GetComponent<NavMeshAgent>();
            agent.agentTypeID = 0;
            startWaitTime = 1f;
            waitTime = startWaitTime;
            randomSpot = Random.Range(0, moveSport.Length);
            DistanationTars = moveSport[randomSpot];
            agent.SetDestination(DistanationTars.position);
            
        }

        void CheckDistanation()
        {
            if (Vector3.Distance(transform.position, DistanationTars.position) < 0.5f)
            {
                if (waitTime <= 0)
                {
                    if (isGoHome)
                    {
                        DistanationTars = Home;
                        isGoHome = false;
                    }
                    else
                    {
                        randomSpot = Random.Range(0, moveSport.Length);
                        DistanationTars = moveSport[randomSpot];
                        isGoHome = true;
                    }
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
                agent.SetDestination(DistanationTars.position);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                Debug.Log(other.name);
            }
                
            
        }
        void Update()
        {
            CheckDistanation();
        }
    }
}
