using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    bool isPause;

    // COMPONENT
    Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        animator.SetBool("IsPause", isPause);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == TouchCheck.TAG){
            isPause = !isPause;
        }
    }

    public bool GetIsPause(){return this.isPause;}
    public void Click(){this.isPause = !isPause;}
}
