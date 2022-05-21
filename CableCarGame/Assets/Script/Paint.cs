using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint : MonoBehaviour
{
    public GameObject Pancil;
    bool isPress = false;
    bool IsHavePath = false;

    List<GameObject> paints = new List<GameObject>();
    public List<Vector3> Paths = new List<Vector3>();

    public void DrawPath()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsHavePath)
            {
                IsHavePath = false;
                CleanPaths();
            }

            isPress = true;
            var mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            paints.Add(Instantiate(Pancil, mousePos, Quaternion.identity));
        }

        if (isPress)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            paints[paints.Count - 1].transform.position = mousePos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            IsHavePath = true;
            isPress = false;
        }
    }

    public void ExplanePathNode()
    {
        // ù∞ï“List
        var tempPosList = new Vector3[999];
        var maxCount = paints[0].GetComponent<TrailRenderer>().GetPositions(tempPosList);

        for (int i = 0; i < maxCount; i++)
        {
            var current = tempPosList[i];
            var next = tempPosList[i + 1];
            var intervalX = (next.x - current.x) / 100;
            var intervalY = (next.y - current.y) / 100;

            for (int j = 1; j <= 100; j++)
            {
                var newpos = current + new Vector3(intervalX * j, intervalY * j, 0);
                Paths.Add(newpos);
            }
        }
    }

    public void CleanPaths()
    {
        for (int i = 0; i < paints.Count; i++)
        {
            Destroy(paints[i]);
        }
        paints.Clear();
        Paths.Clear();
    }
}
