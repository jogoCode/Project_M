using UnityEngine;

public class Sun : MonoBehaviour
{
    private const float MAX_ROTATION = 360f;

    [SerializeField] private DayCycleManager m_dayCycleManager;
    [SerializeField] private Transform m_sunLight;

    private Vector3 _sunStartRotation;
    private float _currentRotation;

    private void Start()
    {
        _sunStartRotation = m_sunLight.localEulerAngles;
    }

    private void Update()
    {
        _currentRotation = Mathf.Lerp(0, MAX_ROTATION, m_dayCycleManager.IngameTime / 24f);
        m_sunLight.localRotation = Quaternion.Euler(_sunStartRotation + Vector3.right * _currentRotation);
    }
}
