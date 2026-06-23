using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class CharacterManager : MonoBehaviour
    {
        private Dictionary<string, CharacterData> _characters = new();

        public void RegisterCharacter(CharacterData data)
        {
            if (!_characters.ContainsKey(data.id.Trim()))
                _characters.Add(data.id.Trim(), data);
        }

        public CharacterData? FindCharacterById(string id)
        {
            if (_characters.TryGetValue(id, out var data))
                return data;
            return null;
        }

        public void SetCharacterActive(string id, bool active)
        {
            if (_characters.TryGetValue(id, out var data) && data.bindInstance != null)
                data.bindInstance.SetActive(active);
        }

        public Vector3 GetCharacterPosition(string id)
        {
            if (_characters.TryGetValue(id.Trim(), out var data) && data.bindInstance != null)
                return data.bindInstance.transform.position;
            return Vector3.zero;
        }
    }

    public struct CharacterData
    {
        public string id;
        public string name;
        public GameObject bindInstance;
    }
}