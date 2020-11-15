using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpawnHandler
{
    public CharacterMng.ECharacters SpawnCharacter;
    public Vector2 LocalPosition;
    public bool UseRandom;
}
public class EnermyInstanceVolume : MonoBehaviour
{
    [SerializeField] SpawnHandler[] m_spawnHandler;
    [SerializeField] bool m_useAutoGroup = false;
    [SerializeField] int m_groupNum;

    void Start()
    {
        if(m_spawnHandler != null)
        {
            if (m_useAutoGroup)
                m_groupNum = CharacterMng.Instance.GetBlankGroupNum;

            for (int i = 0; i < m_spawnHandler.Length; ++i)
            {
                Vector2 pos = Vector2.zero;
                if(m_spawnHandler[i].UseRandom)
                {
                    pos.x = Random.Range(-m_spawnHandler[i].LocalPosition.x, m_spawnHandler[i].LocalPosition.x);
                    pos.y = Random.Range(-m_spawnHandler[i].LocalPosition.y, m_spawnHandler[i].LocalPosition.y);
                }
                else
                    pos = (Vector3)m_spawnHandler[i].LocalPosition;

                pos += (Vector2)transform.position;
                CharacterMng.Instance.InstantiateCharacter(m_spawnHandler[i].SpawnCharacter, pos, m_groupNum);
            }
        }
    }
}
