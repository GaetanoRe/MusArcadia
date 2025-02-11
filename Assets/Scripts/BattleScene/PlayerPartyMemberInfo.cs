using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusArcadia.Assets.Scripts.GeneralUse;
using UnityEngine;
namespace MusArcadia.Assets.Scripts.BattleScene
{
    [CreateAssetMenu(fileName = "New Player Party Member", menuName = "Entity/Player Party Member")]
    public class PlayerPartyMemberInfo : Entity
    {
        
        private float _exp;
        public float exp{ get{
            return _exp;
        } 
        set{
            _exp = value;
            while(_exp >= expCap){
                _exp = value - expCap;
                LevelUp();
                
            }
        } }
        public float expCap {
            get{
                return statSheet.level * 3.05f + 100;
            }
        }

        public List<Armor> armorEquipped;
        public Weapon weaponEquipped;
        public 

        void Start()
        {
            
        }

        public override void attack(Entity subject){
    if(subject is EnemyPartyMemberInfo){
        Debug.Log($"{this.name} is attacking {subject.name}!");
        float allOutDamage = meleeDamage;
        if(weaponEquipped != null){
            allOutDamage += UnityEngine.Random.Range(weaponEquipped.minPhysDamageBonus + weaponEquipped.minElementDamage, 
            weaponEquipped.maxPhysDamageBonus + weaponEquipped.maxElementDamage);
        }

        if(critChanceRoll()){
            allOutDamage *= 2;
            subject.status = StatusEffects.Dazed;
            Debug.Log("Critical hit!");
        }

        Debug.Log($"Damage calculated: {allOutDamage}");
        subject.takeDamage(allOutDamage);
    }
}


        public override void castMagic(Entity subject, Magic spell){
            if(spell.defensive && subject is PlayerPartyMemberInfo){
                base.castMagic(subject, spell);
            }
            else if (subject is EnemyPartyMemberInfo){
                base.castMagic(subject, spell);
            }
            
        }

        public override void useItem(Entity subject, Consumable item)
        {
            if(subject is PlayerPartyMemberInfo){
                subject.health += item.healthHealed;
                subject.mana += item.manaHealed;
            }
        }

        public override void takeDamage(float amount)
        {
            base.takeDamage(amount);
        }

        public override void run()
        {
            
        }

        public virtual void LevelUp(){
            statSheet.level++;
            reevalStats();
        }

        public float totalPhysicalDefense(){
            float totalDefense = 0f;
            foreach(Armor armor in armorEquipped){
                totalDefense += UnityEngine.Random.Range(armor.minPhysDefenseBonus, armor.maxPhysDefenseBonus);
            }
            return totalDefense * 0.5234f;
        }

        public float totalElementalDefense(){
            float totalDefense = 0f;
            foreach(Armor armor in armorEquipped){
                totalDefense += UnityEngine.Random.Range(armor.minElementDefenseBonus, armor.maxElementDefenseBonus);
            }
            return totalDefense * 0.5234f;
        }
    }
}