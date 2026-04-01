
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class UIManager : Singleton<UIManager>
{
    
    public float charSpeed = 0.2f;
    public GameObject UICanvas;
    public TMPro.TextMeshProUGUI dialogueText;
    public GameObject buttonPrefab;
    public Transform buttonContainer;
    public RawImage profPic;
    public GameObject profPicContainer;
    public TMPro.TextMeshProUGUI characterText;


    private List<GameObject> activeButtons = new List<GameObject>();
    private TextReveal typeWriter = null;
    private bool isRevealing = false;
    private void OnEnable()
    {
        EventDispatcher.instance.AddListener<ShowUI>(ShowDialogueText);
        EventDispatcher.instance.AddListener<ShowResponses>(ShowResponseButtons);
        EventDispatcher.instance.AddListener<HideUI>(HideCanvas);
        EventDispatcher.instance.AddListener<ShowVisualPortrait>(ShowVisualPortraitImage);
        EventDispatcher.instance.AddListener<HideVisualPortrait>(HideVisualPortraitImage);
        EventDispatcher.instance.AddListener<ShowCharacterName>(ShowCharacterNameText);
    }



    private void OnDisable()
    {
        EventDispatcher.instance.RemoveListener<ShowUI>(ShowDialogueText);
        EventDispatcher.instance.RemoveListener<ShowResponses>(ShowResponseButtons);
        EventDispatcher.instance.RemoveListener<HideUI>(HideCanvas);
        EventDispatcher.instance.RemoveListener<ShowVisualPortrait>(ShowVisualPortraitImage);
        EventDispatcher.instance.RemoveListener<HideVisualPortrait>(HideVisualPortraitImage);
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
        typeWriter = new TextReveal(evtData.text, charSpeed);
        dialogueText.text = typeWriter.GetTextReveal();
        isRevealing = true;
        
        
    }

    private void ShowVisualPortraitImage(ShowVisualPortrait imageData)
    {
        if (imageData.texture != null)
        {
            profPic.texture = imageData.texture;
        }
        profPicContainer?.SetActive(true);
    }

    private void HideVisualPortraitImage(HideVisualPortrait imageData)
    {
        profPicContainer?.SetActive(false);
    }

    private void ShowCharacterNameText(ShowCharacterName nameData)
    {
        characterText.text = nameData.text;
    }
    // Update is called once per frame
    void Update()
    {
        if (isRevealing)// if the text is not revealed
        {
            typeWriter.Update();// update the time 
            dialogueText.text = typeWriter.GetTextReveal();// get the current index
            if(typeWriter.IsTextRevealed())
            {
                isRevealing = false;
            }
        }
        
    }
}
