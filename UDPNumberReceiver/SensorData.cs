using System;
using System.Collections.Generic;
using System.Text;

namespace UDPNumberReceiver
{
   public class SensorData
    {
        public string dates { get; set; }

        public string CO { get; set; }

        public string NOx { get; set; }

        public string ParticleLevel { get; set; }

        public SensorData()
        {

        }


        public SensorData(string _dates, string _CO, string _NOx, string _ParticleLevel)
        {
            dates = _dates;
            CO = _CO;
            NOx = _NOx;
            ParticleLevel = _ParticleLevel;
        }

        public override string ToString()
        {
            return $"Date: {dates}, CO: {CO}, NOx: {NOx}, Particle Level:{ParticleLevel}.";
        }
    }
}
