using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button tutorialButton;
    [SerializeField] private Button introSequenceButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button quitGameButton;


    public void OnEnable() {
        startGameButton.onClick.AddListener(OnStartGameClicked);
        tutorialButton.onClick.AddListener(OnTutorialClicked);
        introSequenceButton.onClick.AddListener(OnIntroSequenceClicked);
        creditsButton.onClick.AddListener(OnCreditsClicked);
        quitGameButton.onClick.AddListener(OnQuitGameClicked);
    } 

    public void OnDisable() {
        startGameButton.onClick.RemoveListener(OnStartGameClicked);
        tutorialButton.onClick.RemoveListener(OnTutorialClicked);
        introSequenceButton.onClick.RemoveListener(OnIntroSequenceClicked);
        creditsButton.onClick.RemoveListener(OnCreditsClicked);
        quitGameButton.onClick.RemoveListener(OnQuitGameClicked);
    }

    private void OnStartGameClicked() {
        SceneManager.LoadScene("MScene");
    }

    private void OnTutorialClicked() {
        SceneManager.LoadScene("Tutorial");
    }

    private void OnIntroSequenceClicked() {
        SceneManager.LoadScene("IntroSequence");
    }

    private void OnCreditsClicked() {
        SceneManager.LoadScene("Credits");
    }

    private void OnQuitGameClicked() {
        // Sale del juego
        Application.Quit();

        // Si estamos en el editor, termina la simulación
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }


}
