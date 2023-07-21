using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Game.Scripts.Utils
{
    public static class Helper
	{
        #region CameraMain

        private static Camera _camera;
        public static Camera Camera
        {
            get
            {
                if (_camera == default)
                {
                    _camera = Camera.main;
                }

                return _camera;
            }
        }

        #endregion
       
        #region WaitDictionary
        
        private static readonly Dictionary<float, WaitForSeconds> WaitDictionary = new Dictionary<float, WaitForSeconds>();
        
        public static WaitForSeconds GetWait(float time)
        {
            if (WaitDictionary.TryGetValue(time, out var wait))
                return wait;

            WaitDictionary[time] = new WaitForSeconds(time);
            return WaitDictionary[time];
        }
        
        public static IEnumerator WaitCoroutine(float duration, Action finished = null)
        {
            yield return GetWait(duration);
            finished?.Invoke();
        }
        
        #endregion

        #region PointerOverUI

        private static PointerEventData _eventDataCurrentPos;
        private static List<RaycastResult> _result;
        public static bool IsPointerOverUI()
        {
            _eventDataCurrentPos = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
            _result = new List<RaycastResult>();
            EventSystem.current.RaycastAll(_eventDataCurrentPos, _result);
            return _result.Count > 0;
        }
        
        public static List<RaycastResult> RaycastAllUI()
        {
            _eventDataCurrentPos = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
            _result = new List<RaycastResult>();
            EventSystem.current.RaycastAll(_eventDataCurrentPos, _result);
            return _result;
        }
        
        #endregion

        #region SaveDataString

        public static async void DelayForSeconds(float delay, Action doAfterDelay)
        {
            var milliseconds = (int)(delay * 1000);
            await Task.Delay(milliseconds);
            doAfterDelay?.Invoke();
        }

        #endregion
        
        #region GetRandomPointOnPlane

        public static Vector3 GetRandomPointOnPlane(Transform target, float radius)
        {
            Vector2 insideUnitCircle = Random.insideUnitCircle;            
            Vector3 direction = new Vector3(insideUnitCircle.x, 0, insideUnitCircle.y);            
            return target.position + direction * radius;        
        }

        #endregion

        #region ListShuffle

        public static List<T> ShuffleList<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
            return list;
        }

        #endregion
        
        #region MoneyShrinker

        public static string MoneyString(this int moneyValue)
        {
            if (moneyValue < 0)
            {
                return "0";
            }
            
            if (moneyValue < 10_000)
            {
                return moneyValue.ToString();
            }
            
            string moneyString = moneyValue.ToString();
            int thousandCount = (moneyString.Length - 1) / 3;
            switch (thousandCount)
            {
                case 1:
                    moneyString = moneyString.Substring(0, moneyString.Length - 3) + "K";
                    break;
                case 2:
                    moneyString = moneyString.Substring(0, moneyString.Length - 6) + "M";
                    break;
                case 3:
                    moneyString = moneyString.Substring(0, moneyString.Length - 9) + "B";
                    break;
                default:
                    moneyString = moneyString.Substring(0, moneyString.Length - 9 ) + "B";
                    break;
            }
            return moneyString;
        }
        
        #endregion
        
        #region SaveDataString

        public static void SaveDateTime(string saveKey, DateTime dateTime)
        {
            string convertedToStringDateTime = dateTime.ToString("u", CultureInfo.InvariantCulture);
            PlayerPrefs.SetString(saveKey, convertedToStringDateTime);
        }

        public static DateTime GetDateTime(string saveKey)
        {
            if (PlayerPrefs.HasKey(saveKey))
            {
                string convertedToStringDateTime = PlayerPrefs.GetString(saveKey);
                DateTime dateTime = DateTime.ParseExact(convertedToStringDateTime, "u", CultureInfo.InvariantCulture);
                return dateTime;
            }
            
            return DateTime.Now;
        }
        
        #endregion
    }
}
