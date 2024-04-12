using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FadeRemoveBehaviour : StateMachineBehaviour
{
    public float fadeTime = 0.05f;
    private float timeElapse = 0;
    SpriteRenderer spriteRenderer;
    GameObject objToRemove;
    Color starColor;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapse = 0f;
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        starColor = spriteRenderer.color;
        objToRemove = animator.gameObject;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapse += Time.deltaTime;

        float newAlpha = starColor.a * (1 - timeElapse / fadeTime);
        spriteRenderer.color = new Color(starColor.r, starColor.g, starColor.b, newAlpha);

        if(timeElapse > fadeTime)
        {
            Destroy(objToRemove);
        }
    }

}
