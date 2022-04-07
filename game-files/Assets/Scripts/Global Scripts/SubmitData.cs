using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

// This script sends game data to the server
public class SubmitData : MonoBehaviour
{

    public void UploadAfterLevel()
    {
        StartCoroutine(Upload());
    }

    public IEnumerator Upload()
    {
        int gameNum = 0;

        WWWForm numForm = new WWWForm();
        numForm.AddField("PlayerID", Global.playerID);
        numForm.AddField("GroupID", Global.groupID);

        string getGameNumURL = "https://stat2games.sites.grinnell.edu/php/Statspital/getstatspitalgamenum.php";

        using (var w = UnityWebRequest.Post(getGameNumURL, numForm))
        {
            Debug.Log("starting fetching game num");
            yield return w.SendWebRequest();
            Debug.Log("fetched");
            if (w.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Fetching game number failed.  Error message: ");
            }
            else
            {
                int num = int.Parse(w.downloadHandler.text);
                if (num > 1)
                {
                    gameNum = num;
                }
                else
                {
                    gameNum += num;
                }
                Debug.Log("gamenum: " + gameNum);
            }
        }

        for (int i = 0; i < Global.patient.Length; i++)
        {
            // Create a Web Form
            WWWForm form = new WWWForm();
            form.AddField("GameNum", gameNum);
            form.AddField("PlayerID", Global.playerID);
            form.AddField("GroupID", Global.groupID);
            form.AddField("Level", Global.level);
            form.AddField("Patient", Global.patient[i]);
            form.AddField("YMed", Global.yMed[i]);
            form.AddField("BMed", Global.bMed[i]);
            form.AddField("ComMed", Global.comMed[i]);
            form.AddField("Health", Global.health[i]);
            form.AddField("Time", Global.time.ToString("F2"));
            form.AddField("Win", Bool_To_Int(Global.win));
            form.AddField("NumHealthy", Global.numHealthy);
            form.AddField("NumPatients", Global.numPatients);

            string databaseURL = "https://www.stat2games.sites.grinnell.edu/php/Statspital/sendstatspitalgamedata.php";

            // Upload to a cgi script
            using (var w = UnityWebRequest.Post(databaseURL, form))
            {
                yield return w.SendWebRequest();
                if (w.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(w.error);
                }
                else
                {
                    Debug.Log("Finished Uploading Game Data");
                }
            }
        }
    }

    int Bool_To_Int(bool win)
    {
        if (win)
        {
            return 1;
        } else
        {
            return 0;
        }
    }
}


    