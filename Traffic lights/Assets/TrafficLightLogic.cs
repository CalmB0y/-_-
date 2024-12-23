using System.Collections;
using UnityEngine;

public class TrafficLightController : MonoBehaviour
{
    public GameObject redLight;    // Объект красного света
    public GameObject yellowLight; // Объект желтого света
    public GameObject greenLight;  // Объект зеленого света

    public float redDuration = 5f;    // Длительность красного света
    public float yellowDuration = 2f; // Длительность желтого света
    public float greenDuration = 5f;  // Длительность зеленого света

    private bool isEmergencyGreen = false; // Флаг экстренной активации зеленого
    private bool isStopRed = false;        // Флаг экстренной активации красного

    private void Start()
    {
        // Запускаем цикл работы светофора
        StartCoroutine(TrafficLightCycle());
    }

    private IEnumerator TrafficLightCycle()
    {
        while (true)
        {
            // Красный свет
            SetLightState(true, false, false);
            yield return new WaitForSeconds(redDuration);

            // Желтый свет
            SetLightState(false, true, false);
            yield return new WaitForSeconds(yellowDuration);

            // Зеленый свет
            SetLightState(false, false, true);
            yield return new WaitForSeconds(greenDuration);

            // Сбрасываем флаги после экстренного вмешательства
            isEmergencyGreen = false;
            isStopRed = false;
        }
    }

    public void ActivateEmergencyGreen()
    {
        if (isEmergencyGreen) return; // Игнорируем, если уже активен экстренный режим
        isEmergencyGreen = true;

        StopAllCoroutines(); // Останавливаем текущий цикл работы
        StartCoroutine(EmergencyGreenSequence()); // Запускаем экстренный цикл
    }

    public void StopRed()
    {
        if (isStopRed) return; // Игнорируем, если уже активен экстренный режим
        isStopRed = true;

        StopAllCoroutines(); // Останавливаем текущий цикл работы
        StartCoroutine(EmergencyRedSequence()); // Запускаем экстренный цикл
    }

    private IEnumerator EmergencyGreenSequence()
    {
        // Желтый свет перед зеленым
        SetLightState(false, true, false);
        yield return new WaitForSeconds(yellowDuration);

        // Зеленый свет на 5 секунд
        SetLightState(false, false, true);
        yield return new WaitForSeconds(greenDuration);

        // Возвращаемся к обычному циклу
        isEmergencyGreen = false;
        StartCoroutine(TrafficLightCycle());
    }

    private IEnumerator EmergencyRedSequence()
    {
        // Сразу включаем красный свет
        SetLightState(true, false, false);
        yield return new WaitForSeconds(redDuration);

        // Возвращаемся к обычному циклу
        isStopRed = false;
        StartCoroutine(TrafficLightCycle());
    }

    private void SetLightState(bool isRed, bool isYellow, bool isGreen)
    {
        // Управляем состоянием света
        redLight.SetActive(isRed);
        yellowLight.SetActive(isYellow);
        greenLight.SetActive(isGreen);
    }
}