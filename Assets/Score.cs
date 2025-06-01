using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score instance;

    public int score = 0;
    public TMP_Text text;
    private PlayerData[] data = new PlayerData[10];

    public static int enemyPoints = 0;
    public static int bossPoints = 0;
    public static int repairPoints = 0;
    public static int timePoints = 0;

    public TMP_Text enemyText, bossText, repairText, timeText, totalText;

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Debug.LogWarning("More than 1 Score detected!");
            gameObject.SetActive(false);
            return;
        }
    }


    public void AddEnemyPoints(int _score)
    {
        score += _score;
        enemyPoints += _score;
        text.text = score.ToString();
        enemyText.text = enemyPoints.ToString();
        totalText.text = text.text;
    }

    public void AddBossPoints(int _score)
    {
        score += _score;
        bossPoints += _score;
        text.text = score.ToString();
        bossText.text = bossPoints.ToString();
        totalText.text = text.text;
    }

    public void AddRepairPoints(int _score)
    {
        score += _score;
        repairPoints += _score;
        text.text = score.ToString();
        repairText.text = repairPoints.ToString();
        totalText.text = text.text;
    }


    public void AddTimePoints(int _score)
    {
        score += _score;
        timePoints += _score;
        text.text = score.ToString();
        timeText.text = timePoints.ToString();
        totalText.text = text.text;
    }

    public void ResetScore()
    {
        PlayerData[] pd = new PlayerData[10];
        SaveSystem.SavePlayer(pd);
        Debug.Log("Score Reset!");
    }

    public int GetScore()
    {
        return score;
    }



    public void SaveScore(string _name)
    {
        PlayerData newPlayer = new PlayerData(_name, score);
        data = SaveSystem.LoadPlayer();
        if (data == null)
        {
            data = new PlayerData[10];
            data[0] = newPlayer;
            SaveSystem.SavePlayer(data);
            return;
        }


        int i = 0;
        while(i < 10)
        {
            //1. wenn leer ist, 2. wenn newPlayer > i ist

            if(data[i] == null)
            {
                data[i] = newPlayer;
                break;
            }
            
            if(newPlayer.score > data[i].score)
            {
                int k = 0;
                while(k  < data.Length && data[k] != null)
                {
                    k++;
                }


                for(k -= 1; k>i-1; k--)
                {
                    data[k + 1] = data[k];
                }

                data[i] = newPlayer;
                break;
            }

            i++;
        }

        Debug.LogError("---------------");
        foreach(PlayerData pd in data)
        {
            if (pd == null) break;
            Debug.Log(pd.name + ": " + pd.score);
        }

        SaveSystem.SavePlayer(data);
    }

}
