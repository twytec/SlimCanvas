using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SlimCanvasFormsTest
{
    public partial class MainPage : ContentPage
    {
        SlimCanvas.XF.SlimCanvasXF slim;
        SlimCanvas.Canvas slimCanvas;
        Picker pic;
        List<TestSamples.SamplesModel> samples;
        bool canLoaded = false;

        public MainPage()
        {
            InitializeComponent();

            rootGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(40, GridUnitType.Absolute) });
            rootGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });

            pic = new Picker()
            {
                HorizontalOptions = LayoutOptions.Center
            };
            TestSamples.SampleConfiguration config = new TestSamples.SampleConfiguration();
            samples = config.GetAllSamples();
            for (int i = 0; i < samples.Count; i++)
            {
                var s = samples[i];
                pic.Items.Add($"{i + 1} {s.Description}");
            }

            pic.SelectedIndexChanged += Pic_SelectedIndexChanged;
            Grid.SetRow(pic, 0);
            rootGrid.Children.Add(pic);

            slim = new SlimCanvas.XF.SlimCanvasXF();
            Grid.SetRow(slim, 1);
            rootGrid.Children.Add(slim);
            slim.CanvasLoaded += Xf_CanvasLoaded;
        }

        private void Pic_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (canLoaded)
            {
                var si = pic.SelectedIndex;
                var model = samples[si];
                ScenarioLoad(model);
            }
        }

        private void Xf_CanvasLoaded(SlimCanvas.Canvas slimCanvasPCL)
        {
            canLoaded = true;
            slimCanvas = slimCanvasPCL;

            pic.SelectedIndex = 0;
        }

        void ScenarioLoad(TestSamples.SamplesModel model)
        {
            var init = (TestSamples.ISamples)Activator.CreateInstance(model.ClassType);
            init.LoadScenario(slimCanvas);
        }
    }
}
