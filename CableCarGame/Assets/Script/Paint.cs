using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint : MonoBehaviour
{
    public GameObject Pancil;
    bool isPreas = false;
    bool isPlaying = false;

    public GameObject Player;
    List<GameObject> paints = new List<GameObject>();
    public Vector3[] posList = new Vector3[999];
    int index = 0;
    int maxCount = 0;

    void Update()
    {
        if (isPlaying)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                index--;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                index++;
            }

            index = Mathf.Clamp(index, 0, maxCount - 1);
            Player.transform.position = posList[index];
        }
        else
        {
            drawMode();
        }


        if (Input.GetKeyDown(KeyCode.Delete))
        {
            for (int i = 0; i < paints.Count; i++)
            {
                Destroy(paints[i]);
            }
            paints.Clear();
            isPlaying = false;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            isPlaying = true;

            maxCount = paints[0].GetComponent<TrailRenderer>().GetPositions(posList);
            Player.transform.position = posList[index];
        }
    }

    void drawMode()
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
    }
}
