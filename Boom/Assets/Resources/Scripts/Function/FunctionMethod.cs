using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FunctionMethod : MonoBehaviour
{
    public static Vector2 GetRelativePositionRandom(){
        int maxX = (int)GameDefine.X_MAX;
        int maxY = (int)GameDefine.Y_MAX;
        int minX = (int)GameDefine.X_MIN;
        int minY = (int)GameDefine.Y_MIN;
        int x = UnityEngine.Random.Range(0, maxX - minX + 1) + minX;
        int y = UnityEngine.Random.Range(0, maxY - minY + 1) + minY;
        return new Vector2(x, y);
    }

    public static IEnumerator EffectChangeScene(string pathScene, float effectTime){
        yield return new WaitForSeconds(effectTime);
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(pathScene);
        while(!loadScene.isDone){
            yield return null;
        }
    }


}
