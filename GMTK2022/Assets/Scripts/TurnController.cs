using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnController : MonoBehaviour
{
    [HideInInspector] public List<Character> characterList;
    [HideInInspector] public Character currentCharacter;
    
    [Range(0, 20)]
    public int level;
    public PlayerController p1;

    void Awake() {
        characterList =  new List<Character>();
    }
    void Start() {
        characterList.Sort();
        startRound();
    }
    public void AddToTurnControl(Character character) {
        characterList.Add(character);
    }
    public void RemoveFromTurnControl(Character character) {
        if(currentCharacter == character) EndTurn(character);
        characterList.Remove(character);
        if (characterList.Count<=1){
            if (characterList [0] is PlayerController){ 
                SceneManager.LoadScene(level+1);
            }else{
                SceneManager.LoadScene(0);
            }
            
        }
    }
    public void EndTurn (Character character) {
        if (currentCharacter == character){
            int index = characterList.IndexOf(character);
            index++;
            if(index<characterList.Count) {
                currentCharacter = characterList[index];
                characterList[index].BeginTurn();
            }
            else startRound();
        }
        else {
            Debug.LogError("EndTurn called from non-currentCharacter character called: " + character);
        }
    }
    private void startRound() {
        currentCharacter = characterList[0];
        characterList[0].BeginTurn();
    }
  
}
