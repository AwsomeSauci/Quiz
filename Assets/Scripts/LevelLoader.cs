using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private Spawner Spawner;
    [SerializeField]
    private GameObject GameObject;
    [SerializeField]
    private Text Text;
    void Start()
    {
        Starter();
    }
    public void RestartGame()
    {
        PlayerPrefs.SetInt("CurrentLevel", 0);
        Starter();
    }
    void Starter()
    {
        Text.DOFade(0f, 0f);
        Spawner = GameObject.GetComponent<Spawner>();
        Spawner.SpawnCubes(PlayerPrefs.GetInt("CurrentLevel"), true);
    }
   
}
