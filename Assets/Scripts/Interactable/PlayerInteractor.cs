using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Range Parameters")]
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private LayerMask interactMask;
    [SerializeField] private InteractionUI ui;   // reference to your UI script

    private IInteractable currentInteractable;

    void Update()
    {
        CheckForInteractable();

        if (currentInteractable != null && Input.GetKeyDown(KeyCode.E))
        {
            currentInteractable.Interact();
            ui.Hide();
        }
    }

    void CheckForInteractable()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactMask))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (interactable != currentInteractable)
                {
                    currentInteractable = interactable;
                    ui.Show(interactable.GetPromptText());
                }
                return;
            }
        }

        // If we reach here, nothing interactable is hit
        currentInteractable = null;
        ui.Hide();
    }
}