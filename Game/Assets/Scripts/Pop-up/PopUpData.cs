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
    }

    [Serializable] public struct Step{

    public string stepText;
}