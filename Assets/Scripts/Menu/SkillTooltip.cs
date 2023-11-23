using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Settings;

public class SkillTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        int index = transform.GetSiblingIndex();
        int id = MyCharacterController.Instance.skillDictionary[index];
        string text = "", skillElemental, skillDamageType;
        if (id != -1)
        { 
            if (LocalizationSettings.SelectedLocale.Equals(LocalizationSettings.AvailableLocales.GetLocale("en")))
            {
                text = "<b>Level: </b>";
                if (MyCharacterController.Instance.levelDictionary[index] == 5)
                    text += "Max\n<b>Next level cost:</b> Max\n";
                else
                    text += MyCharacterController.Instance.levelDictionary[index] + "\n<b>Next level cost:</b> " + (MyCharacterController.Instance.levelDictionary[index] * 3 * 60) + "\n";
                text += "<b>Type: </b>";
                skillElemental = MapController.Instance.prizeHolder.prizeSkillList[id].skillRef.skillElemental;
                switch (skillElemental)
                {
                    case "yin":
                        text += "Yin";
                        break;
                    case "yang":
                        text += "Yang";
                        break;
                    case "taichi":
                        text += "Taichi";
                        break;
                }
                text += "\n<b>Damage type: </b>";
                skillDamageType = MapController.Instance.prizeHolder.prizeSkillList[id].skillRef.skillType;
                switch (skillDamageType)
                {
                    case "Internal":
                        text += "Internal";
                        break;
                    case "External":
                        text += "External";
                        break;
                }
            }
            else
            {
                text = "<b>Ca�p �o�: </b>";
                if (MyCharacterController.Instance.levelDictionary[index] == 5)
                    text += "To�i �a\n<b>Tie�n ca�p �o� tie�p theo:</b> To�i �a\n";
                else
                    text += MyCharacterController.Instance.levelDictionary[index] + "\n<b>Tie�n ca�p �o� tie�p theo:</b> " + (MyCharacterController.Instance.levelDictionary[index] * 3 * 60) + "\n";
                text += "<b>Thuo�c t�nh: </b>";
                skillElemental = MapController.Instance.prizeHolder.prizeSkillList[id].skillRef.skillElemental;
                switch (skillElemental)
                {
                    case "yin":
                        text += "A�m nhu";
                        break;
                    case "yang":
                        text += "D��ng c��ng";
                        break;
                    case "taichi":
                        text += "Tha�i c��c";
                        break;
                }
                text += "\n<b>Sa�t th��ng: </b>";
                skillDamageType = MapController.Instance.prizeHolder.prizeSkillList[id].skillRef.skillType;
                switch (skillDamageType)
                {
                    case "Internal":
                        text += "No�i co�ng";
                        break;
                    case "External":
                        text += "Ngoa�i co�ng";
                        break;
                }
            }
            text += "\n";
        }
        if (LocalizationSettings.SelectedLocale.Equals(LocalizationSettings.AvailableLocales.GetLocale("en")))
            text += "Cost 20 money per active or reroll";
        else
            text += "To�n 20 tie�n cho mo�i la�n k�ch hoa�t hoa�c reroll";
        TooltipController.Instance.ShowTooltip(text,350f, 0,250);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipController.Instance.HideTooltip();

    }
}
