using System.Collections;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 管理遊戲主流程的Class 
/// </summary>
public class GameManager : MonoBehaviour
{
    public enum GameStates
    {
        Start,      // 開始遊戲
        Drawing,    // 開始畫路
        Playing,    // 開始操控
        Restart,    // 重新遊戲
    }

    GameStates gameState = GameStates.Start;
    public GameStates GameState { get => gameState;}

    // 需要物件
    public Paint Paint;
    public Player Player;

    void Update()
    {
        switch (gameState)
        {
            case GameStates.Start:
                startState();
                break;
            case GameStates.Drawing:
                drawState();
                break;
            case GameStates.Playing:
                playState();
                break;
            case GameStates.Restart:
                restartState();
                break;
            default:
                break;
        }
    }

    void changeState(GameStates newState)
    {
        Debug.LogError($"new State = {newState}");
        gameState = newState;
    }

    void startState()
    {
        changeState(GameStates.Drawing);
    }

    void drawState()
    {
        Paint.DrawPath();

        // 清除繪圖
        if (Input.GetKey(KeyCode.Delete))
        {
            changeState(GameStates.Restart);
        }

        // 開始遊戲
        if (Input.GetKey(KeyCode.Insert))
        {
            Paint.ExplanePathNode();
            Player.ActivePlayer(Paint.Paths);

            changeState(GameStates.Playing);
        }
    }

    void playState()
    {
        // 重新遊戲
        if (Input.GetKey(KeyCode.Delete))
        {
            changeState(GameStates.Restart);
        }
    }

    void restartState()
    {
        Paint.CleanPaths();
        Player.UnactivePlayer();
        changeState(GameStates.Start);
    }
}
