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
    public class QMNestedButton
    {
        public Transform mainMenu;
        public Transform buttonMenu;

        public QMNestedButton(Transform perant, string name, Sprite icon)
        {
            GameObject menu = UnityEngine.Object.Instantiate<GameObject>(Main.Instance.quickMenuStuff.quickMenu.transform.Find("Container/Window/QMParent/Menu_DevTools").gameObject, Main.Instance.quickMenuStuff.quickMenu.transform.Find("Container/Window/QMParent"));
            menu.transform.parent = Main.Instance.quickMenuStuff.quickMenu.transform.Find("Container/Window/QMParent");
            menu.name = name + "_EVILEYE_Menu";

            mainMenu = menu.transform;
            buttonMenu = menu.transform.Find("Scrollrect/Viewport/VerticalLayoutGroup/Buttons");
            GameObject backButton = menu.transform.Find("Header_DevTools/LeftItemContainer/Button_Back").gameObject;
            menu.transform.Find("Header_DevTools/LeftItemContainer/Text_Title").GetComponent<TextMeshProUGUI>().text = name;
            backButton.SetActive(true);
            backButton.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
            backButton.GetComponent<Button>().onClick.AddListener(new Action(() =>
            {
                menu.SetActive(false);
                perant.parent.parent.parent.parent.gameObject.SetActive(true);
            }));

            for (int i = 0; i < buttonMenu.childCount; i++)
                GameObject.Destroy(buttonMenu.GetChild(i).gameObject);

            new QMSingleButton(perant, name, name, icon, delegate
            {
                perant.transform.parent.parent.parent.parent.gameObject.SetActive(false);
                menu.SetActive(true);
            });
        }

    }
}
