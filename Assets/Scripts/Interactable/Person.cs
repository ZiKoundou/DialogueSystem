using UnityEngine;

public class Person : MonoBehaviour, IInteractable
{
    public string dialogueName;
    public string GetPromptText() => "Press E";

    public void Interact()
    {
        DialogueManager.instance.StartDialogue(dialogueName);
    }
}