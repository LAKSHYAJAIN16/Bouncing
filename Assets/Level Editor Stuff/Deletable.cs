using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deletable : MonoBehaviour
{
    public EditSystem editSystem;
    public EditableObject obj;
    public bool isEditable = true;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (isEditable)
            {
                editSystem.RemoveEditable(obj.ID);
            }
            Destroy(gameObject);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 rot = transform.rotation.eulerAngles;
            rot.z -= 0.5f;
            transform.rotation = Quaternion.Euler(rot);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 rot = transform.rotation.eulerAngles;
            rot.z += 0.5f;
            transform.rotation = Quaternion.Euler(rot);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 scale = transform.localScale;
            scale.x += 0.05f;
            transform.localScale = scale;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 scale = transform.localScale;
            scale.x -= 0.05f;
            transform.localScale = scale;
        }


        if (Input.GetKey(KeyCode.Alpha1))
        {
            Vector3 scale = transform.localScale;
            scale.y -= 0.01f;
            transform.localScale = scale;
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            Vector3 scale = transform.localScale;
            scale.y += 0.01f;
            transform.localScale = scale;
        }
    }
}
