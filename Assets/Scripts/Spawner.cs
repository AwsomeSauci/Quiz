using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Spawner : MonoBehaviour
{
    private const float CardSize = 2.22f;
    private const string FindText = "Find ";
    [SerializeField]
    private CardBoundleData[] Levels;
    [SerializeField]
    private Text Text;
    [SerializeField]
    private GameObject card;
    [SerializeField]
    private Fader Fader;
    private int SelectedLevel;
    private List<GameObject> cards = new List<GameObject>();
    private List<string> FindedValues = new List<string>();
    Vector2[,] points;
    //Очистка игрового поля
    void ClearField()
    {
        if (cards != null)
            for (int i = 0; i < cards.Count; i++) Destroy(cards[i]);
        cards.Clear();
    }
    //Вычисление сдвига значений при генерации массива координат ячеек
    float CalcShift(int NumItems)
    {
        if (NumItems % 2 != 0) return CardSize * (NumItems / 2);
        else return CardSize * (NumItems / 2) - CardSize / 2;
    }
    //Генерация массива координат ячеек
    void GenerateMatrix()
    {
        points = new Vector2[Levels[SelectedLevel].strings, Levels[SelectedLevel].columns];
        for (int i = 0; i < Levels[SelectedLevel].strings; i++)
            for (int j = 0; j < Levels[SelectedLevel].columns; j++)
            {
                points[i, j].x = j * CardSize - CalcShift(Levels[SelectedLevel].columns);
                points[i, j].y = i * CardSize - CalcShift(Levels[SelectedLevel].strings);
            }
    }
    //Выбор куба, который необходимо найти
    public void SelectCube(bool effects)
    {
        int NumCube;
        int CountCubes = 0;
        //поиск максимально возможного количества ячеек
        for (int i = 0; i < Levels.Length; i++) CountCubes += Levels[SelectedLevel].columns;
        int FindedCount = 0;
        //Генерация случайного выбора куба для поиска, при отсутствии не использованных ранее
        //значений, генерируется случайное
        do
        {
            NumCube = Random.Range(0, cards.Count);
            if (FindedCount > CountCubes) break;
            FindedCount++;
        }
        while (FindedValues.Find(str => str == Levels[SelectedLevel].CardData[NumCube].Identifer) != null);
        FindedValues.Add(Levels[SelectedLevel].CardData[NumCube].Identifer);
        cards[NumCube].GetComponent<CubeController>().SelectThis();
        Text.text = FindText + Levels[SelectedLevel].CardData[NumCube].Identifer;
        Fader.FadeInText(Text,effects);
    }
    public void SpawnCubes(int level, bool effects)
    {
        //Сохранение количества уровней
        PlayerPrefs.SetInt("CountLvls", Levels.Length);
        ClearField();
        SelectedLevel = level;
        GenerateMatrix();
        int cardnum = 0;
        for (int i = 0; i < Levels[SelectedLevel].strings; i++)
        {
            for (int j = 0; j < Levels[SelectedLevel].columns; j++)
            {
                cards.Add(Instantiate(card));
                if (effects) cards[cardnum].GetComponent<CubeController>().BounceStart();
                cards[cardnum].transform.position = points[i, j];
                cards[cardnum].GetComponent<CubeController>().SetSprite(Levels[SelectedLevel].CardData[cardnum].Sprite);
                cardnum++;
            }
        }
        SelectCube(effects);
    }
}
