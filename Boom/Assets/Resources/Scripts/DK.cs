using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DK : MonoBehaviour
{
    GameObject touchCheck;

    private void Update()
    {
        // if(SceneManager.GetActiveScene().name != "MapChoesScene"
        //     && Camera.main.GetComponent<GameManager>().GetIsPause()) return;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchCheck = (GameObject)Instantiate(Resources.Load("Prefabs/TouchCheck"));
                    touchCheck.transform.position = Camera.main.ScreenToWorldPoint(touch.position);
                    break;
                case TouchPhase.Ended:
                    Destroy(touchCheck);
                    break;
            }
        }
    }
}
