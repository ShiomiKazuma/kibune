using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryStarter : MonoBehaviour
{
    [SerializeField]
    GameObject LastDialogue;

    public void SetObjectiveAutomatically()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Objective");
        var mapI = GameObject.FindObjectOfType<ObjectiveMapIndicator>();
        mapI.SetTarget(go.transform);
    }

    public void SetObjective(Transform tr)
    {
        var mapI = GameObject.FindObjectOfType<ObjectiveMapIndicator>();
        mapI.SetTarget(tr.transform);
    }

    public void GotoLastDialogue()
    {
        var data = FindFirstObjectByType<FramedEventsInGameGeneralManager>().ReadSaveData();
        data.Finished[2] = true;
        var fMan = GameObject.FindObjectOfType<FlagManager>();
        fMan.OverwriteProgress(data.Finished);
        var man = GameObject.FindAnyObjectByType<FramedEventsInGameGeneralManager>();
        man.SaveData();
        man.TryGetSetProgressData();
        LastDialogue.GetComponent<SimpleConversation>().enabled = true;
        SetObjective(LastDialogue.transform);
    }

    public void RunStoryByIndex(int index)// index�ȍ~�̃X�g�[���[�𑖂点��
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

    private void Start()
    {
        RunStory();
    }
}
