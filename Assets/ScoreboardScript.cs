using TMPro;
using UnityEngine;

public class ScoreboardScript : MonoBehaviour
{   

    public void RefreshScoreboard()
    {
        PlayerData[] data = SaveSystem.LoadPlayer();

        for(int i = 0; i<10; i++)
        {
            TMP_Text nameText;
            TMP_Text pointsText;
            nameText = transform.GetChild(i).GetChild(0).GetComponent<TMP_Text>();
            pointsText = transform.GetChild(i).GetChild(1).GetComponent<TMP_Text>();

            switch (i)
            {
                default:
                    nameText.text = i+1 + "th  ";
                    break;
                case 0:
                    nameText.text = "1st  ";
                    break;
                case 1:
                    nameText.text = "2nd  ";
                    break;
                case 2:
                    nameText.text = "3rd  ";
                    break;
            }

            if (data[i] == null)
            {
                nameText.text += "";
                pointsText.text = "0";
            }
            else
            {
                nameText.text += data[i].name;
                pointsText.text = data[i].score.ToString();
            }
        }
    }

}
