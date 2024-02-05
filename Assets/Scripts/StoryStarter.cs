using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryStarter : MonoBehaviour
{
    public void SetObjectiveAutomatically()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Objective");
        var mapI = GameObject.FindObjectOfType<ObjectiveMapIndicator>();
        mapI.SetTarget(go.transform);
    }

    public void RunStoryByIndex(int index)// index以降のストーリーを走らせる
    {
        var manager = GameObject.FindObjectOfType<FramedEventsInGameGeneralManager>();
        manager.TryGetSetProgressData();
        var prog = manager.ReadSaveData().Finished;
        for (int i = 0; i <= index; i++)
        {
            prog[i] = true;
        }
        manager.RunStory(prog);
    }
    public void RunStory()
    {
        Debug.Log("Story Starter Running");
        var manager = GameObject.FindObjectOfType<FramedEventsInGameGeneralManager>();
        manager.TryGetSetProgressData();
        var prog = manager.ReadSaveData().Finished;
        manager.RunStory(prog);
    }
}
