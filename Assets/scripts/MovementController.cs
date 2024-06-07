using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject cubeToGo;
    public GameObject ballToGo;
    public GameObject yellowBox;
    public GameObject door;
    public GameObject hatToGo;
    public GameObject junkToGo;
    public GameObject recyclingMachine;
    public GameObject shop;

    private CommandHandler commandHandler;

    void Start()
    {
        commandHandler = GetComponent<CommandHandler>();
    }

    public void HandleGoCommand(string command)
    {
        if (command == "go to the cube")
        {
            MoveAgentTo(cubeToGo);
        }
        else if (command == "go to the hat")
        {
            MoveAgentTo(hatToGo);
        }
        else if (command == "go to the ball")
        {
            MoveAgentTo(ballToGo);
        }
        else if (command == "go to the box")
        {
            MoveAgentTo(yellowBox);
        }
        else if (command == "go to the door")
        {
            MoveAgentTo(door);
        }
        else if (command == "go to the junk")
        {
            MoveAgentTo(junkToGo);
        }
        else if (command == "go to the machine")
        {
            MoveAgentTo(recyclingMachine);
        }
        else if (command == "go to the shop")
        {
            MoveAgentTo(shop);
        }
        else
        {
            commandHandler.ShowFeedback("Unknown command");
        }
    }

    void MoveAgentTo(GameObject target)
    {
        if (target != null)
        {
            agent.SetDestination(target.transform.position);
        }
        else
        {
            commandHandler.ShowFeedback("Target not found");
        }
    }
}
