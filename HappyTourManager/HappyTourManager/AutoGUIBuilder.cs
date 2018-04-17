using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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

        private Panel CreateContainer(PropertyInfo propInfo, object editor)
        {
            throw new NotImplementedException();
        }

        private UIElement CreateEditor(PropertyInfo propInfo, object content)
        {
            throw new NotImplementedException();
        }
    }
}
