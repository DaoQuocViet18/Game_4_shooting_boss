using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public GameObject End_Screen;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI textName;
    private int score;

    // Start is called before the first frame update
    void OnEnable()
    {
        textName.text = MainManager.Instance.namePlayer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Again_game()
    {
        End_Screen.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }    

    public void Exit()
    {
        if (score > MainManager.Instance.bestScore)
        {
            MainManager.Instance.saveBestScore(MainManager.Instance.namePlayer, score);
        }

        SceneManager.LoadScene(0);
    }    

    public void Score(int point)
    {
        score += point;
        scoreText.text = "" + score;
    }    


}
