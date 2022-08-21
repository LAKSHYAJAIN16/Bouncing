using TMPro;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class HintManager : MonoBehaviour
{
    public EditSystem editSystem;
    public GameManager gameManager;
    public TextMeshProUGUI HintTXT;
    public GameObject HintPlusUI;
    public GameObject HintMenuUI;
    public int HintsAvailable;
    public HintKey[] HintKeys;
    public string[] HintsDone;

    private void Start()
    {
        //TODO : Load Hints from PlayerPrefs;
        HintTXT.text = HintsAvailable.ToString();
    }

    public void RequestHint()
    {
        //If we're playing, do nothing
        if (gameManager.playing == true) return;

        //If we don't have hints available, get the frick out of here
        if (HintsAvailable <= 0)
        {
            HintMenuUI.SetActive(true);
            return;
        }

        //Deduct Hint Available
        HintsAvailable -= 1;
        HintTXT.text = HintsAvailable.ToString();

        //If the Hints available is zero, make it the plus UI
        if(HintsAvailable == 0) {
            HintPlusUI.SetActive(true);
        }

        //Loop through all of the editables
        foreach (EditableObject editableObject in editSystem.AllEditables)
        {
            //Get the ID
            string objID = editableObject.ID;

            //If we haven't edited it yet
            if (!HintsDone.Contains(objID))
            {
                //Get the actual pos
                foreach (HintKey key in HintKeys)
                {
                    if (key.ID == objID)
                    {
                        Vector3 targetPos = key.Position;
                        editableObject.transform.position = targetPos;
                        List<string> strings = HintsDone.ToList();
                        strings.Add(objID);
                        HintsDone = strings.ToArray();
                        break;
                    }
                }

                break;
            }
        }
    }

    public void CloseHintPopup()
    {
        HintMenuUI.SetActive(false);
    }
}

[System.Serializable]
public struct HintKey
{
    public string ID;
    public Vector3 Position;
}
