using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueManager : Singleton<DialogueManager>
{
    public DialogueDatabase database;
    DialogueLine m_currentLine = null;
    private const float kDefaultTime = 5.0f;
    private float m_currentWaitTime = 0.0f;
    private float m_currentTime = 0.0f;

    Dictionary<string, DialogueLine> m_DialogueTable = new Dictionary<string, DialogueLine>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(DialogueLine line in database.database)
        {
            m_DialogueTable.Add(line.name, line);
        }  
    }

    // Update is called once per frame
    void Update()
    {


        if (m_currentLine != null && m_currentWaitTime > 0.0f)
        {
            m_currentTime += Time.deltaTime;
            if (m_currentTime > m_currentWaitTime)
            {
                if (m_currentLine.responses.Count > 0)
                {
                    // if (m_currentLine.responses.Count == 1)
                    // {
                    //     StartDialogue(m_currentLine.responses[0].name);
                    // }
                    // else
                    // {
                        //Timer is Done
                        List<UIResponseData> lines = new List<UIResponseData>();
                        int index = 0;
                        foreach (DialogueLine line in m_currentLine.responses)
                        {
                            UIResponseData response = new UIResponseData();
                            response.text = line.dialogue;

                            int currentIndex = index;
                            response.buttonAction = () => { PlayResponse(currentIndex); };
                            index++;

                            lines.Add(response);
                        }

                        EventDispatcher.instance.SendEvent<ShowResponses>(new ShowResponses
                        {
                            responses = lines
                        });
                    // }
                }
                else
                {
                    EventDispatcher.instance.SendEvent<HideUI>(new HideUI());
                    EventDispatcher.instance.SendEvent<ToggleLock>(new ToggleLock {value = true});
                }    

                m_currentWaitTime = 0.0f;
            }
        }
        
    }

    public void Test()
    {

    }

    public void PlayResponse(int index)
    {
        if (m_currentLine != null)
        {
            if (index >= 0 && index < m_currentLine.responses.Count)
            {
                DialogueLine responseLine = m_currentLine.responses[index];
                StartDialogue(responseLine.name);
            }
        }
    }

    public void StartDialogue(string dialogueName)
    {
        if (m_DialogueTable.TryGetValue(dialogueName, out m_currentLine))
        {
            m_currentWaitTime = kDefaultTime;
            EventDispatcher.instance.SendEvent<ToggleLock>(new ToggleLock {value = false});
            EventDispatcher.instance.SendEvent<ShowUI>(new ShowUI
            {
                text = m_currentLine.dialogue,
            });
            //Tell UI system to show dialogue
            //Tell Audio System to play audio
            if (m_currentLine.dialogueAudio != null)
            {
                m_currentWaitTime = m_currentLine.dialogueAudio.length;
                //Tell AUdio system to start playing.
                EventDispatcher.instance.SendEvent<PlaySound>(new PlaySound
                {
                    sound = m_currentLine.dialogueAudio,
                });
            }
            //Wait for time to expire
            //Tell UI to show responses
        }
        else
        {
            Debug.Log(string.Format("Couldn't find Dialgogue Line {0}", dialogueName));
        }
    }
    //void CancelDialogue()

    public void SelectResponse(int index)
    {

    }

}
