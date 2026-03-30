using UnityEngine;

public class Person : MonoBehaviour, IInteractable
{
    public string dialogueName;
    public string GetPromptText() => "Press E";

    public void Interact()
    {
        Debug.Log("Dialogue Opened");
        if (Input.GetKeyUp(KeyCode.E))
        {
            DialogueManager.instance.StartDialogue(dialogueName);
        }
    }
}