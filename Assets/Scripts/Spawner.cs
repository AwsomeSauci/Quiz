using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Spawner : MonoBehaviour
{
    const float CardSize = 2.22f;
    [SerializeField]
    private CardBoundleData Level1;
    [SerializeField]
    private CardBoundleData Level2;
    [SerializeField]
    private CardBoundleData Level3;
    [SerializeField]
    private Text Text;

    [SerializeField]
    private GameObject card;
    private CardBoundleData Level;
    private List<GameObject> cards = new List<GameObject>();
    private List<string> FindedValues = new List<string>();

    Vector2[,] points;
    void ClearField()
    {
        if (cards != null)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                Destroy(cards[i]);
            }
            cards.Clear();
        }
    }
    void LevelSelector(int lvl)
    {
        switch (lvl)
        {
            case 1:
                Level = Level1;
                break;
            case 2:
                Level = Level2;
                break;
            case 3:
                Level = Level3;
                break;
        }
    }
    void GenerateMatrix()
    {
        points = new Vector2[Level.strings, Level.columns];
        for (int i = 0; i < Level.strings; i++)
        {
            for (int j = 0; j < Level.columns; j++)
            {
                points[i, j].x = j * CardSize - CardSize * (Level.columns / 2);
                points[i, j].y = i * CardSize - CardSize * (Level.strings / 2);
            }
        }
    }
   public void SelectCube(bool effects)
    {
        int NumCube;
        int CountCubes = Level1.CardData.Length + Level2.CardData.Length + Level3.CardData.Length;
        int FindedCount = 0;
        do
        {
            NumCube = Random.Range(0, cards.Count);
            if (FindedCount > CountCubes) break;
            FindedCount++;
        }
        while (FindedValues.Find(str => str == Level.CardData[NumCube].Identifer) != null);
        cards[NumCube].GetComponent<CubeController>().SelectThis();
        Text.text = "Find " + Level.CardData[NumCube].Identifer;
        if(effects==true) Text.DOFade(1f, 0.5f);
        else Text.DOFade(1f, 0f);
        FindedValues.Add(Level.CardData[NumCube].Identifer);
    }
    public void SpawnCubes(int level, bool effects)
    {
        ClearField();
        LevelSelector(level);
        GenerateMatrix();
        int cardnum = 0;
        for (int i = 0; i < Level.strings; i++)
        {
            for (int j = 0; j < Level.columns; j++)
            {
                cards.Add(Instantiate(card));
                if(effects == true)
                cards[cardnum].GetComponent<CubeController>().BounceStart();
                cards[cardnum].transform.position = points[i, j];
                cards[cardnum].GetComponent<CubeController>().SetSprite(Level.CardData[cardnum].Sprite);
                cardnum++;
            }
        }
        SelectCube(effects);
    }
}
