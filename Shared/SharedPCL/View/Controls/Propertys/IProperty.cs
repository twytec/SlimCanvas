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
    public interface IProperty
    {
        /// <summary>
        /// to be added
        /// </summary>
        void SetValue(object value);

        /// <summary>
        /// to be added
        /// </summary>
        object GetValue();

        /// <summary>
        /// to be added
        /// </summary>
        event EventHandler PropertyChanged;
    }
}
