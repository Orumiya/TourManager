using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HappyTourManager
{
    class AutoGUIBuilder
    {
        public AutoGUIBuilder(Panel parent, object content)
        {
            var stackPanel = new StackPanel();
            parent.Children.Add(stackPanel);
            BuildGUI(stackPanel, content);
        }

        private void BuildGUI(StackPanel stackPanel, object content)
        {
            Type t = content.GetType();
            foreach (var propInfo in t.GetProperties())
            {
                var editor = CreateEditor(propInfo, content);
                if (editor != null)
                {
                    var container = CreateContainer(propInfo, editor);
                    stackPanel.Children.Add(container);
                }


            }
        }

        private Panel CreateContainer(PropertyInfo propInfo, UIElement editor)
        {
            throw new NotImplementedException();
        }

        private UIElement CreateEditor(PropertyInfo propInfo, object content)
        {
            UIElement editor = null;

            if (propInfo.PropertyType == typeof(string) || propInfo.PropertyType == typeof(int))
            {
                TextBox txtBox = new TextBox();

                Binding binding = new Binding(propInfo.Name);
                binding.Source = content;

                if (propInfo.SetMethod == null || propInfo.SetMethod.IsPublic == false) 
                {
                    txtBox.IsEnabled = false;
                    binding.Mode = BindingMode.OneWay;
                }

                txtBox.SetBinding(TextBox.TextProperty, binding);

                editor = txtBox;
            }
            else if (propInfo.PropertyType == typeof(DateTime))
            {
                DatePicker datePicker = new DatePicker();

                Binding binding = new Binding(propInfo.Name);
                binding.Source = content;
                datePicker.SetBinding(DatePicker.SelectedDateProperty, binding);

                editor = datePicker;
            }

            return editor;
        }
    }
}
