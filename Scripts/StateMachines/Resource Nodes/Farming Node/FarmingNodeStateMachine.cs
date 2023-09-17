using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WSS.StateMachines;
using WSS.Attributes;

namespace WSS.StateMachines.FarmingNode
{
    public class FarmingNodeStateMachine : StateMachine
    {
        //Reference Assignment
        [field: SerializeField] public GameObject PlantedState { get; private set; }
        [field: SerializeField] public GameObject FirstGrowthState { get; private set; }
        [field: SerializeField] public GameObject SecondGrowthState { get; private set; }
        [field: SerializeField] public GameObject HarvestState { get; private set; }

        //Timing Variables
        [field: SerializeField] public float TimeBetweenStates { get; private set; }

        private void OnEnable()
        {
            
        }

        private void Start()
        {
            SwitchState(new FarmingNodePlantedState(this));
        }

        private void OnDisable()
        {
            
        }
    }
}