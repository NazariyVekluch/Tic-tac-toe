using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class StartSceneUIController : UIController
{

    public InputField NameInputField;
    public Dropdown XODropdown;

    private void Start()
    {
        FillPlayerInfo();
    }

    // fill player info
    private void FillPlayerInfo()
    {
        // fill player name
        NameInputField.text = GameCenter.Name;

        // fill symbol in dropdown list
        for (int i = 0; i < XODropdown.options.Count; i++)
        {
            if (XODropdown.options[i].text == GameCenter.PlayerSymbol.ToString())
            {
                XODropdown.value = i;
                break;
            }
        }
    }

    // start new xogame
    public void StartPress()
    {
        // set Player name
        if (NameInputField.text == string.Empty)
            GameCenter.Name = "Player1";
        else
            GameCenter.Name = NameInputField.text;

        // set players symbol
        int symbolIndex = XODropdown.value;
        GameCenter.PlayerSymbol = XODropdown.options[symbolIndex].text[0];

        symbolIndex = symbolIndex == 0 ? 1 : 0;
        GameCenter.OpponentSymbol = XODropdown.options[symbolIndex].text[0];

        //open next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
