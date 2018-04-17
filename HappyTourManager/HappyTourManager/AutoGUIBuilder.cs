using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HappyTourManager
{
    class AutoGUIBuilder
    {
        public AutoGUIBuilder(Panel parent, object content)
        {
            var stackPanel = new StackPanel();
            parent.Children.Add(stackPanel);
        }
    }
}
