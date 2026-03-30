using UnityEngine;

public class Person : MonoBehaviour, IInteractable
{
    public string GetPromptText() => "Press E";

    public void Interact()
    {
        Debug.Log("Dialogue Opened");
        // play animation, give loot, etc.
    }
}