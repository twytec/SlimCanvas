using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimCanvas.View.Controls.Propertys
{
    /// <summary>
    /// to be added
    /// </summary>
    public class BasicProperty : IProperty
    {
        BasicElement myElement;
        internal BasicProperty(BasicElement element)
        {
            myElement = element;
        }

        internal object myValue;

        /// <summary>
        /// to be added
        /// </summary>
        public event EventHandler PropertyChanged;

        /// <summary>
        /// to be added
        /// </summary>
        protected virtual void OnPropertyChanged(EventArgs e)
        {
            PropertyChanged?.Invoke(null, e);
        }
        internal void PropertyChangedTrigger()
        {
            OnPropertyChanged(null);
        }

        /// <summary>
        /// to be added
        /// </summary>
        public object GetValue()
        {
            return myValue;
        }

        /// <summary>
        /// to be added
        /// </summary>
        public void SetValue(object value)
        {
            myValue = value;

            PropertyChangedTrigger();

            if (Canvas.ViewDraw != null)
                Canvas.ViewDraw.UpdateView();
        }
    }
}
