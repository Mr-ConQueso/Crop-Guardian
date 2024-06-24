using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class GroundEnemy : EnemyController
    {
        private NavMeshAgent navMeshAgent;
        private Transform playerTransform;

        protected override void Move()
        {
            navMeshAgent.isStopped = false;
        }

        protected override void StopMoving()
        {
            navMeshAgent.isStopped = true;
        }

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

            navMeshAgent.speed = moveSpeed;
            
            if (playerTransform != null)
            {
                navMeshAgent.SetDestination(playerTransform.position);
            }
        }
    }
}
