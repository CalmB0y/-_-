using UnityEngine;
using UnityEngine.SceneManagement; // Для работы с загрузкой сцен

public class MenuController : MonoBehaviour
{

    public void LoadScene(int sceneIndex)
    {
        // Проверяем, существует ли сцена с указанным индексом
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogError("Scene index out of range: " + sceneIndex);
        }
    }

    // Метод для выхода из приложения
    public void ExitApp()
    {
        #if UNITY_EDITOR
            // Если мы в редакторе Unity, просто останавливаем игру
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // В сборке выходим из приложения
            Application.Quit();
        #endif
    }
}
