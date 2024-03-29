﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RPG.UI
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] GameObject pauseMenuUI;
        [SerializeField] bool GameIsPaused = false;
        [SerializeField] int retryLevel;
        Button[] buttons;

        void Start()
        {
            buttons = GameObject.FindObjectsOfType<Button>();
            StartCoroutine(buttonsCo());
        }

        public void SetRetryLevel(int level)
        {
            retryLevel = level;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused) { MenuContinue(); }
                else { MenuPause(); }
            }
        }
        public void LoadScene(int sceneNumber)
        {
            SceneManager.LoadScene(sceneNumber);
        }
        public void LoadStarMenu()
        {
            SceneManager.LoadScene("Menu");
        }
        public void QuitGame()
        {
            Application.Quit();
        }
        public void MenuContinue()
        {
            Debug.Log("MIERDA PUTA CARRAGO");
            pauseMenuUI.SetActive(false); ;
            Time.timeScale = 1;
            GameIsPaused = false;
        }
        public void MenuPause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }

        public void RetryButton()
        {
            SceneManager.LoadScene(retryLevel);
        }

        private IEnumerator buttonsCo()
        {
            for(int i = 0; i < buttons.Length; i++)
            {
                buttons[i].gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(4f);
            for(int i = 0; i < buttons.Length; i++)
            {
                buttons[i].gameObject.SetActive(true);
            }
        }
    }
}
