using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint : MonoBehaviour
{
    public GameObject Pancil;
    bool isPreas = false;

    List<GameObject> paints = new List<GameObject>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isPreas = true;
            var mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            paints.Add(Instantiate(Pancil, mousePos, Quaternion.identity));
        }

        if (isPreas)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            paints[paints.Count - 1].transform.position = mousePos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isPreas = false;
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            for (int i = 0; i < paints.Count; i++)
            {
                Destroy(paints[i]);
            }
            paints.Clear();
        }
    }
}
