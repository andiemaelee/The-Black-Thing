using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tutorial;
using static UnityEditor.VersionControl.Asset;
public enum TutorialState
{
    Sub,
    Main,
    End,//이 단계로 넘어가면 오류, 다음단계 0으로 이동해야함.
};


public class TutorialManager : GameManager
{
    private Dictionary<TutorialState, GameState> states;
    private TutorialState currentPattern;

    TutorialManager()
    {
        states = new Dictionary<TutorialState, GameState>();

        states[TutorialState.Sub] = new Tutorial.Sub();
        states[TutorialState.Main] = new Tutorial.Main();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeGameState(TutorialState patternState)
    {
        if (states == null) return;

        if (states.ContainsKey(patternState) == false)
        {
            Debug.Log("없는 패턴 입니다.");
            return;
        }
        if (activeState != null)
        {
            activeState.Exit(this); //미리 정리한다.
        }
        currentPattern = patternState;
        activeState = states[patternState];
        activeState.Enter(this, dot);
    }
}
