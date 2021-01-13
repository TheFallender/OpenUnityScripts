/************************************
 * TimeSlowdown.cs                  *
 * Created by: Agustin Ferrari      *
 ************************************/

// With this script you are able to slowdown the time
// as a mechanic. You are able to see the "juice" left
// by attaching a text UI component on the Unity Inspector.

using UnityEngine;
using System.Collections;

public class TimeSlowdown : MonoBehaviour {
    //############### Variables ###############
    float juiceMeter = 0.5f;
    float juiceMeterCapacity = 0.5f;
    public UnityEngine.UI.Text text = null;


    //############### Unity Methods ###############
    private void Update () {
        //On MouseDetect start the routines
        if (Input.GetMouseButtonDown(1)) {
            //Set the TimeScale to the old value (deployed time meter)
            Time.timeScale = juiceMeter;

            //Stop any routines that are still open
            StopCoroutine(ReduceTime());
            StopCoroutine(RestoreJuice());

            //Start reducing time
            StartCoroutine(ReduceTime());
        }

        TimeJuiceUpdate(juiceMeter);
    }


    //############### Methods ###############
    //Reduce TimeScale
    private IEnumerator ReduceTime () {
        //Keep looping until the user stops holding the button
        while (Input.GetMouseButton(1)) {
            //Too slow kills you => GameOver
            if (Time.timeScale - 0.025f <= 0f) {
                Time.timeScale = 0f;                //Completely pause time
                TimeJuiceUpdate(Time.timeScale);    //UpdateTheTimeJuice
                Destroy(this);
                yield break;                        //Finish the iteration
            }

            //Decrease it (slowdown) every X seconds
            Time.timeScale -= 0.005f;
            juiceMeter = Time.timeScale;            //Update time metter

            yield return new WaitForSecondsRealtime(0.05f);
        }

        //Start restoring the time meter
        StartCoroutine(RestoreJuice());

        //Restore the timescale
        Time.timeScale = 1f;
    }

    //Restore the Time quantity
    private IEnumerator RestoreJuice () {
        while (true) {
            //Increase it every X seconds
            juiceMeter += 0.025f;

            //Keep looping until it's equal or bigger than the max capacity 0.5
            if (juiceMeter < juiceMeterCapacity) {
                yield return new WaitForSecondsRealtime(0.08f);
            } else {
                //Reset to the max capacity and set it
                juiceMeter = juiceMeterCapacity;    //Restore to the capacity in case it overshot   
                break;                              //Out of the loop
            }
        }
    }

    //Update Time meter left
    private void TimeJuiceUpdate (float val) {
        //Update the text of the time meter
        if (text != null)
            text.text = Mathf.FloorToInt(val * 2 * 100) + " juice left";
    }
}