using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class TimeOfDay : MonoBehaviour
{
    [SerializeField] FloatVariable timeVariable;
    [SerializeField] float timeShiftSpeed = 10;
    [SerializeField] float timeShiftStrength = 0.1f;

    float startingTimeOfDay;

    private void OnEnable()
    {
        startingTimeOfDay = transform.eulerAngles.x;
        timeVariable.OnValueChanged += UpdateTimeOfDay;
    }

    public void UpdateTimeOfDay(float value)
    {
        StopAllCoroutines();
        StartCoroutine(ShiftTimeOfDay(value));
    }

    IEnumerator ShiftTimeOfDay(float value)
    {
        for (int i = 0; i < 100; i++)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(value + startingTimeOfDay, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z), timeShiftStrength);

            yield return new WaitForSeconds(1 / timeShiftSpeed);
        }
    }
}
