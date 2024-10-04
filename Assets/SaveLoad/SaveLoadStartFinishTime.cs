using System.Collections;
using UnityEngine;
using System;

namespace Scripts.SaveLoadNamespace
{
    public sealed class SaveLoadStartFinishTime : SaveLoad<StartFinishTimeData>
    {
        protected override string SaveLoadKey => "StartFinishData";

        protected override StartFinishTimeData LoadDefaultData()
        {
            var data = new StartFinishTimeData();
            return data;
        }

        protected override void SetupData(StartFinishTimeData data)
        {
            data.AddStartTime(DateTime.Now);
        }
    }
}