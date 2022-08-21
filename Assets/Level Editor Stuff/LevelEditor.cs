using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public class LevelEditor : MonoBehaviour
{
    public EditSystem edit;
    public Level CurrentEditingLevel;
    public Vector3 victoryPlatformPos;
    public Stage CurrentStage;
    public void Insert(GameObject pref)
    {
        GameObject a = Instantiate(pref, Vector2.zero, Quaternion.identity);
        if(a.TryGetComponent<EditableObject>(out EditableObject b))
        {
            b.ID = DateTime.Now.ToString();
            edit.AllEditables.Concat(new EditableObject[] { b });

            Deletable x = a.AddComponent<Deletable>();
            x.editSystem = edit;
            x.isEditable = true;
            x.obj = b;
        }
        else
        {
            EditableObject c = a.AddComponent<EditableObject>();
            c.canDrag = true;
            c.NaturalColor = Color.black;
            c.DragColor = Color.green;
            c.xBoundMax = 15.6f;
            c.xBoundMin = -15.81f;
            c.yBoundMax = 6.7f;
            c.yBoundMin = -6.7f;
            edit.AllEditables.Concat(new EditableObject[] { c });

            c.ID = DateTime.Now.ToString();
            Deletable x = a.AddComponent<Deletable>();
            x.editSystem = edit;
            x.isEditable = false;
        }
    }

    public void Save()
    {
        //Define some variables
        Vector3 winPlatformPos;
        List<LevelEditorItem> blockers = new List<LevelEditorItem>();
        List<LevelEditorItem> leftBoosters = new List<LevelEditorItem>();
        List<LevelEditorItem> rightBoosters = new List<LevelEditorItem>();
        List<LevelEditorItem> bouncers = new List<LevelEditorItem>();
        List<LevelEditorItem> megaBouncers = new List<LevelEditorItem>();

        //Get All of the Deletables in the scene
        Deletable[] deletables = FindObjectsOfType<Deletable>();

        //Loop through each one
        foreach (Deletable deletable in deletables)
        {
            //Get the Transform Component
            Transform transformComponent = deletable.GetComponent<Transform>();

            //Get the Editable Component
            EditableObject editable = deletable.GetComponent<EditableObject>();

            //Check if its name is "Win Platform"
            if (transformComponent.name == "Winner Platform")
            {
                //Extract Position
                Vector3 position = transformComponent.position;
                winPlatformPos = position;
                victoryPlatformPos = winPlatformPos;
            }

            else
            {
                //Get Name
                string n = transformComponent.name;

                //Remove (clone)
                string type = n.Replace("(Clone)", "");

                //Retrieve Editable Object component
                if (deletable.obj != null)
                {
                    //Create Level Editor Item
                    LevelEditorItem temp_lvl = new LevelEditorItem()
                    {
                        id = deletable.obj.ID,
                        pos = transformComponent.position,
                        rot = transformComponent.rotation,
                        scale = transformComponent.localScale,
                    };

                    if (type == "Collider")
                    {
                        bouncers.Add(temp_lvl);
                    }

                    else if (type == "Left Booster")
                    {
                        leftBoosters.Add(temp_lvl);
                    }

                    else if (type == "Right Booster")
                    {
                        rightBoosters.Add(temp_lvl);
                    }

                    else if (type == "Mega Collider")
                    {
                        megaBouncers.Add(temp_lvl);
                    }
                }

                else
                {
                    LevelEditorItem level = new LevelEditorItem()
                    {
                        id=DateTime.Now.Millisecond.ToString(),
                        pos = transformComponent.position,
                        rot = transformComponent.rotation,
                    };

                    //Has to be Blocker
                    blockers.Add(level);
                }
            }
        }

        //Create Level
        Level lvl = new Level()
        {
            blockers = blockers,
            bouncers =bouncers,
            leftBoosters = leftBoosters,
            rightBoosters = rightBoosters,
            megaBouncers = megaBouncers,
        };

        CurrentEditingLevel = lvl;
    }

    public void SetKeys()
    {
        //Get All of the Deletables in the scene
        Deletable[] deletables = FindObjectsOfType<Deletable>();

        //Our Hint keys
        List<HintKey> keys = new List<HintKey>();

        //Loop through each one
        foreach (Deletable deletable in deletables)
        {
            //Get the Transform Component
            Transform transformComponent = deletable.GetComponent<Transform>();

            //Get the Editable Component
            EditableObject editable = deletable.GetComponent<EditableObject>();

            //Check if its name is "Win Platform"
            if (deletable.obj != null && transformComponent.name != "Winner Platform")
            {
                //Extract Position
                HintKey key = new HintKey()
                {
                    ID = editable.ID,
                    Position = transformComponent.position
                };
                keys.Add(key);
            }
        }

        //Create Stage
        Stage stage = new Stage()
        {
            id = Guid.NewGuid().ToString(),
            init = CurrentEditingLevel,
            keys = keys.ToArray(),
            victoryPlatformPos = victoryPlatformPos
        };
        CurrentStage = stage;

        Write(stage);
    }

    public void Write(Stage stage)
    {
        string json = JsonUtility.ToJson(stage);
        string path = @"D:\Projects\v2\unity\Some 2D Game idk\Assets\Level Editor Stuff\Levels" + @"\" + stage.id + @"_stage.json";
        File.WriteAllText(path, json);
        Debug.Log(json);
    }
}

[System.Serializable]
public struct LevelEditorItem
{
    public string id;
    public Quaternion rot;
    public Vector3 pos;
    public Vector3 scale;
}

[System.Serializable]
public class Level
{
    public List<LevelEditorItem> blockers;
    public List<LevelEditorItem> leftBoosters;
    public List<LevelEditorItem> rightBoosters;
    public List<LevelEditorItem> bouncers;
    public List<LevelEditorItem> megaBouncers;
}

[System.Serializable]
public class Stage
{
    public string id;
    public Vector3 victoryPlatformPos;
    public Level init;
    public HintKey[] keys;
}
