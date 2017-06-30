using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public IEnumerator PlayerDeathAndSpawnTimer()
    {
        GameManager.gameManagerInstance.playerAlive = false;
        GameManager.gameManagerInstance.playerInstance.DeathAnimation();
        yield return new WaitForSeconds(0.833f);
        GameManager.gameManagerInstance.playerInstance.DeathAnimationReset();
        GameManager.gameManagerInstance.playerInstance.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        GameManager.gameManagerInstance.playerInstance.gameObject.transform.position = GameManager.gameManagerInstance.spawnPoint;
        GameManager.gameManagerInstance.playerInstance.gameObject.SetActive(true);
        GameManager.gameManagerInstance.playerAlive = true;
        StopCoroutine("PlayerDeathAndSpawnTimer");
    }
}
