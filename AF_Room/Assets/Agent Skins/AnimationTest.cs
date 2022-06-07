using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class AnimationTest : MonoBehaviour
{
    public GameObject target;

    Tweener moveA, moveB;

    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init(false, false, LogBehaviour.Verbose);
        DOTween.defaultAutoPlay = AutoPlay.None;

        Sequence actions = DOTween.Sequence();

        /*
        GameObject workingTarget = target;
        
        //Tween moveA = transform.DOMoveX(3.0f, 1.0f);
        GameObject moveContainerA = new GameObject("A");
        moveContainerA.transform.parent = this.gameObject.transform;
        workingTarget.transform.parent = moveContainerA.transform;
        workingTarget = moveContainerA;
        Tween moveA = workingTarget.transform.DOLocalMove(new Vector3(3.0f, 0.0f, 0.0f), 1.0f);
        moveA.SetLoops(2, LoopType.Yoyo);
        moveA.onComplete += (() => Debug.Log("Move A Complete"));

        //Tween moveB = transform.DOMoveY(3.0f, 1.0f);
        GameObject moveContainerB = new GameObject("B");
        moveContainerB.transform.parent = this.gameObject.transform;
        workingTarget.transform.parent = moveContainerB.transform;
        workingTarget = moveContainerB;
        Tween moveB = workingTarget.transform.DOLocalMove(new Vector3(0.0f, 3.0f, 0.0f), 1.0f);
        moveB.SetLoops(2, LoopType.Yoyo);
        moveB.onComplete += (() => Debug.Log("Move B Complete"));
        */

        //Tween moveA = transform.DOMoveX(3.0f, 1.0f);
        moveA = transform.DOLocalMove(new Vector3(3.0f, 0.0f, 0.0f), 1.0f).SetRelative().SetAutoKill(false);
        moveA.SetLoops(-1, LoopType.Yoyo);
        moveA.onComplete += (() => Debug.Log("Move A Complete"));

        //Tween moveB = transform.DOMoveY(3.0f, 1.0f);
        moveB = transform.DOLocalMove(new Vector3(0.0f, 3.0f, 0.0f), 1.0f).SetRelative().SetAutoKill(false);
        //Tween moveB = transform.DOLocalMoveY(3.0f, 1.0f);
        moveB.SetLoops(-1, LoopType.Yoyo);
        moveB.onComplete += (() => Debug.Log("Move B Complete"));

        actions.Append(moveA);
        actions.Insert(0.0f, moveB);

        actions.SetLoops(-1, LoopType.Restart);

        actions.Play();
        //moveA.Play();
        //moveB.Play();
    }
}
