using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI instance; 

    public GameObject endScreen;

    public Text playerWonText;

    public bool gameOver = false, isTutorial;

    public float p1Score = 0, p2Score = 0;

    [SerializeField] GameObject healthFillP1, healthFillP2, p1Powerup, p2Powerup, settingsMenu, mainMenu;

    public bool p1Eligible = false, p2Eligible = false, used1 = false, used2 = false; 

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject); 
        }
        instance = this; 
    }

    public void GameOver(bool p1) {
        gameOver = true; 
        endScreen.SetActive(true);
        endScreen.GetComponent<AudioSource>().Play(); 
        if (p1) playerWonText.text = "Player 1 Won!";
        else playerWonText.text = "Player 2 Won!"; 
    }

    public void Restart(int index) {
        SceneManager.LoadScene(index); 
    }

    public void Quit() {
        Application.Quit(); 
    }

    IEnumerator ShowForTime(GameObject go, float time) {
        go.SetActive(true);
        yield return new WaitForSeconds(time);
        go.SetActive(false); 
    }

    private void Update() {
        if (isTutorial) {
            if (Input.GetKey(KeyCode.Escape)) {
                Restart(0); 
            }
            return; 
        }
        //healthFillP1.GetComponent<RectTransform>().sizeDelta = new Vector2(p1Score, healthFillP1.GetComponent<RectTransform>().sizeDelta.y);
        healthFillP1.transform.localScale = new Vector3(Mathf.Clamp(p1Score,0f,1f), 1f, 1f); 
        //healthFillP2.GetComponent<RectTransform>().sizeDelta = new Vector2(p2Score, healthFillP2.GetComponent<RectTransform>().sizeDelta.y); 
        healthFillP2.transform.localScale = new Vector3(Mathf.Clamp(p2Score, 0f, 1f), 1f, 1f); 

        if (p1Score >= 1 && !used1) {
            StartCoroutine(ShowForTime(p1Powerup, 4.5f));
            p1Eligible = true;
            used1 = true; 
        }
        if (p2Score >= 1 && !used2) {
            StartCoroutine(ShowForTime(p2Powerup, 4.5f));
            p2Eligible = true;
            used2 = true; 
        }
    }

    public void VolumeSlider(float volume) {
        AudioListener.volume = volume; 
    }

    public void OpenSettings() {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false); 
    }

    public void CloseSettings() {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true); 
    }
}
