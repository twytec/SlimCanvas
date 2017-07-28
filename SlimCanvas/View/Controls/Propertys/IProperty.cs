using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.Propertys
{
    public interface IProperty
    {
        void SetValue(object value);
        object GetValue();
        event EventHandler PropertyChanged;
    }
}
