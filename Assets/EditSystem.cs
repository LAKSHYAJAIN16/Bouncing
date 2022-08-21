using UnityEngine;
using System.Linq;

public class EditSystem : MonoBehaviour
{
    public EditableObject[] AllEditables;

    public void SwitchOff()
    {
        foreach (EditableObject item in AllEditables)
        {
            item.canDrag = false;
        }
    }

    public void SwitchOn()
    {
        foreach (EditableObject item in AllEditables)
        {
            item.canDrag = true;
        }
    }

    public void RemoveEditable(string id)
    {
        AllEditables = AllEditables.Where(e => e.ID != id).ToArray();
    }
}
