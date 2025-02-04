using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettingsSO", menuName = "Game/PlayerSettingsSO")]
public class PlayerSettingsSO : ScriptableObject
{
    // This boolean variable will determine if the player can move in the game.
    // It could be used to enable or disable player movement depending on the game context (e.g., during cutscenes or menus).
    public bool canPlayerMove;
}