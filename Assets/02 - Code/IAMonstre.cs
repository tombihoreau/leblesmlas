using UnityEngine;
using UnityEngine.AI;

public class IAMonstre : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Transform _joueur;
    private PlayerHealth _playerHealth;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        
        GameObject joueurObj = GameObject.FindGameObjectWithTag("Player");
        if (joueurObj != null)
        {
            _joueur = joueurObj.transform;
            _playerHealth = joueurObj.GetComponent<PlayerHealth>();
        }
    }

    void Update()
    {
        // On vérifie si le joueur existe ET s'il est encore en vie
        if (_joueur is not null && _playerHealth is not null && !_playerHealth.estMort)
        {
            _agent.SetDestination(_joueur.position);
        }
        else
        {
            if (_agent.enabled && _agent.isOnNavMesh)
            {
                _agent.isStopped = true; 
            }
        }
    }
}