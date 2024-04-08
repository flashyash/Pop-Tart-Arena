// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// {
// public class GameManager : MonoBehaviour
// {
//     static public int CharacterIndex = 0;
//     [SerializeField] GameObject[] _CharacterPrefabs;
 
//     void Awake()
//     {
//         // Instantiate selected character = make player
//         Instantiate(_CharacterPrefabs[CharacterIndex]);
//     }
// }
// public class SelectionScript : MonoBehaviour
// {
//     [SerializeField] Button[] _IconPrefabs;
 
//     void Awake()
//     {
//         // Instantiate your icons(Buttons) with characters and add them listener to change CharacterIndex in main script.
//         int length = _IconPrefabs.Length;
//         for (int i = 0; i < length; i++)
//         {
//             int j = i;
//             Button _Icon = Instantiate(_IconPrefabs[i]);
//             _Icon.onClick.AddListener(()=> GameManager.CharacterIndex = j);
//         }
//     }
// }