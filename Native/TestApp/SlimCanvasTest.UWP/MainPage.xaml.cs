using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x407 dokumentiert.

namespace SlimCanvasTest.UWP
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        SlimCanvas.Canvas slimCanvas;

        public MainPage()
        {
            this.InitializeComponent();

            SlimCanvas.UWP.SlimCanvasUWP canvas = new SlimCanvas.UWP.SlimCanvasUWP();
            rootGrid.Children.Add(canvas);
            slimCanvas = canvas.SlimCanvasPCL;

            ScenarioLoad(new TestSamples.SamplesModel() { ClassType = typeof(TestSamples.Scenarios.Scenario1) });

            LoadMenu();
        }

        List<TestSamples.SamplesModel> samples;

        void LoadMenu()
        {
            TestSamples.SampleConfiguration scenarios = new TestSamples.SampleConfiguration();
            samples = scenarios.GetAllSamples();

            int i = 1;
            foreach (var item in samples)
            {
                ComboBoxItem cbi = new ComboBoxItem()
                {
                    Content = $"{i} {item.Description}"
                };
                i++;
                cbScenarien.Items.Add(cbi);
            }

            cbScenarien.SelectedIndex = 0;
        }

        private void CbScenarien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = samples[cbScenarien.SelectedIndex];
            ScenarioLoad(item);
        }

        void ScenarioLoad(TestSamples.SamplesModel model)
        {
            var init = (TestSamples.ISamples)Activator.CreateInstance(model.ClassType);
            init.LoadScenario(slimCanvas);
        }
    }
}
