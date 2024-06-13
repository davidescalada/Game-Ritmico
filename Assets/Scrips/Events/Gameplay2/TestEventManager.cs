using UnityEngine;

public class TestEventManager : MonoBehaviour
{
    public EventManager eventManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            eventManager.TriggerRandomEvent();
        }
    }
}
