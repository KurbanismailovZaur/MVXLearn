using System;
using System.Collections;
using UnityEngine;

namespace MVXLearn
{
    public class SettingsCreator : MonoBehaviour
    {
        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                
            }
        }
    }
}