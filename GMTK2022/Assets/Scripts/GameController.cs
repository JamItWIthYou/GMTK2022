using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the game controller class. It is a singleton, meaning it is shared over every scene and there's only one of them. Get it in your script with getGameController().
//It can be used to store things that need to be transferred between scenes e.g. which upgrades the player has or how many levels have been unlocked
public class GameController : MonoBehaviour
{
    private static GameController _gameControl;

    public static GameController getGameController () {
//        if(_gameControl == null) {                                                                These lines could cause bugs but might be necessary to enable if this function needs
//            _gameControl = GameObject.FindObjectOfType<GameController>();                         to be called in Awake(), which could happen before GameController.Awake() sets _gameControl
//        }
        return _gameControl;
    }

    // Ensure that only one Game Controller ever exists
    private void Awake() {
        if (_gameControl != null && _gameControl != this) {
            Destroy(this.gameObject);
        } else {
            _gameControl = this;
        }
        DontDestroyOnLoad(gameObject);
    }
}