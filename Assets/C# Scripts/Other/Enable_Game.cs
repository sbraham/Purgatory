using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enable_Game : MonoBehaviour {

    public GameObject game;

    void Awake ()
    {
        game.SetActive(true);
    }
}
