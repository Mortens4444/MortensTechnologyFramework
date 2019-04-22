using System;
using System.ComponentModel;

namespace Mtf.Controls.ComboBox
{
    public static class BasicExtensions
    {
        public static void SelectFirst(this System.Windows.Forms.ComboBox comboBox)
        {
            if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }

        public static void SelectFirstOrSetDisabled(this System.Windows.Forms.ComboBox comboBox)
        {
            if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
            else
            {
                comboBox.Enabled = false;
            }
        }

        public static int IndexOf(this System.Windows.Forms.ComboBox comboBox, object obj)
        {
            return comboBox.Items.IndexOf(obj);
        }

        /// <summary>
        /// Get all items from an enum type
        /// </summary>
        /// <param name="comboBox">The combobox to be filled</param>
        /// <param name="enumType">typeof(enum), enum::typeid</param>
        public static void GetItems(this System.Windows.Forms.ComboBox comboBox, Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new InvalidEnumArgumentException();
            }
            comboBox.DataSource = Enum.GetValues(enumType);
            comboBox.SelectFirst();
        }

        public static void GetItemsWithDescription(System.Windows.Forms.ComboBox comboBox, Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new InvalidEnumArgumentException();
            }
            var objects = Enum.GetValues(enumType);
            comboBox.DataSource = Enum.GetValues(enumType);
            for (var i = 0; i < objects.Length; i++)
            {
                comboBox.Items.Add(Utils.EnumExtensions.BaseExtensions.GetDescription(objects.GetValue(i)));
            }
            comboBox.SelectFirst();
        }
    }
}