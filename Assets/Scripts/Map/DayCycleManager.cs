using UnityEngine;

public class DayCycleManager : MonoBehaviour
{
    [Tooltip("The length of a day in real life seconds.")]
    [SerializeField, Min(0f)] private float m_dayLengthRealtime = 4;

    /// <summary>
    /// How much time has passed ingame (in hours).
    /// </summary>
    public float IngameTime { get; private set; }

    /// <summary>
    /// How much time as passed in realtime (in seconds).
    /// </summary>
    private float _passedTime;

    private void Update()
    {
        _passedTime = (_passedTime + Time.deltaTime) % m_dayLengthRealtime;

        float t = Mathf.InverseLerp(0f, m_dayLengthRealtime, _passedTime);
        IngameTime = Mathf.Lerp(0f, 24, t);
    }
}
