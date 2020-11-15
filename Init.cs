using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Init : MonoBehaviour
{
    VirtualJoystick m_joystickUI;
    AGrid grid;

    private void Awake()
    {
        CharacterMng.Instance.Init();
        UIMng.Instance.Init();
        AStarMng.Instance.Init();
        PlayerCharacter mainCharacter = CharacterMng.Instance.MainCharacter;
        UIMng.Instance.Open<VirtualJoystick>(UIMng.UIName.VirtualJoystick).Enabled(mainCharacter);
        UIMng.Instance.Open<MapUI>(UIMng.UIName.MapUI);

        string path = "Assets/AStarBuildArray.astar";
        TextAsset asset = new TextAsset(File.ReadAllText(path));
        string[] value = asset.text.Split('_');
        GameObject obj = new GameObject(typeof(AGrid).ToString(), typeof(AGrid));
        AGrid grid = obj.GetComponent<AGrid>();
        string[] arr = value[0].Split(',');
        grid.UnwalkableLayer = int.Parse(arr[0]);
        grid.NodeRadius = float.Parse(arr[1]);
        grid.WorldSize.x = float.Parse(arr[2]);
        grid.WorldSize.y = float.Parse(arr[3]);
        grid.CreateGrid(value);
        AStarMng.Instance.Grid = grid;
    }
}
