using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAttack : MonoBehaviour
{
    private GameMaster gm;
    public bool inCooldown = false;

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player"){
            Throwing player = other.gameObject.GetComponent<Throwing>();
            if (player.pckgCount > 0 && !inCooldown)
            {
                StopAllCoroutines();
                StartCoroutine(Cooldown());
                player.ThrowBox(Random.insideUnitSphere.normalized, 10000f);
            }
            else if(!inCooldown)
            {
                gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
                gm.FailLevel();
            }
        }
    }

    IEnumerator Cooldown(){
        inCooldown = true;
        yield return new WaitForSeconds(2f);
        inCooldown = false;
    }
}
