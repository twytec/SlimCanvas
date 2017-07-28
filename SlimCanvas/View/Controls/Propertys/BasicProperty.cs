using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.Propertys
{
    public class BasicProperty : IProperty
    {
        BasicElement myElement;
        internal BasicProperty(BasicElement element)
        {
            myElement = element;
        }

        internal object myValue;

        public event EventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(EventArgs e)
        {
            PropertyChanged?.Invoke(null, e);
        }
        internal void PropertyChangedTrigger()
        {
            OnPropertyChanged(null);
        }

        public object GetValue()
        {
            return myValue;
        }

        public void SetValue(object value)
        {
            myValue = value;

            PropertyChangedTrigger();

            if (Canvas.ViewDraw != null)
                Canvas.ViewDraw.UpdateView();
        }
    }
}
