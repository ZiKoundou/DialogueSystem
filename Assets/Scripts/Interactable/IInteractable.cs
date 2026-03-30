public interface IInteractable
{
    string GetPromptText();   // What the UI should show
    void Interact();          // What happens when player interacts
}