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
            itemPanel.gameObject.SetActive(true);
            actionPanel.gameObject.SetActive(false);
        }

        public void OnMagicButtonPressed()
        {
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
            partyPanel.gameObject.SetActive(false);
            actionPanel.gameObject.SetActive(true);
        }

        public void OnItemsBackPressed()
        {
            itemPanel.gameObject.SetActive(false);
            actionPanel.gameObject.SetActive(true);
        }

        public void OnMagicBackPressed()
        {
            magicPanel.gameObject.SetActive(false);
            actionPanel.gameObject.SetActive(true);
        }

    }

}

