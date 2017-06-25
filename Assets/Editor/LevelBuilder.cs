using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR //works when used in unity editor but not when building the game
using UnityEditor;
#endif
public class LevelBuilder : ScriptableWizard
{
    //public GameObject objectOne;

    //public GameObject objectTwo;

    //public List<int> totalSize;

    //public Vector2 tileSize;

    public string gameObjectName;

    [MenuItem("Level Builder/Open Window")]
	private static void CreateLevelBuilderMenu()
    {
        ScriptableWizard.DisplayWizard<LevelBuilder>("LevelBuilder", "Create");
    }

    private void OnWizardCreate()
    {

        GameObject emptyGameobject = new GameObject(gameObjectName);

        /*Vector2 tilePosition = new Vector2(0, 0);
        for (int i = 0; i< totalSize.Count; i++)
        {
            if (totalSize[i] == 1)
            {
                GameObject obj = Instantiate(objectOne, tilePosition, Quaternion.identity);
                tilePosition = tilePosition + tileSize;
                obj.transform.parent = emptyGameobject.transform;
            }
            else if (totalSize[i] == 2)
            {
                GameObject obj = Instantiate(objectTwo, tilePosition, Quaternion.identity);
                tilePosition = tilePosition + tileSize;
                obj.transform.parent = emptyGameobject.transform;
            }
            else
            {
                GameObject obj = Instantiate(objectOne, tilePosition, Quaternion.identity);
                tilePosition = tilePosition + tileSize;
                obj.transform.parent = emptyGameobject.transform;
            }
            
        }*/
    }
}
