using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace IO.Extensions
{
    public static class IOExtensions
    {

        #region PlayerPrefs


        #region Save

        /// <summary>
        /// Sets the bool and saves it (as int) to PlayerPrefs.
        /// </summary>
        /// <param name="p_key">The string key.</param>
        public static void Save(this bool p_value, string p_key)
        {
            IOPlayerPrefs.SaveBool(p_key, p_value);
        }

        /// <summary>
        /// Sets the float and saves it to PlayerPrefs.
        /// </summary>
        /// <param name="p_key">The string key.</param>
        public static void Save(this float p_value, string p_key)
        {
            IOPlayerPrefs.SaveFloat(p_key, p_value);
        }

        /// <summary>
        /// Sets the int and saves it to PlayerPrefs.
        /// </summary>
        /// <param name="p_key">The string key.</param>
        public static void Save(this int p_value, string p_key)
        {
            IOPlayerPrefs.SaveInt(p_key, p_value);
        }

        /// <summary>
        /// Sets the string and saves it to PlayerPrefs.
        /// </summary>
        /// <param name="p_key">The string key.</param>
        public static void Save(this string p_value, string p_key)
        {
            IOPlayerPrefs.SaveString(p_key, p_value);
        }

        #endregion Save


        #region Load
        /// <summary>
        /// Loads the boolean with the given string key from PlayerPrefs.
        /// </summary>
        /// <param name="p_key">The string key.</param>
        public static bool Load(this bool p_value, string p_key, bool p_defaultValue = false)
        {
            return IOPlayerPrefs.LoadBool(p_key, p_defaultValue);
        }

        /// <summary>
        /// Loads the float with the given string key from PlayerPrefs.
        /// </summary>
        /// <param name="p_key">The string key.</param>
        public static float Load(this float p_value, string p_key, float p_defaultValue = 0f)
        {
            return IOPlayerPrefs.LoadFloat(p_key, p_defaultValue);
        }

        /// <summary>
        /// Loads the int with the given string key from PlayerPrefs.
        /// </summary>
        /// <param name="p_key">The string key.</param>
        public static int Load(this int p_value, string p_key, int p_defaultValue = 0)
        {
            return IOPlayerPrefs.LoadInt(p_key, p_defaultValue);
        }

        /// <summary>
        /// Loads the string with the given string key from PlayerPrefs.
        /// </summary>
        /// <param name="p_key">The string key.</param>
        public static string Load(this string p_value, string p_key, string p_defaultValue = "")
        {
            return IOPlayerPrefs.LoadString(p_key, p_defaultValue);
        }
        #endregion Load


        #endregion PlayerPrefs


        #region Util
        public static void DoIfValid(this Action p_action){
            if (p_action != null)
            {
                p_action();
            }
        }

        public static void DoIfValid<T>(this Action<T> p_action, T p_paremeter){
            if (p_action != null)
            {
                p_action(p_paremeter);
            }
        }
        #endregion Util
    }

}
