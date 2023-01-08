using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuScript : MonoBehaviour
{
    public GameObject Start_Screen;
    public GameObject Guide_Screen;
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Enter_Name(TMP_InputField EN)
    {
        MainManager.Instance.namePlayer = EN.text;
    }    

    public void Start_game()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(1);
    }

    public void Guide_game()
    {
        Guide_Screen.SetActive(true);
        Start_Screen.SetActive(false);
    }

    public void Exit()
    {
        Start_Screen.SetActive(true);
        Guide_Screen.SetActive(false);
    }
}
