using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Globals 
{
    public enum PunchState { Jab = 1, Hook, UpperCut, HookJab, JabUpper, UpperHook };
    public static float height = 3.5f;
    internal static float armLength= 0.8f ;
    public static string LearnMenuInformation { get; set; }
    public static readonly string MainMenu = "Main Menu";
    public static readonly string LearnMenu = "Learn Menu";
    public static readonly string Video = "Video";
    public static readonly string Practice = "Practice";
    public static readonly string Calibration = "Calibration_final";
    public static readonly string TrainMenu = "Train Menu";
    public static readonly string COMBO_HOOKJAB = "HookJab";
    public static readonly string COMBO_JABUPPER = "JabUpper";
    public static readonly string COMBO_UPPERHOOK = "UpperHook";
    public static readonly string HOOK = "Hook";
    public static readonly string JAB = "Jab";
    public static readonly string UPPERCUT = "UpperCut";

}
