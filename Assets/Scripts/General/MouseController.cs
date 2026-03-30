using UnityEngine;

public class MouseController : MonoBehaviour
{
    FirstPersonController fpc;
    void Start()
    {
        fpc = FindFirstObjectByType<FirstPersonController>();
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
        fpc.cameraCanMove = playerstate.value;
        fpc.playerCanMove = playerstate.value;
        fpc.enableHeadBob = playerstate.value;
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