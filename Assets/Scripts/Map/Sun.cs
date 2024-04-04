using UnityEngine;

public class Sun : MonoBehaviour
{
    [SerializeField] private DayCycleManager dayCycleManager;
    [SerializeField] private Light sunLight;

    private void Update()
    {
        RotateSun();
    }

    private void RotateSun()
    {
        // Calculate rotation angle based on time of the day
        float angle = CalculateSunRotationAngle();

        // Apply rotation to the sun
        sunLight.transform.rotation = Quaternion.Euler(angle, -90f, 0);
    }

    private float CalculateSunRotationAngle()
    {
        // Calculate the time passed since the start of the day
        float timePassedSinceStart = dayCycleManager.IngameTime - dayCycleManager.DayStart;

        // Wrap the time passed between 0 and 24 hours
        timePassedSinceStart = (timePassedSinceStart + 24) % 24;

        // Convert the time passed to rotation angle (0 to 360 degrees)
        float angle = Mathf.Lerp(0f, 360f, timePassedSinceStart / 24f);

        return angle;
    }
}
