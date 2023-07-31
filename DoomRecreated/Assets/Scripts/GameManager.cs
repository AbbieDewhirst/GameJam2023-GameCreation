using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerMovement playerController;
    public int Health = 100;
    public Image damagePanel;
    float damagePanelFloat;
    public bool GameStarted = false;
    public float currentTime;
    public bool GameEnded;
    public string playerName;
    public bool RunStarted;
    public Material speedLineMat;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        instance = this;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1;
    }

    public void setName(string name)
    {
        playerName = name;
        UI_Manager.instance.agent3DText.text = "Agent: " + playerName;
    }

    public void Damage(int damage)
    {
        Health -= damage;
        damagePanelFloat = 0.2f;
        CameraShake.instance.shakeDuration = 0.5f;
        
    }

    public void LostGame()
    {
        
        UI_Manager.instance.canPause = false;
        UI_Manager.instance.gameEndedCanvas.SetActive(true);
        UI_Manager.instance.gamePausePanel.SetActive(false);
        UI_Manager.instance.isPaused = false;

        TimeSpan ts = TimeSpan.FromSeconds(GameManager.instance.currentTime);
        String result = ts.ToString("mm\\:ss\\:ff");
        UI_Manager.instance.timeLostTimer.text = result;
        if(RunStarted)
        {
            PlayfabManager.instance.sendLeaderboardLosses((int) (currentTime * 100));
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameStarted = false;
    }

    public void winGame()
    {
        UI_Manager.instance.canPause = false;
        UI_Manager.instance.gameWonCanvas.SetActive(true);
        UI_Manager.instance.gamePausePanel.SetActive(false);
        UI_Manager.instance.isPaused = false;

        TimeSpan ts = TimeSpan.FromSeconds(GameManager.instance.currentTime);
        String result = ts.ToString("mm\\:ss\\:ff");
        UI_Manager.instance.timeWonTimer.text = result;

        PlayfabManager.instance.sendLeaderboardWin((int) (currentTime * 100));

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameStarted = false;
    }

    public void startGame()
    {
        GameStarted = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void StartRun()
    {
        RunStarted = true;
    }
    public void RestartLvl()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    float currentLineMatAlpha = 1;
    float currentWindSoudVol = 0;
    void Update()
    {
        if(Health <= 0 && GameEnded == false)
        {
            Debug.Log("PLAYER DEAD");
            LostGame();

            GameEnded = true;
        }
        if(playerController.rb.velocity.magnitude > 30)
        {
            currentLineMatAlpha -= Time.deltaTime * 0.2f;
            currentLineMatAlpha = Mathf.Clamp(currentLineMatAlpha, 0.8f, 1);

            currentWindSoudVol += Time.deltaTime * 0.1f;
            currentWindSoudVol = Mathf.Clamp(currentWindSoudVol, 0f, 0.2f);
            SoundManager.instance.windSource.volume = currentWindSoudVol;

            speedLineMat.SetFloat("_SpeedLinesRemap", currentLineMatAlpha);
        }
        else
        {
            currentLineMatAlpha += Time.deltaTime * 0.2f;
            currentLineMatAlpha = Mathf.Clamp(currentLineMatAlpha, 0.8f, 1);

            currentWindSoudVol -= Time.deltaTime * 0.3f;
            currentWindSoudVol = Mathf.Clamp(currentWindSoudVol, 0f, 0.2f);
            SoundManager.instance.windSource.volume = currentWindSoudVol;

            speedLineMat.SetFloat("_SpeedLinesRemap", currentLineMatAlpha);
        }

        if(GameStarted && GameEnded ==false && RunStarted)
        {
            currentTime += Time.deltaTime;
        }

        damagePanelFloat -= Time.deltaTime;
        damagePanelFloat = Mathf.Clamp01(damagePanelFloat);
        damagePanel.color = new Color(damagePanel.color.r, damagePanel.color.g, damagePanel.color.b, damagePanelFloat);

        playerController.sensMultiplier = Mathf.Lerp(0.1f, 1f, UI_Manager.instance.sensitivitySlider.value);
    }
}
