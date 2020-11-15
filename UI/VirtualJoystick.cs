using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : UIBase
{
    List<RaycastResult> m_hitList = new List<RaycastResult>();
    PointerEventData m_eventData;
    bool m_isDown;

    BaseCharacter m_character;
    public override UIBase Init()
    {
        base.Init();
        return this;
    }
    public void Enabled(BaseCharacter mainCharacter)
    {
        m_character = mainCharacter;
    }
    public override void Open() 
    {
        gameObject.SetActive(true);
    }
    public override void Close()
    {
        m_isDown = false;
        m_character.State = ECharacterState.Idle;
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (!m_character)
            return;

        if (m_character.State == ECharacterState.Death)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
                return;

            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(Physics2D.Raycast(worldPosition, Vector3.down))
            {
                m_character.GetAStarAgent.SetDestination(worldPosition);
                m_character.State = ECharacterState.Move;
            }
        }
    }
}
