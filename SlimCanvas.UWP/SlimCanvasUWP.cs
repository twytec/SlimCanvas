using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlimCanvas;
using SlimCanvas.Abstractions;
using SlimCanvas.View.Controls.EventTypes;
using SlimCanvas.View.Controls.EnumTypes;
using SlimCanvas.View;

namespace SlimCanvas.UWP
{
    /// <summary>
    /// Create new Canvas on UWP
    /// </summary>
    public class SlimCanvasUWP : Windows.UI.Xaml.Controls.SwapChainPanel
    {
        /// <summary>
        /// This is the portable Canvas
        /// </summary>
        public Canvas SlimCanvasPCL;

        /// <summary>
        /// Create new Canvas on UWP
        /// </summary>
        public SlimCanvasUWP()
        {
            var input = new IUserInput(this);

            SlimCanvasPCL = new Canvas(new DrawInCanvas(this), input, new IAssetsUwp(), new IGraphicsUwp());

            SetPlattformSettings();
            
        }

        #region SetPlattformSettings

        void SetPlattformSettings()
        {
            var device = Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily;

            if (device == "Windows.Mobile")
            {
                SlimCanvasPCL.Platform = Plattform.UniversalWindowsMobile;
            }
            else if (device == "Windows.Desktop")
            {
                SlimCanvasPCL.Platform = Plattform.UniversalWindowsDesktop;
            }
            else if (device == "Windows.Xbox")
            {
                SlimCanvasPCL.Platform = Plattform.UniversalWindowsXbox;
            }
            else if (device == "Windows.Holographic")
            {
                SlimCanvasPCL.Platform = Plattform.UniversalWindowsHolographic;
            }
            else if (device == "Windows.IoT")
            {
                SlimCanvasPCL.Platform = Plattform.UniversalWindowsIoT;
            }
            else if (device == "Windows.Team")
            {
                SlimCanvasPCL.Platform = Plattform.UniversalWindowsTeam;
            }
        }

        #endregion
    }
}
