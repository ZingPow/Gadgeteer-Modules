using Gadgeteer.Interfaces;

namespace Gadgeteer.Modules.LoveElectronics
{
    /// <summary>
    ///     A LedArray module for Microsoft .NET Gadgeteer
    ///     Provides an array of 7 LEDs.
    /// </summary>
    public class LedArray : Module
    {
        private const int m_LedCount = 7;
        private readonly DigitalOutput[] m_LedPort;
        private readonly bool[] m_LedStatus;

        /// <summary>
        ///     An Led Array consisting of 7 LEDs that the user can use to indicate program state.
        /// </summary>
        /// <param name="socketNumber">The socket that this module is plugged in to.</param>
        public LedArray(int socketNumber)
        {
            Socket socket = Socket.GetSocket(socketNumber, true, this, null);
            m_LedStatus = new bool[7];
            m_LedPort = new DigitalOutput[7];
            for (int index = 0; index < 7; ++index)
            {
                m_LedPort[index] = new DigitalOutput(socket, (Socket.Pin) (index + 3), true, this);
                m_LedStatus[index] = false;
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
            get { return m_LedStatus[index]; }
            set
            {
                if (m_LedStatus[index] == value)
                    return;
                m_LedStatus[index] = value;
                m_LedPort[index].Write(!value);
            }
        }

        /// <summary>
        ///     Lights a number of LEDs on the module, from D1 to D7.
        /// </summary>
        /// <param name="count">The number of LEDs to light.</param>
        public void LightLeds(int count)
        {
            if (count > 7)
                count = 7;
            else if (count < 0)
                count = 0;
            for (int index = 0; index < 7; ++index)
                this[index] = index < count;
        }

        /// <summary>
        ///     Turns off any LEDs that may be on.
        /// </summary>
        public void Clear()
        {
            for (int index = 0; index < 7; ++index)
                this[index] = false;
        }
    }
}