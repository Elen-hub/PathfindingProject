using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : BaseCharacter
{
    List<BaseMonster> m_tamingList = new List<BaseMonster>();
    public List<BaseMonster> GetTaming { get { return m_tamingList; } }
    public BaseMonster SetTaming { set { m_tamingList.Add(value); } }
    public BaseMonster RemoveTaming { set { m_tamingList.Remove(value); } }
}
