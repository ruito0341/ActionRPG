using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    public TextMeshProUGUI[] menuItems;
    private int currentIndex = 0;

    void Start()
    {
        UpdateMenu();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentIndex = (currentIndex + 1) % menuItems.Length;
            UpdateMenu();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentIndex = (currentIndex - 1 + menuItems.Length) % menuItems.Length;
            UpdateMenu();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SelectItem();
        }
    }

    void UpdateMenu()
    {
        for (int i = 0; i < menuItems.Length; i++)
        {
            if (i == currentIndex)
            {
                menuItems[i].color = Color.black;
                menuItems[i].text = "> " + menuItems[i].text.Replace("> ", "");
            }
            else
            {
                menuItems[i].color = Color.white;
                menuItems[i].text = menuItems[i].text.Replace("> ", "");

            }
        }
    }
    void SelectItem()
    {
        string item = menuItems[currentIndex].text.Replace("> ", "");
        Debug.Log("Selected: " + item);

        switch (item)
        {
            case "Start Game":
                SceneManager.LoadScene("GameScene");
                break;
            case "Options":
                SceneManager.LoadScene("OptionsScene");
                break;
            case "Exit":
                Application.Quit();
                break;
        }
    }




    }

