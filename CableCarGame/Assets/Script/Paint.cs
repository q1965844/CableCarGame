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
    public List<Vector3> posList = new List<Vector3>();
    int index = 0;
    bool isUp = false;

    // à⁄ìÆ
    float velocity = 0;
    public int Mass = 10;
    public int Speed = 10;
    public int MaxSpeed = 10;
    public float Friction = 0.98f;
    int input;
    public int acceleration = 1;


    void Update()
    {
        if (!isPlaying)
        {
            drawMode();
        }

        // äJénóVùE
        StartPlayMode();

        // èdêVóVùE
        NewMethod();
    }

    private void FixedUpdate()
    {
        if (isPlaying)
        {
            playMode();
        }
    }

    private void NewMethod()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            for (int i = 0; i < paints.Count; i++)
            {
                Destroy(paints[i]);
            }
            paints.Clear();
            isPlaying = false;
        }
    }

    private void StartPlayMode()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPlaying = true;

            var tempPosList = new Vector3[999];
            var maxCount = paints[0].GetComponent<TrailRenderer>().GetPositions(tempPosList);

            // ù∞ï“List
            for (int i = 0; i < maxCount; i++)
            {
                var current = tempPosList[i];
                var next = tempPosList[i + 1];
                var intervalX = (next.x - current.x) / 100;
                var intervalY = (next.y - current.y) / 100;

                for (int j = 1; j <= 100; j++)
                {
                    var newpos = current + new Vector3(intervalX * j, intervalY * j, 0);
                    posList.Add(newpos);
                }
            }

            Player.transform.position = posList[index];
        }
    }

    void playMode()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            input = 1;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            input = -1;
        }
        else
        {
            input = 0;
        }

        // å¥ñ{ï˚ñ@
        //velocity = (int)Mathf.Round(Mathf.Lerp(velocity, (velocity + Speed * input + gforce), Time.deltaTime * acceleration));

        // Add Speed (Vt = V0 + at)
        velocity += Speed * input * Time.deltaTime;

        // Add Gravity
        float gforce = Mathf.Ceil(gravityForce());
        velocity += gforce;

        // Add Air Friction
        velocity *= Friction;

        // Limit max speed
        velocity = Mathf.Clamp(velocity, -MaxSpeed, MaxSpeed);

        index += Mathf.RoundToInt(velocity);
        index = Mathf.Clamp(index, 0, posList.Count - 2);
        Player.transform.position = posList[index];
        Debug.Log($"velocity {velocity}, gforce = {gforce} ,input = {input},isUp {isUp}");
    }

    private float gravityForce()
    {
        if (index <= 0)
        {
        return 0;
        }

        float f;
        var now = posList[index];
        var last = posList[index - 1];

        var FDir = (now - last).normalized;
        var sita = Vector3.Angle(FDir, Vector3.right);
        isUp = Vector3.Dot(Vector3.up, FDir) > 0;
        f = Mathf.Abs(Mathf.Sin(sita) * Mass);

        if (isUp)
        {
            f = -f;
        }

        return f;
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
