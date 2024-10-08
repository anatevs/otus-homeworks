using Scripts.SaveLoadNamespace;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VContainer;

namespace Scripts.Time
{
    public class TimeLogView : MonoBehaviour
    {
        [SerializeField]
        private GameObject _timeTextPrefab;

        [SerializeField]
        private GameObject _startTimeField;

        [SerializeField]
        private GameObject _finishTimeField;

        private StartFinishTimeData _startFinishTimeData;

        [Inject]
        public void Construct(SaveLoadStartFinishTime saveLoadStartFinishTime)
        {
            _startFinishTimeData = saveLoadStartFinishTime.GetData();
        }

        private void Awake()
        {
            var startTimes = _startFinishTimeData.GetStartLocalTimeStrings();
            var finishTimes = _startFinishTimeData.GetFinishLocalTimeStrings();

            SetupLines(startTimes, _startTimeField);
            SetupLines(finishTimes, _finishTimeField);
        }

        private void SetupLines(List<string> lines, GameObject field)
        {
            foreach (var startT in lines)
            {
                AddLine(startT, field);
            }
        }

        private void AddLine(string lineText, GameObject field)
        {
            var lineGO = Instantiate(_timeTextPrefab);

            lineGO.GetComponent<TMP_Text>().text = lineText;

            lineGO.transform.SetParent(field.transform);
        }
    }
}