using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    [SerializeField] Transform target;

    NavMeshAgent agent;
    NavMeshLink link;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.GetPause())
        {
            agent.SetDestination(target.position);

            if (agent.isOnOffMeshLink)
            {
                agent.CompleteOffMeshLink();
            }
        }
        else
        {
            agent.SetDestination(transform.position);
        }
    }
}
