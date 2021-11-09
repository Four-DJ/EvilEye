using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements;

namespace EvilEye.SDK.ButtonAPI
{
    public class QMNestedButton
    {
        public QMMenu menu;
        public Transform menuTransform;

        public QMNestedButton(Transform perant, string name, Sprite icon, string perantName = "")
        {
            menu = new QMMenu(name, name, perantName, false, true);
            menuTransform = menu.menuContents;

            new QMSingleButton(perant, name, name, icon, delegate
            {
                menu.OpenMenu();
            });
        }

    }
}
