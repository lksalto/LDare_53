using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAttack : MonoBehaviour
{
    public GameMaster gm;

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player"){
            Throwing player = other.gameObject.GetComponent<Throwing>();
            if(player.pckgCount > 0)
                player.ThrowBox(0);
            else gm.ResetLevel();
        }
    }
}
