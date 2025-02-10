using MusArcadia.Assets.Scripts.BattleScene;
using MusArcadia.Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace MusArcadia.Assets.Scripts.UI {
    public class ButtonController : MonoBehaviour
    {
        public enum ButtonState
        {
            Nothing,
            Fight,
            UseItem,
            CastMagic,
            ViewParty,
            Run
        }

        public ButtonState state = ButtonState.Nothing;

        public BattleManager battleManager;

        public BattleUI battleUI;

        public Image actionPanel;

        public Image itemPanel;

        public Image magicPanel;

        public Image partyPanel;

        public Image statusPanel;

    



        

        public void OnFightButtonPressed()
        {
            state = ButtonState.Fight;

            // Wait for player to enact turn...

            battleUI.setTurns();
        }

        public void OnItemButtonPressed()
        {
            state = ButtonState.UseItem;
            itemPanel.gameObject.SetActive(true);
            actionPanel.gameObject.SetActive(false);
        }

        public void OnMagicButtonPressed()
        {
            state = ButtonState.CastMagic;
            magicPanel.gameObject.SetActive(true);
            actionPanel.gameObject.SetActive(false);
        }

        public void OnPartyButtonPressed()
        {
            battleUI.UpdatePartyInfo();
            partyPanel.gameObject.SetActive(true);
            actionPanel.gameObject.SetActive(false);
        }

        public void OnRunButtonPressed()
        {
            state = ButtonState.Run;
        }

        public void OnPartyBackPressed()
        {
            state = ButtonState.Nothing;
            partyPanel.gameObject.SetActive(false);
            actionPanel.gameObject.SetActive(true);
        }

        public void OnItemsBackPressed()
        {
            state = ButtonState.Nothing;
            itemPanel.gameObject.SetActive(false);
            actionPanel.gameObject.SetActive(true);
        }

        public void OnMagicBackPressed()
        {
            state = ButtonState.Nothing;
            magicPanel.gameObject.SetActive(false);
            actionPanel.gameObject.SetActive(true);
        }

    }

}

