/************************************
 * InputSettings.cs                 *
 * Created by: Agustin Ferrari      *
 ************************************/

// This script is a basic Action -> Input pairing
// system. You can use this to allow the user to set
// their own keybinds. You are allowed to change the
// key of the actions on the fly!

using System.Collections.Generic;
using UnityEngine;

namespace Settings {
    public static class Inputs {
        //############### Base Variables ###############
        #region BaseVariables

        //Each of the available actions
        public enum ActionsInp {
            Left,
            Right,
            Duck,
            Jump,
            Menu,
            Pause,
        };

        //The mapping of each action to it's key
        private static Dictionary<ActionsInp, KeyCode> keymap = new Dictionary<ActionsInp, KeyCode>() {
            {ActionsInp.Left , KeyCode.A},
            {ActionsInp.Right , KeyCode.D},
            {ActionsInp.Duck , KeyCode.S},
            {ActionsInp.Jump , KeyCode.Space},
            {ActionsInp.Menu , KeyCode.E},
            {ActionsInp.Pause , KeyCode.Escape},
        };

        #endregion BaseVariables



        //############### Dictionary Info ###############
        #region DictionaryInfo

        //Modify the key for an action (allow for multiple actions with same key)
        public static void ModifyInput (ActionsInp action, KeyCode keyPressed) {
            keymap[action] = keyPressed;
        }

        //Get the key of an action
        public static KeyCode GetActionKey (ActionsInp action) {
            return keymap[action];
        }

        #endregion DictionaryInfo



        //############### Inputs Bool ###############
        #region InputsBool

        //Get Input
        public static bool GetInput (ActionsInp action) {
            return Input.GetKey(keymap[action]);
        }

        //Get Input Down
        public static bool GetInputDown (ActionsInp action) {
            return Input.GetKeyDown(keymap[action]);
        }

        //Get Input Up
        public static bool GetInputUp (ActionsInp action) {
            return Input.GetKeyUp(keymap[action]);
        }

        #endregion InputsBool



        //############### Save Manipulation ###############
        #region SaveManipulation

        //String to save to the file
        public static string StringToSave () {
            return JsonUtility.ToJson(keymap);
        }

        //Dictionary to build from the save
        public static void LoadFromSave (string jsonString) {
            keymap = JsonUtility.FromJson<Dictionary<ActionsInp, KeyCode>>(jsonString);
        }

        #endregion SaveManipulation
    }
}