using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "KeyWord")]
public class KeyWordSO : ScriptableObject
{
    [SerializeField] private List<string> keyWords;

    public List<string> KeyWords => keyWords;
}
