using EvilEye.SDK.ButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EvilEye.Module
{
    abstract class BaseModule
    {
        public string name;
        public bool toggled;
        public bool save;

        string discription;
        QMNestedButton category;
        bool isToggle;
        Sprite image;

        public BaseModule(string name, string discription, QMNestedButton category, Sprite image = null, bool isToggle = false, bool save = false)
        {
            this.name = name;
            this.discription = discription;
            this.category = category;
            this.isToggle = isToggle;
            this.save = save;
            this.image = image;
        }

        public virtual void OnEnable()
        {

        }

        public virtual void OnDisable()
        {

        }

        public void OnUIInit()
        {
            if (isToggle)
            {
                QMToggleButton qMToggleButton = new QMToggleButton(category.menuTransform, name, discription, new Action<bool>((bool state) =>
                {
                    this.toggled = state;
                    if (state)
                    {
                        OnEnable();
                    }
                    else
                    {
                        OnDisable();
                    }
                }));
                if (Main.Instance.config.getConfigBool(name))
                {
                    qMToggleButton.toggleButton.Select();
                }
            }
            else
            {
                new QMSingleButton(category.menuTransform, name, discription, image, delegate
                {
                    OnEnable();
                });
            }
        }
    }
}
