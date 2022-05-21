using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool isActive = false;

    List<Vector3> pathNodes = new List<Vector3>();
    int index = 0;
    bool isUp = false;

    // 移動
    float velocity = 0;
    public int Mass = 10;
    public int Speed = 10;
    public int MaxSpeed = 10;
    public float Friction = 0.98f;
    int input;
    public int acceleration = 1;

    private void Awake()
    {
        isActive = false;
        gameObject.SetActive(isActive);
    }

    public void ActivePlayer(List<Vector3> path)
    {
        pathNodes = path;
        transform.position = pathNodes[index];
        isActive = true;
        gameObject.SetActive(isActive);
    }
    public void UnactivePlayer()
    {
        index = 0;
        transform.position = Vector3.zero;
        isActive = false;
        gameObject.SetActive(isActive);
    }

    void FixedUpdate()
    {
        if (isActive == false)
        {
            return;
        }

        playMode();
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

        // 原本方法
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
        index = Mathf.Clamp(index, 0, pathNodes.Count - 2);
        transform.position = pathNodes[index];
        Debug.Log($"index {index}, velocity {velocity}, gforce = {gforce} ,input = {input},isUp {isUp}");
    }

    private float gravityForce()
    {
        if (index <= 0)
        {
            return 0;
        }

        float f;
        var now = pathNodes[index];
        var last = pathNodes[index - 1];

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
}


