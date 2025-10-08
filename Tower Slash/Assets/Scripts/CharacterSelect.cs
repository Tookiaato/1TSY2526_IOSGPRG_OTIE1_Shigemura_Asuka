using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    public int chosenCharacter;
    public bool isChoosing;
    public bool chosen;

    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject chooseButton;

    private void Start()
    {
        isChoosing = true;
    }
    public void Play()
    {
        playButton.SetActive(false);
        chooseButton.SetActive(true);
        isChoosing = true;
    }
    public void ChooseDefault()
    {
        chosenCharacter = 0;
        isChoosing = false;
        chosen = true;
        chooseButton.SetActive(false);
    }

    public void ChooseSpeed()
    {
        chosenCharacter = 1;
        isChoosing = false;
        chosen = true;
        chooseButton.SetActive(false);
    }

    public void ChooseTank()
    {
        chosenCharacter= 2;
        isChoosing = false;
        chosen = true;
        chooseButton.SetActive(false);
    }
}
