using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryStarter : MonoBehaviour
{
    private void Start()
    {
        var manager = GameObject.FindObjectOfType<FramedEventsInGameGeneralManager>();
        var prog = manager.ReadSaveData().Finished;
        manager.RunStory(prog);
        Debug.Log($"Run Story : a-{prog[0].ToString()},b-{prog[1].ToString()},c-{prog[2].ToString()}");
    }
}
