using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomEvent",menuName = "RandomEvent/NewRandomEvent" )]
public class RandomEventBase : ScriptableObject
{

    public RandomEventType eventType;
    public AreaType area;
    public int Value_Sleep;
    public int Value_Supply;

    [TextArea]
    [SerializeField] private string eventInfo;

    public string EventInfo { get => eventInfo; }

}

public enum RandomEventType
{
    GoodEvent,
    BadEvent,
}