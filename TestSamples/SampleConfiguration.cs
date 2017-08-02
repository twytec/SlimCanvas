using System;
using System.Collections.Generic;
using System.Text;
using TestSamples.Scenarios;

namespace TestSamples
{
    public class SampleConfiguration
    {
        public List<SamplesModel> GetAllSamples()
        {
            List<SamplesModel> samples = new List<SamplesModel>
            {
                new SamplesModel() { Description = "Hello World", ClassType = typeof(Scenario1) },
                new SamplesModel() { Description = "Draw primitive", ClassType = typeof(Scenario2) },
                new SamplesModel() { Description = "Brush", ClassType = typeof(Scenario3) },
                new SamplesModel() { Description = "Image", ClassType = typeof(Scenario4) },
                new SamplesModel() { Description = "Animate", ClassType = typeof(Scenario5) },
                new SamplesModel() { Description = "Drag me", ClassType = typeof(Scenario6) },
                new SamplesModel() { Description = "Camera", ClassType = typeof(Scenario7) },
            };

            return samples;
        }
    }
}
