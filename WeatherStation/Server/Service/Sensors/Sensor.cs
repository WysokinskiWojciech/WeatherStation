using System.Device.I2c;

namespace WeatherStation.Server.Service.Sensors
{
    public class Sensor
    {
        public I2cDevice Initialize(byte i2cAddress)
        {
            return I2cDevice.Create(new I2cConnectionSettings(1, i2cAddress));
        }
    }
}
