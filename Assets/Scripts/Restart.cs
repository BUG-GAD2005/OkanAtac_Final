using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    [SerializeField] private SaveManager saveManager;

    private void RestartGame()
    {
        saveManager.ResetSave();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
