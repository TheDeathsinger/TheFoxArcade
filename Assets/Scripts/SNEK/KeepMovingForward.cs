﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeepMovingForward : MonoBehaviour
{
    #region Private Fields
    /// <summary>
    /// stores reference to MoveForward corutine
    /// </summary>
    private IEnumerator coru;

    private float waitTimeBeforeMovement = 0.25f;

    /// <summary>
    /// Diviser in the movement calculation
    /// </summary>
    private float movementDampen = 3.0f;
    #endregion

    private void Awake()
    {
        coru = MoveForward();
        StartCoroutine(coru);
    }

    /// <summary>
    /// Keeps the attachted object moving forward.
    /// Pretty much only ment for the Snek object
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveForward()
    {
        while (true)
        {
            switch (SnekControl.CurrentDesiredDirection)
            {
                case SnekControl.DesiredDirection.Up:
                    gameObject.transform.Translate(Vector2.up / 2);
                    break;
                case SnekControl.DesiredDirection.Down:
                    gameObject.transform.Translate(Vector2.down / 2);
                    break;
                case SnekControl.DesiredDirection.Left:
                    gameObject.transform.Translate(Vector2.left / 2);
                    break;
                case SnekControl.DesiredDirection.Right:
                    gameObject.transform.Translate(Vector2.right / 2);
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(waitTimeBeforeMovement);
        }
    }

    /// <summary>
    /// Loops over snek segemnts and moves them all.
    /// </summary>
    private void MoveSnekStack(Vector2 movementVector)
    {
        List<Segment> segLis = new List<Segment>(SnekSegmentController.GetSegmentList());

        foreach (Segment seg in segLis)
        {
            //Get the gameobject of each segment and transform it in the correct direction
            GameObject go = seg.gameObject;
            go.transform.Translate(movementVector / movementDampen);
        }
    }
}