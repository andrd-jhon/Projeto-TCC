using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "PopUpData", menuName = "ScriptableObjects/PopUpData", order = 2)]
    public class PopUpData : ScriptableObject {
        public string mechanicName;
        public Sprite mechanicView;
        public string mechanicDescription;
        public List<Step> stepsQuantity;
        public int totalReward;
        public int secondReward;
        public int thirdReward;
        public int fourthReward;
        // public List<string> testeString = new List<string>();
    }

    [Serializable] public struct Step{

        public string stepText;
}

    // [Serializable] public struct Reward{
    //     public int totalReward;
    //     public int secondReward;
    //     public int thirdReward;
    //     public int fourthReward;
    // }

//apartir