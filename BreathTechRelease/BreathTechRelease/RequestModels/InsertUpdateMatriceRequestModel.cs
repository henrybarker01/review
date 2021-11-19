using System;
namespace BreathTechRelease.RequestModels
{
    public class InsertUpdateMatriceRequestModel
    {
        public string uid { get; set; }
        public string segmentName { get; set; }
        public string audioVideoName { get; set; }
        public bool isAudioVideo { get; set; }
    }
}
