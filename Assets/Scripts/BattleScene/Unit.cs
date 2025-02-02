using MusArcadia.Assets.Scripts.BattleScene;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

namespace MusArcadia.Assets.Scripts.BattleScene{
    public class Unit : MonoBehaviour
{
    public string name;
    public PlayerPartyMemberInfo playerInfo;

    public int level;

    public Sprite playerSprite;

    public float currentHealth;
    public float maxHealth;
    public float currentMana;
    public float maxMana;

    public float physicalDamage;
    public float magicDamage;


    public float physicalDefense;
    public float magicDefense;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        level = playerInfo.statSheet.level;
        currentHealth = playerInfo.health;
        maxHealth = playerInfo.maxHealth;
        currentMana = playerInfo.mana;
        maxMana = playerInfo.maxMana;
        physicalDamage = playerInfo.meleeDamage;
        magicDefense = playerInfo.magicalDefense;
        physicalDefense = playerInfo.physicalDefense;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

}
