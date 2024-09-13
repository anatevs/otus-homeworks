using System;
using System.Collections;
using UnityEngine;

namespace Scripts
{
    public class TimeLogger : MonoBehaviour
    {
        void Start()
        {
            Debug.Log(GetStringTime(DateTime.Now));
        }

        private string GetStringTime(DateTime dateTime)
        {
            return dateTime.ToString("dd.MM.yyyy, HH.mm.ss");
        }
    }
}