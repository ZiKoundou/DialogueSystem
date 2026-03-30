using UnityEngine;
using TMPro;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private GameObject root;
    [SerializeField] private TextMeshProUGUI promptText;

    public void Show(string text)
    {
        promptText.text = text;
        root.SetActive(true);
    }

    public void Hide()
    {
        root.SetActive(false);
    }
}