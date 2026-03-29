using UnityEngine;

public class GameManager : MonoBehaviour {
    void Start() {
        ClienteBarControllerSimple.Instance.StartGame();
        AudioManager.Instance.PlayBGM("BgMusic");
    }

    public void GameOver() {
        
    }
}
