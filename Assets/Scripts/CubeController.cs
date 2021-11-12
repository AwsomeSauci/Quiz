using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CubeController : MonoBehaviour
{
    [SerializeField]
    private GameObject image;
    [SerializeField]
    private GameObject cube;
    [SerializeField]
    private bool ThisSelected;
    [SerializeField]
    private ParticleSystem particle;
    [SerializeField]
    private Bouncer Bouncer;

    public void BounceStart()
    {
        StartCoroutine(Bouncer.BounceCube(cube));
    }
    IEnumerator RightAnswer()
    {
        Instantiate(particle).transform.position = image.transform.position;
        yield return new WaitForSeconds(1.2f);
        PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel") + 1);
        if (PlayerPrefs.GetInt("CurrentLevel") == 4)
        {
            GameObject.Find("GameObject").GetComponent<Fader>().FadeInRestart();
        }
        else GameObject.Find("GameObject").GetComponent<Spawner>().SpawnCubes(PlayerPrefs.GetInt("CurrentLevel"), false);
    }
    IEnumerator WrongAnswer()
    {
        image.transform.DOPunchPosition(new Vector3(0.5f, 0f, 0f), 1f);
        yield return new WaitForSeconds(0f);
    }

    public void SelectThis()
    {
        ThisSelected = true;
    }
    private void OnMouseDown()
    {
        if (ThisSelected == true)
        {
            StartCoroutine(Bouncer.BounceSprite(image));
            StartCoroutine(RightAnswer());
        }
        else
        {
            StartCoroutine(WrongAnswer());
        }
    }
    public void SetSprite(Sprite sprite)
    {
        image.GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
