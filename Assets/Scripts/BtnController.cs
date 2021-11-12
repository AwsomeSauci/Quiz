using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnController : MonoBehaviour
{
    [SerializeField]
    private Fader Fader;
    [SerializeField]
    private GameObject GameObject;
    public void ResetBtn()
    {
        Fader.FadeInScene();
        Fader.FadeOutRestart();
        StartCoroutine(RestartGame());

    }
    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(1f);
        GameObject.GetComponent<LevelLoader>().RestartGame();
        Fader.FadeOutScene();
    }
}
