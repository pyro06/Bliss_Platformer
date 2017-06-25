using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingTiles : MonoBehaviour
{
    [SerializeField]
    DisappearingTilesSetOne[] setOneTiles;

	// Use this for initialization
	IEnumerator Start ()
    {
        setOneTiles = FindObjectsOfType<DisappearingTilesSetOne>();
        yield return new WaitForSeconds(1);
        StartCoroutine("FadeInOutTileSetOne");
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    IEnumerator FadeInOutTileSetOne()
    {
        while(true)
        {
            for(int i = 0; i < setOneTiles.Length; i++)
            {
                setOneTiles[i].gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(4);
            for (int i = 0; i < setOneTiles.Length; i++)
            {
                setOneTiles[i].gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(4);
        } 
    }
}
