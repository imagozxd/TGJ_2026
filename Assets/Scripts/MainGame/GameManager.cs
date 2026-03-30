using UnityEngine;

public class GameManager : MonoBehaviour {
    void Start() {
        ClienteBarControllerSimple.Instance.StartGame();
        AudioManager.Instance.PlayBGM("BgMusic");
    }

    public void GameOver() {

        BackToMainMenu();
    }

    public void BackToMainMenu()
    {
        // vuelve al menu princopal (scene)
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
