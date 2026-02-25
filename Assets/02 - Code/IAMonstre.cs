using UnityEngine;
using UnityEngine.AI;

public class IAMonstre : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Transform _joueur;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        
        GameObject joueurObj = GameObject.FindGameObjectWithTag("Player");
        if (joueurObj != null)
        {
            _joueur = joueurObj.transform;
        }
    }

    void Update()
    {
        if (_joueur is not null)
        {
            _agent.SetDestination(_joueur.position);
        }
    }
}