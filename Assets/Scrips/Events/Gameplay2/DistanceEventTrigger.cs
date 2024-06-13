using UnityEngine;

public class DistanceEventTrigger : MonoBehaviour
{
    public EventManager eventManager;
    public Transform player;
    public Transform enemy;

    private float initialDistance;
    private bool triggered75 = false;
    private bool triggered50 = false;
    private bool triggered25 = false;
    private bool triggered5 = false;

    void Start()
    {
        initialDistance = Vector3.Distance(player.position, enemy.position);
    }

    void Update()
    {
        float currentDistance = Vector3.Distance(player.position, enemy.position);
        float distancePercentage = (currentDistance / initialDistance) * 100;

        if (!triggered75 && distancePercentage <= 75)
        {
            eventManager.TriggerRandomEvent();
            triggered75 = true;
        }
        else if (!triggered50 && distancePercentage <= 55)
        {
            eventManager.TriggerRandomEvent();
            triggered50 = true;
        }
        else if (!triggered25 && distancePercentage <= 35)
        {
            eventManager.TriggerRandomEvent();
            triggered25 = true;
        }
        else if (!triggered5 && distancePercentage <= 15)
        {
            Debug.Log("SE SUPONE QUE ESTE ES EL EVENTO2, VIBRACION ?");
            eventManager.TriggerSpecificEvent(1); // Asumiendo que el evento de vibración es el Event2
            triggered5 = true;
        }
    }
}

