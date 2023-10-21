using UnityEngine;

[CreateAssetMenu(fileName = "ShakeSettings", menuName = "ScriptableObjects/ShakeSettings")]
public class ShakeSettings : ScriptableObject
{
    public float Amplitude;
    public float Frequency;
    public float Duration;
    public int Priority;
    public float InDuration;
}
