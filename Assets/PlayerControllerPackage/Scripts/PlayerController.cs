using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerControllerType
{
    FirstPerson,
    ThirdPerson
}
public class PlayerController : MonoBehaviour
{
    public PlayerControllerType controllerType;
    public List <Component> currentScripts = new List <Component> ();
    [HideInInspector]public TextAsset importJson;
}
