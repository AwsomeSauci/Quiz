using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject GameObject;
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject.GetComponent<Fader>().FadeInScene();
        GameObject.GetComponent<Fader>().FadeOutRestart();
        StartCoroutine(RestartGame());
    }
    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(1f);
        GameObject.GetComponent<LevelLoader>().RestartGame();
        GameObject.GetComponent<Fader>().FadeOutScene();
    }
}
