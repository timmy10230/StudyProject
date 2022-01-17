using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public static UIController instance;

    public Transform canvas;

	void Awake () {
        if (!instance) instance = this;
        canvas = GameObject.Find("Canvas").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowQuestInfo(Quest quest)
    {
        Transform info = GameObject.Find("Canvas/Quest Info/Background/Info/Viewport/Content").transform;
        info.Find("Name").GetComponent<Text>().text = quest.questName;
        info.Find("Description").GetComponent<Text>().text = quest.description;

        string taskString = "Task: \n";
        if (quest.task.kills != null)
        {
            foreach (Quest.QuestKill qk in quest.task.kills)
            {
                taskString += "Slay" + qk.amount + " " + MonsterDatabase.monsters[qk.id] + ".\n";
            }
        }

        if (quest.task.items != null)
        {
            foreach (Quest.QuestItem qi in quest.task.items)
            {
                taskString += "Bring" + qi.amount + " " + ItemDatabase.items[qi.id] + ".\n";
            }
        }

        if (quest.task.talkTo != null)
        {
            foreach (int id in quest.task.talkTo)
            {
                taskString += "Talk To" + NPCDatabase.npcs[id] + ".\n";
            }
        }

        info.Find("Task").GetComponent<Text>().text = taskString;

        string rewardString = "Reward: \n";
        if (quest.reward.items != null)
        {
            foreach (Quest.QuestItem qi in quest.reward.items)
            {
                rewardString += qi.amount + " " + ItemDatabase.items[qi.id] + ".\n";
            }
        }

        if (quest.reward.exp > 0) rewardString += quest.reward.exp + "Experience .\n";
        if (quest.reward.money > 0) rewardString += quest.reward.money + "Money .\n";
        info.Find("Reward").GetComponent<Text>().text = rewardString;

    }
}
