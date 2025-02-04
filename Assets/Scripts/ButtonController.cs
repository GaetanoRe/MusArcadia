using MusArcadia.Assets.Scripts.BattleScene;
using MusArcadia.Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace MusArcadia.Assets.Scripts.UI {
    public class ButtonController : MonoBehaviour
    {
        public BattleManager battleManager;

        public BattleUI battleUI;

        public Image actionPanel;

        public Image itemPanel;

        public Image magicPanel;

        public Image partyPanel;


        public void OnFightButtonPressed()
        {

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

