using UnityEngine;
using System.IO;
using System.Linq;

public class LevelLoader : MonoBehaviour
{
    //Path
    public string LevelPath;

    //Winner Platform
    public Transform WinnerPlatform;

    //Edit System
    public EditSystem editSystem;

    //Hint Manager
    public HintManager hintManager;

    //All of da objects
    public GameObject blocker, bouncer, megaBouncer, leftBooster, rightBooster;

    public void Awake()
    {
        //Get Stage
        Stage stage = Convert();

        //Set Init Position for Victory Platform First
        WinnerPlatform.position = stage.victoryPlatformPos;

        //Set all of the Blockers
        foreach (LevelEditorItem item in stage.init.blockers)
        {
            GameObject x = Instantiate(blocker, item.pos, item.rot);
            x.transform.localScale = item.scale;
        }

        //Set all of the Bouncers
        foreach (LevelEditorItem item in stage.init.bouncers)
        {
            GameObject pref = GameObject.Instantiate(bouncer, item.pos, item.rot);
            pref.transform.localScale = item.scale;
            EditableObject ed = pref.GetComponent<EditableObject>();
            ed.ID = item.id;
            editSystem.AllEditables = editSystem.AllEditables.Concat(new EditableObject[] { ed }).ToArray();
        }

        //Set all of the Mega Bouncers
        foreach (LevelEditorItem item in stage.init.megaBouncers)
        {
            GameObject pref = GameObject.Instantiate(megaBouncer, item.pos, item.rot);
            pref.transform.localScale = item.scale;
            EditableObject ed = pref.GetComponent<EditableObject>();
            ed.ID = item.id;
            editSystem.AllEditables = editSystem.AllEditables.Concat(new EditableObject[] { ed }).ToArray();
        }

        //Set all of the Left Boosters
        foreach (LevelEditorItem item in stage.init.leftBoosters)
        {
            GameObject pref = GameObject.Instantiate(leftBooster, item.pos, item.rot);
            pref.transform.localScale = item.scale;
            EditableObject ed = pref.GetComponent<EditableObject>();
            ed.ID = item.id;
            editSystem.AllEditables = editSystem.AllEditables.Concat(new EditableObject[] { ed }).ToArray();
        }

        //Set all of the Bouncers
        foreach (LevelEditorItem item in stage.init.rightBoosters)
        {
            GameObject pref = GameObject.Instantiate(rightBooster, item.pos, item.rot);
            pref.transform.localScale = item.scale;
            EditableObject ed = pref.GetComponent<EditableObject>();
            ed.ID = item.id;
            editSystem.AllEditables = editSystem.AllEditables.Concat(new EditableObject[] { ed }).ToArray();
        }

        //Set Keys
        hintManager.HintKeys = stage.keys;
    }

    public Stage Convert()
    {
        string defacto = @"D:\Projects\v2\unity\Some 2D Game idk\Assets\Level Editor Stuff\Levels" + @"\";
        string path = defacto + LevelPath;
        string fileContents = File.ReadAllText(path);
        Stage stage = JsonUtility.FromJson<Stage>(fileContents);
        return stage;
    }
}
