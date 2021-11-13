using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class CubeController : MonoBehaviour
{
    private const string gameobject = "GameObject";
    private const string CurrentLevel = "CurrentLevel";
    private const string CountLvls = "CountLvls";
    [SerializeField]
    private GameObject image;//спрайт внутри куба
    [SerializeField]
    private GameObject cube;//куб целиком
    [SerializeField]
    private bool ThisSelected;
    [SerializeField]
    private ParticleSystem particle;//партикл при верном выборе
    [SerializeField]
    private Bouncer Bouncer;
    //Функция Bounce эффекта для куба
    public void BounceStart()
    {
        StartCoroutine(Bouncer.BounceCube(cube));
    }
    IEnumerator RightAnswer()
    {
        Instantiate(particle).transform.position = image.transform.position;
        yield return new WaitForSeconds(1.2f);
        PlayerPrefs.SetInt(CurrentLevel, PlayerPrefs.GetInt(CurrentLevel) + 1);
        if (PlayerPrefs.GetInt(CurrentLevel) == PlayerPrefs.GetInt(CountLvls))
        {
            PlayerPrefs.SetInt(CurrentLevel, PlayerPrefs.GetInt(CurrentLevel) - 1);
            GameObject.Find(gameobject).GetComponent<Fader>().FadeInRestart();
        }
        else GameObject.Find(gameobject).GetComponent<Spawner>().SpawnCubes(PlayerPrefs.GetInt(CurrentLevel), false);
    }
    IEnumerator WrongAnswer()
    {
        image.transform.DOPunchPosition(new Vector3(0.5f, 0f, 0f), 1f);
        yield return new WaitForSeconds(0f);
    }
    //Данный куб выбран в качестве верного ответа
    public void SelectThis()
    {
        ThisSelected = true;
    }
    //Обработка клика по кубу
    private void OnMouseDown()
    {
        if (ThisSelected)
        {
            //Bounce эффект спрайта внутри куба
            StartCoroutine(Bouncer.BounceSprite(image));
            StartCoroutine(RightAnswer());
        }
        else
        {
            StartCoroutine(WrongAnswer());
        }
    }
    //Установка спрайта для куба
    public void SetSprite(Sprite sprite)
    {
        image.GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
