using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarMover : MonoBehaviour
{
    public Transform barRotation;
    public Quaternion startPos;
    public Quaternion endPos;
    public Gamemanager gm;

    // Update is called once per frame
    void Update()
    {
        barRotation.rotation = Quaternion.Lerp(startPos, endPos, gm.timer / 90f);
    }
}
