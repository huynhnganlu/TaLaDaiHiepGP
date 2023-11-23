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
                text = "<b>Caáp ñoä: </b>";
                if (MyCharacterController.Instance.levelDictionary[index] == 5)
                    text += "Toái ña\n<b>Tieàn caáp ñoä tieáp theo:</b> Toái ña\n";
                else
                    text += MyCharacterController.Instance.levelDictionary[index] + "\n<b>Tieàn caáp ñoä tieáp theo:</b> " + (MyCharacterController.Instance.levelDictionary[index] * 3 * 60) + "\n";
                text += "<b>Thuoäc tính: </b>";
                skillElemental = MapController.Instance.prizeHolder.prizeSkillList[id].skillRef.skillElemental;
                switch (skillElemental)
                {
                    case "yin":
                        text += "AÂm nhu";
                        break;
                    case "yang":
                        text += "Döông cöông";
                        break;
                    case "taichi":
                        text += "Thaùi cöïc";
                        break;
                }
                text += "\n<b>Saùt thöông: </b>";
                skillDamageType = MapController.Instance.prizeHolder.prizeSkillList[id].skillRef.skillType;
                switch (skillDamageType)
                {
                    case "Internal":
                        text += "Noäi coâng";
                        break;
                    case "External":
                        text += "Ngoaïi coâng";
                        break;
                }
            }
            text += "\n";
        }
        if (LocalizationSettings.SelectedLocale.Equals(LocalizationSettings.AvailableLocales.GetLocale("en")))
            text += "Cost 20 money per active or reroll";
        else
            text += "Toán 20 tieàn cho moãi laàn kích hoaït hoaëc reroll";
        TooltipController.Instance.ShowTooltip(text,350f, 0,250);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipController.Instance.HideTooltip();

    }
}
