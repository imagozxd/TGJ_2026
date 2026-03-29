using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] private Button backButton;

    void OnEnable() {
        backButton.onClick.AddListener(OnBackButtonClicked);
    }

    void OnDisable() {
        backButton.onClick.RemoveListener(OnBackButtonClicked);
    }

    private void OnBackButtonClicked() {
        // Volver al menú principal
        SceneManager.LoadScene("MainMenu");
    }
}
