using UnityEngine;

public class MouseController : MonoBehaviour
{
    FirstPersonController fps;
    void Start()
    {
        fps = FindFirstObjectByType<FirstPersonController>();
    }
    void OnEnable()
    {
        EventDispatcher.instance.AddListener<ToggleLock>(MouseStateDialougueToggle);
    }
    void Disable()
    {
        EventDispatcher.instance.RemoveListener<ToggleLock>(MouseStateDialougueToggle);
    }

    //false: 
    // camera cannot move
    // player cannot move
    // mouse unlocked
    //true: normal fpsstuff
    private void MouseStateDialougueToggle(ToggleLock playerstate)
    {
        fps.cameraCanMove = playerstate.value;
        fps.playerCanMove = playerstate.value;
        fps.enableHeadBob = playerstate.value;
        if (playerstate.value)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}