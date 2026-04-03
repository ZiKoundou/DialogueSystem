using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Voice", menuName = "Scriptable Objects/Voice")]
public class Voice : ScriptableObject
{
    public List<AudioClip> clips = new List<AudioClip>();
    public int frequency;
    [Range(-3f, 3f)]
    public float minPitch = 1f;
    [Range(-3f, 3f)]
    public float maxPitch = 1f;
}
