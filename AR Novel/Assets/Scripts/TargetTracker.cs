using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Net;
using Vuforia;

// https://answers.unity.com/questions/1308579/combine-two-or-more-image-targets-simultaneously-t.html
// https://developer.vuforia.com/forum/unity-extension-technical-discussion/imagetargetbehaviour-change-just-image-target-not-dataset-help-please
// https://answers.unity.com/questions/1198506/how-change-image-target-behaviour-in-vuforia.html
public class TargetTracker : MonoBehaviour, ITrackableEventHandler
{
    // Vuforia Mode
    public enum Level {
        LevelOne,
        LevelTwo,
        LevelThree,
        Unknown
    }
    public Level ARNovelMode = Level.Unknown;
    private Level ARNovelModeFound = Level.Unknown;
    [HideInInspector] public TrackableBehaviour trackableBehaviour;

    [HideInInspector] public bool isARVuforiaImageTargetTracked = false;
    [HideInInspector] public GameObject vuforiaImageTarget;
    public GameObject rootObject;

    protected virtual void Awake()
     {
         trackableBehaviour = GetComponent<TrackableBehaviour>();
         if (trackableBehaviour) {
            trackableBehaviour.RegisterTrackableEventHandler(this);
            
            //DefaultTrackableEventHandler eventHandlerComponent = trackableBehaviour.GetComponent<DefaultTrackableEventHandler>();
         } else {
            Debug.Log("DEBUG LOG: Did not register trackable behaviour \n");
            Console.Write("CONSOLE WRITE: Did not register trackable behaviour  \n");
         }

        if (vuforiaImageTarget == null ) {
            Console.Write ("VuforiaImageTarget was not found !!\n");
        }
             
    }

    // Vuforia
    public void OnTrackableStateChanged(
              TrackableBehaviour.Status previousStatus,
              TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED
            || newStatus == TrackableBehaviour.Status.TRACKED)
        {
            TrackedBehaviour();
        } else {
            TrackLostBehaviour();
        }

    }
    
    private void TrackedBehaviour() {
        
        Debug.Log("==================================================\n");
        Console.Write("==================================================\n");

        //Debug.Log("DEBUG LOG:  TARGET DETECTED\n");
        //Console.Write("CONSOLE WRITE: TARGET DETECTED\n");
        //Debug.Log("DEBUG LOG MY TRACKABLE: Trackable: " + trackableBehaviour.TrackableName+",   gameobject name: "+trackableBehaviour.name + ", Tracka: "+trackableBehaviour.Trackable.Name +",  ID: "+trackableBehaviour.Trackable.ID+"\n");
        //Console.Write("CONSOLE WRITE MY TRACKABLE: Trackable: " + trackableBehaviour.TrackableName+",   gameobject name: "+trackableBehaviour.name + ", Tracka: "+trackableBehaviour.Trackable.Name +",  ID: "+trackableBehaviour.Trackable.ID+"\n");
        isARVuforiaImageTargetTracked = true;

        switch (trackableBehaviour.TrackableName) {
            case "CircleMarkerOne":
                ARNovelModeFound = Level.LevelOne;
                Debug.Log("DEBUG LOG MY TRACKABLE: CircleMarkerOne\n");
                Console.Write("CONSOLE WRITE MY TRACKABLE: CircleMarkerOne\n");

                break;
            case "CircleMarkerTwo":
                ARNovelModeFound = Level.LevelTwo;
                Debug.Log("DEBUG LOG MY TRACKABLE: CircleMarkerTwo\n");
                Console.Write("CONSOLE WRITE MY TRACKABLE: CircleMarkerTwo\n");

                break;
            case "CircleMarkerThree": 
                ARNovelModeFound = Level.LevelThree;
                Debug.Log("DEBUG LOG MY TRACKABLE: CircleMarkerThree\n");
                Console.Write("CONSOLE WRITE MY TRACKABLE: CircleMarkerThree\n");

                break;
            default: 
                ARNovelModeFound = Level.Unknown;
                break;
        }

        if (IsThereOnlyOneActiveImageTarget()) {
            vuforiaImageTarget = trackableBehaviour.gameObject;
            UIManager.target = this;
        } else {
            UIManager.target = null;
            vuforiaImageTarget = null;
        }
    }

    private void TrackLostBehaviour() {
        //Debug.Log("DEBUG LOG:  TARGET LOST DETECTION " + trackableBehaviour.TrackableName +"\n");
        //Console.Write("CONSOLE WRITE: TARGET LOST DETECTION "+ trackableBehaviour.TrackableName + "\n");
        isARVuforiaImageTargetTracked = false;
        ARNovelModeFound = Level.Unknown;
        vuforiaImageTarget = null;
        UIManager.target = null;
    }

    private bool IsAllImageTargetsTracked()
     {
         bool value = true;
 
         foreach (TargetTracker m in FindObjectsOfType<TargetTracker>())
         {
             if (!m.isARVuforiaImageTargetTracked)
                 value = false;
         }

        if (value) {
            Debug.Log("DEBUG LOG:  ALL TARGETS TRACKED\n");
            Console.Write("CONSOLE WRITE: ALL TARGETS NOT TRACKED\n");
        }
         return value;
     }

     
     private bool IsThereOnlyOneActiveImageTarget() {
         bool value = true;
 
         foreach (TargetTracker targetTracker in FindObjectsOfType<TargetTracker>())
         {
             if (targetTracker != this && targetTracker.isARVuforiaImageTargetTracked && ARNovelMode != ARNovelModeFound)
                 value = false;
         }

        if (value) {
            Debug.Log("===============================================================\n");
            Console.Write("===============================================================\n");
            Debug.Log("DEBUG LOG:  THE ONLY ACTIVE TARGET IS "+ trackableBehaviour.TrackableName +"\n");
            Console.Write("CONSOLE WRITE: THE ONLY ACTIVE TARGET IS "+ trackableBehaviour.TrackableName +"\n");
        }

         return value;
     }


}
