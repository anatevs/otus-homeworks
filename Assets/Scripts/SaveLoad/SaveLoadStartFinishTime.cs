using System;
using Scripts.Time;

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

        protected override void SetupLoadData(StartFinishTimeData data)
        {
            data.AddStartTime(DateTime.Now);
        }

        protected override void SetupSaveData(StartFinishTimeData data)
        {
            data.AddFinishTime(DateTime.Now);
        }
    }
}