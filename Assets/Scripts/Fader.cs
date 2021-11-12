using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    [SerializeField]
    Image faderRestart;//для затемнения при отображении кнопки рестарт
    [SerializeField]
    Image faderScene;//для перезапуска игры
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
        StartCoroutine(ToggleBtn(0f));
        button.GetComponent<Image>().DOFade(1f,1f);
    }
    public void FadeInText(Text text, bool effects)
    {
        if (effects) text.DOFade(1f, 0.5f);
        else text.DOFade(1f, 0f);
    }
    public void FadeOutRestart()
    {
        faderRestart.DOFade(0f, 1f);
        button.GetComponent<Image>().DOFade(0f, 0f);
        StartCoroutine(ToggleBtn(1.2f));
    }
    IEnumerator ToggleBtn(float time)
    {
        yield return new WaitForSeconds(time);
        if (button.activeSelf) button.SetActive(false);
        else button.SetActive(true);
    }
}
