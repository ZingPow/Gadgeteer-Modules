using Gadgeteer.SocketInterfaces;

namespace Gadgeteer.Modules.LoveElectronics
{
    /// <summary>
    ///     A LedArray module for Microsoft .NET Gadgeteer
    ///     Provides an array of 7 LEDs.
    /// </summary>
    public class LedArray : Module
    {
        private const int LEDCount = 7;
        private readonly DigitalOutput[] _ledPort;
        private readonly bool[] _ledStatus;

        /// <summary>
        ///     An Led Array consisting of 7 LEDs that the user can use to indicate program state.
        /// </summary>
        /// <param name="socketNumber">The socket that this module is plugged in to.</param>
        public LedArray(int socketNumber)
        {
            Socket socket = Socket.GetSocket(socketNumber, true, this, null);
            _ledStatus = new bool[LEDCount];
            _ledPort = new DigitalOutput[LEDCount];
            for (int index = 0; index < LEDCount; ++index)
            {
                _ledPort[index] = DigitalOutputFactory.Create(socket, (Socket.Pin) (index + 3), true, this);
                _ledStatus[index] = false;
            }
        }

        /// <summary>
        ///     Provides read and write access to each LED on the module.
        /// </summary>
        /// <param name="index">The index of the LED to access.</param>
        /// <returns>
        ///     The status of the LED.
        /// </returns>
        public bool this[int index]
        {
            get { return _ledStatus[index]; }
            set
            {
                if (_ledStatus[index] == value)
                    return;
                _ledStatus[index] = value;
                _ledPort[index].Write(!value);
            }
        }

        /// <summary>
        ///     Lights a number of LEDs on the module, from D1 to D7.
        /// </summary>
        /// <param name="count">The number of LEDs to light.</param>
        public void LightLeds(int count)
        {
            if (count > LEDCount)
                count = LEDCount;
            else if (count < 0)
                count = 0;
            for (int index = 0; index < LEDCount; ++index)
                this[index] = index < count;
        }

        /// <summary>
        ///     Turns off any LEDs that may be on.
        /// </summary>
        public void Clear()
        {
            for (int index = 0; index < LEDCount; ++index)
                this[index] = false;
        }
    }
}