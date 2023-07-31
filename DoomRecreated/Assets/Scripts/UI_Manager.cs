using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;

    public GameObject gameEndedCanvas, gameWonCanvas;
    public GameObject gamePausePanel, grenadePanel;

    public bool isPaused;
    public bool canPause;
    public TMP_Text timeLostTimer, timeWonTimer;
    public TMP_Text bulletCountTxt;
    public TMP_Text timerTxtUI;
    [Header("FIRST TIME PANEL")]
    public GameObject newlyCreatedPanel;
    public TMP_InputField newlyNameInput;

    [Header("Leaderboard")]
    public GameObject leaderBoardPrefab;
    public Transform leaderBoardParent, leadeBoardLossesParent;
    [Header("3D TEXT")]
    public TMP_Text agent3DText;
    public TMP_Text _3dTimer1;
    [Header("Settings")]
    public Slider sensitivitySlider;

    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        sensitivitySlider.value = PlayerPrefs.GetFloat("sense", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && GameManager.instance.GameStarted)
        {
            switchPause();
        }
        if(_3dTimer1.gameObject.activeSelf)
        {
            TimeSpan ts1 = TimeSpan.FromSeconds(GameManager.instance.currentTime);
            String result1 = ts1.ToString("mm\\:ss\\:ff");
            _3dTimer1.text = result1;
        }
        TimeSpan ts = TimeSpan.FromSeconds(GameManager.instance.currentTime);
        String result = ts.ToString("mm\\:ss\\:ff");
        timerTxtUI.text = result;
    }

    public void changeSlider(float value)
    {
        PlayerPrefs.SetFloat("sense", value);
    }

    public void StartGameClick()
    {        
        GameManager.instance.startGame();
    }

    public void startGameEndedCanvas()
    {
        if(gameEndedCanvas.activeSelf) return;
        gameEndedCanvas.SetActive(true);
    }
    public void switchPause()
    {
        if(!canPause) return;
        isPaused = !isPaused;
        gamePausePanel.SetActive(isPaused);
        if(isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
    }
}
