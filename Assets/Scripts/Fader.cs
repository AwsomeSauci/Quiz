using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    [SerializeField]
    Image faderRestart;
    [SerializeField]
    Image faderScene;
    [SerializeField]
    GameObject button;
    public void FadeInScene()
    {
        faderScene.DOFade(1f, 0.5f);
    }
    public void FadeOutScene()
    {
        faderScene.DOFade(0f, 0.2f);
    }
    public void FadeInRestart()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Cube");
        for(int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].GetComponent<Collider2D>().enabled = false;
        }
        faderRestart.DOFade(0.8f,1f);
        button.SetActive(true);
        button.GetComponent<Image>().DOFade(1f,1f);
    }
    public void FadeOutRestart()
    {
        faderRestart.DOFade(0f, 1f);
        button.SetActive(false);
        button.GetComponent<Image>().DOFade(0f, 1f);
    }
}
