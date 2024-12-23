using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SettingsController : MonoBehaviour
{
    public TMP_InputField RedDuration;    // Поле для ввода длительности красного света
    public TMP_InputField YellowDuration; // Поле для ввода длительности желтого света
    public TMP_InputField GreenDuration;  // Поле для ввода длительности зеленого света

    private void Start()
    {
        // Проверяем, что поля связаны
        if (RedDuration == null || YellowDuration == null || GreenDuration == null)
        {
            Debug.LogError("InputFields не связаны. Проверьте настройки в Inspector.");
            return;
        }

        // Устанавливаем значения по умолчанию
        RedDuration.text = PlayerPrefs.GetFloat("RedDuration", 5f).ToString();
        YellowDuration.text = PlayerPrefs.GetFloat("YellowDuration", 2f).ToString();
        GreenDuration.text = PlayerPrefs.GetFloat("GreenDuration", 5f).ToString();
    }

    public void SaveSettings()
    {
        if (RedDuration == null || YellowDuration == null || GreenDuration == null)
        {
            Debug.LogError("InputFields не связаны. Проверьте настройки в Inspector.");
            return;
        }

        // Сохраняем значения из полей
        float redValue, yellowValue, greenValue;

        if (float.TryParse(RedDuration.text, out redValue) &&
            float.TryParse(YellowDuration.text, out yellowValue) &&
            float.TryParse(GreenDuration.text, out greenValue))
        {

            PlayerPrefs.SetFloat("RedDuration", redValue);
            PlayerPrefs.SetFloat("YellowDuration", yellowValue);
            PlayerPrefs.SetFloat("GreenDuration", greenValue);

            PlayerPrefs.Save(); // Обязательно сохраняем изменения

            Debug.Log("Settings saved!");
            Debug.Log($"Red: {redValue}, Yellow: {yellowValue}, Green: {greenValue}");

            SceneManager.LoadScene(0);
        }
        else
        {
            Debug.LogError("Ошибка ввода. Убедитесь, что введены корректные числа.");
        }
    }
}