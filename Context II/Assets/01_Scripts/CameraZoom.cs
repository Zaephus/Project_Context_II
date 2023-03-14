using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public string animationName;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void CallThisFromButton()
    {
        anim.Play(animationName);
    }
}
