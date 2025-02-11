using MusArcadia.Assets.Scripts.BattleScene;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

namespace MusArcadia.Assets.Scripts.BattleScene{
    public class Unit : MonoBehaviour
    {
        public new string name;
        public Entity playerInfo;

        public int level;

        public Sprite playerSprite;
        public Transform playerTransform;

        public SpriteRenderer playerRenderer;
        public float currentHealth;
        public float maxHealth;
        public float currentMana;
        public float maxMana;

        public float physicalDamage;
        public float magicDamage;

        public float physicalDefense;
        public float magicDefense;

        public bool dead;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (playerInfo != null)
            {
                dead = false;
                gameObject.SetActive(true);
                level = playerInfo.statSheet.level;
                currentHealth = playerInfo.health;
                maxHealth = playerInfo.maxHealth;
                currentMana = playerInfo.mana;
                maxMana = playerInfo.maxMana;
                physicalDamage = playerInfo.meleeDamage;
                magicDefense = playerInfo.magicalDefense;
                physicalDefense = playerInfo.physicalDefense;
                playerSprite = playerInfo.entityModel;
                playerRenderer = GetComponent<SpriteRenderer>();
                playerTransform = GetComponent<Transform>();
                playerRenderer.sprite = playerSprite;
            }

            else
            {
                gameObject.SetActive(false);
            }

        }

        // Update is called once per frame
        void Update()
        {
            gameObject.SetActive(!dead);
        }
    }

}
