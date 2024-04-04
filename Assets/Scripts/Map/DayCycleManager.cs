using UnityEngine;

public class DayCycleManager : MonoBehaviour
{
    [Tooltip("The length of a day in real life seconds.")]
    [SerializeField, Min(0f)] private float m_dayLengthRealtime = 4;

    [Tooltip("The time of the day where the game start in ingame hours")]
    [SerializeField, Range(0, 23)] private int m_dayStart = 6;

    /// <summary>
    /// The time at which the day starts (in hours).
    /// </summary>
    public int DayStart => m_dayStart;

    /// <summary>
    /// How much time has passed ingame (in hours).
    /// </summary>
    public float IngameTime { get; private set; }

    /// <summary>
    /// How much time as passed in realtime (in seconds).
    /// </summary>
    private float _passedTime;

    private void Start()
    {
        ConvertToRealTime();
    }

    private void Update()
    {
        _passedTime = (_passedTime + Time.deltaTime) % m_dayLengthRealtime;
        ConvertToIngameTime();
    }

    private void ConvertToRealTime()
    {
        float t = Mathf.InverseLerp(0, 24, m_dayStart);
        _passedTime = Mathf.Lerp(0f, m_dayLengthRealtime, t);
    }

    private void ConvertToIngameTime()
    {
        float t = Mathf.InverseLerp(0f, m_dayLengthRealtime, _passedTime);
        IngameTime = Mathf.Lerp(0, 24, t);
    }
}