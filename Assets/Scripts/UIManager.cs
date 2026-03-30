using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : Singleton<UIManager>
{
    public GameObject UICanvas;
    public TMPro.TextMeshProUGUI dialogueText;
    public GameObject buttonPrefab;
    public Transform buttonContainer;

    private List<GameObject> activeButtons = new List<GameObject>();
    private void OnEnable()
    {
        EventDispatcher.instance.AddListener<ShowUI>(ShowDialogueText);
        EventDispatcher.instance.AddListener<ShowResponses>(ShowResponseButtons);
        EventDispatcher.instance.AddListener<HideUI>(HideCanvas);
    }

    private void OnDisable()
    {
        EventDispatcher.instance.RemoveListener<ShowUI>(ShowDialogueText);
        EventDispatcher.instance.RemoveListener<ShowResponses>(ShowResponseButtons);
        EventDispatcher.instance.RemoveListener<HideUI>(HideCanvas);
    }

    private void HideCanvas(HideUI eventData)
    {
        UICanvas.SetActive(false);
    }
    private void ShowResponseButtons(ShowResponses response)
    {
        foreach(UIResponseData responseData in response.responses)
        {
            GameObject button = GameObject.Instantiate(buttonPrefab, buttonContainer);

            TMPro.TextMeshProUGUI buttonText = 
                button.GetComponentInChildren<TMPro.TextMeshProUGUI>();

            buttonText.text = responseData.text;

            Button uiButton = button.GetComponentInChildren<Button>();
            uiButton.onClick.AddListener(responseData.buttonAction);
            uiButton.onClick.AddListener(CleanupButtons);

            activeButtons.Add(button);
        }
    }

    private void CleanupButtons()
    {
        foreach(GameObject button in activeButtons)
        {
            Destroy(button);
        }

        activeButtons.Clear();
    }
    private void ShowDialogueText(ShowUI evtData)
    {
        UICanvas.SetActive(true);
        dialogueText.text = evtData.text;

        Debug.Log("YOOOO!  This works!!!!!!");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
