using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(fileName = "DialogueDatabase", menuName = "Scriptable Objects/DialogueDatabase")]
public class DialogueDatabase : ScriptableObject
{
    public List<DialogueLine> database = new List<DialogueLine>();
}
