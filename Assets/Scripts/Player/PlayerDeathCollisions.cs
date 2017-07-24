using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathCollisions : MonoBehaviour
{

	public virtual void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Effect();
        }
    }

    public virtual void Effect()
    {
        GameManager.gameManagerInstance.playerInstance.gameObject.SetActive(false);
        StartCoroutine(PlayerDeadTimer(2));
       
    }

    IEnumerator PlayerDeadTimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        LevelManager.levelMangerInstance.ReSpawnPlayerSameLevel();
        GameManager.gameManagerInstance.playerInstance.gameObject.SetActive(true);
    }
}
