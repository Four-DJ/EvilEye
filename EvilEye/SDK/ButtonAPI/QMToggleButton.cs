using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EvilEye.SDK.ButtonAPI
{
    public class QMToggleButton
    {
        public Toggle toggleButton;

        public QMToggleButton(Transform parent, string text, string toolTip, Sprite Icon, Action<bool> action)
        {
            GameObject singleButton = UnityEngine.Object.Instantiate<GameObject>(Main.Instance.quickMenuStuff.quickMenu.transform.Find("Container/Window/QMParent/Menu_Settings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UI_Elements_Row_1/Button_ToggleQMInfo").gameObject, parent);
            singleButton.transform.parent = parent;
            singleButton.name = text + "_EVILEYE_ToggleButton";

            singleButton.transform.Find("Text_H4").gameObject.GetComponent<TextMeshProUGUI>().text = text;
            if (Icon != null)
                singleButton.transform.Find("Icon_On").GetComponent<Image>().sprite = Icon;
            singleButton.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>().field_Public_String_0 = toolTip;
            toggleButton = singleButton.GetComponent<Toggle>();
            toggleButton.onValueChanged = new Toggle.ToggleEvent();
            toggleButton.onValueChanged.AddListener(action);
            singleButton.SetActive(true);
        }
    }
}
