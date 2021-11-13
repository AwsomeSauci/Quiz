using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Spawner : MonoBehaviour
{
    private const float CardSize = 2.22f;
    private const string FindText = "Find ";
    private const string CountLvls = "CountLvls";
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
        bool odd = NumItems % 2 != 0;
        float shift = CardSize * (NumItems / 2);
        return odd ? shift : shift - CardSize / 2;
    }
    //Генерация массива координат ячеек
    Vector2[,] GenerateMatrix()
    {
        int strings = Levels[SelectedLevel].strings;
        int columns = Levels[SelectedLevel].columns;
        Vector2[,] points = new Vector2[strings, columns];
        for (int i = 0; i < strings; i++)
            for (int j = 0; j < columns; j++)
            {
                points[i, j].x = j * CardSize - CalcShift(columns);
                points[i, j].y = i * CardSize - CalcShift(strings);
            }
        return points;
    }
    //Выбор куба, который необходимо найти
    public void SelectCube(bool effects)
    {
        //поиск максимально возможного количества ячеек
        int CountCubes = 0;
        for (int i = 0; i < Levels.Length; i++) CountCubes += Levels[i].columns;
        //Генерация случайного выбора куба для поиска, при отсутствии не использованных ранее
        //значений, генерируется случайное
        int FindedCount = 0;
        string foundedID;
        int NumCube = 0;
        do
        {
            NumCube = Random.Range(0, cards.Count);
            foundedID = FindedValues.Find(str => str == Levels[SelectedLevel].CardData[NumCube].Identifer);
            FindedCount++;
        }
        while (foundedID != null || FindedCount > CountCubes);
        //назначение идентификатора
        FindedValues.Add(Levels[SelectedLevel].CardData[NumCube].Identifer);
        cards[NumCube].GetComponent<CubeController>().SelectThis();
        //изменение текста
        Text.text = FindText + Levels[SelectedLevel].CardData[NumCube].Identifer;
        Fader.FadeInText(Text, effects);
    }
    //создание кубов из префаба
    void CreateCubes(Vector2[,] points, bool effects)
    {
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
    }
    public void SpawnCubes(int level, bool effects)
    {
        SelectedLevel = level;
        //Сохранение количества уровней
        PlayerPrefs.SetInt(CountLvls, Levels.Length);
        ClearField();
        Vector2[,] points = GenerateMatrix();
        CreateCubes(points, effects);
        SelectCube(effects);
    }
}