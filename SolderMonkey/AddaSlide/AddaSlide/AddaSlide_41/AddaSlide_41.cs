using Gadgeteer.Interfaces;

namespace Gadgeteer.Modules.SolderMonkey
{
    /// <summary>
    ///     A AddaSlide module for Microsoft .NET Gadgeteer
    /// </summary>
    public class AddaSlide : Module
    {
        private readonly DigitalOutput csPin;
        private readonly DigitalOutput dataClockPin;
        private readonly DigitalInput dataInPin;
        private readonly bool[] tmpValue = new bool[8];

        /// <summary />
        /// <param name="socketNumber">The socket that this module is plugged in to.</param>
        public AddaSlide(int socketNumber)
        {
            Socket socket = Socket.GetSocket(socketNumber, true, this, null);
            dataInPin = new DigitalInput(socket, Socket.Pin.Four, GlitchFilterMode.Off, ResistorMode.Disabled, null);
            csPin = new DigitalOutput(socket, Socket.Pin.Three, true, null);
            dataClockPin = new DigitalOutput(socket, Socket.Pin.Five, true, null);
        }

        /// <summary />
        /// <returns />
        public double GetSliderPosition()
        {
            byte num = 0;
            csPin.Write(false);
            dataClockPin.Write(false);
            dataClockPin.Write(true);
            dataInPin.Read();
            dataClockPin.Write(false);
            dataClockPin.Write(true);
            dataInPin.Read();
            dataClockPin.Write(false);
            dataClockPin.Write(true);
            dataInPin.Read();
            dataClockPin.Write(false);
            dataClockPin.Write(true);
            tmpValue[7] = dataInPin.Read();
            dataClockPin.Write(false);
            dataClockPin.Write(true);
            tmpValue[6] = dataInPin.Read();
            dataClockPin.Write(false);
            dataClockPin.Write(true);
            tmpValue[5] = dataInPin.Read();
            dataClockPin.Write(false);
            dataClockPin.Write(true);
            tmpValue[4] = dataInPin.Read();
            dataClockPin.Write(false);
            dataClockPin.Write(true);
            tmpValue[3] = dataInPin.Read();
            dataClockPin.Write(false);
            dataClockPin.Write(true);
            tmpValue[2] = dataInPin.Read();
            dataClockPin.Write(false);
            dataClockPin.Write(true);
            tmpValue[1] = dataInPin.Read();
            dataClockPin.Write(false);
            dataClockPin.Write(true);
            tmpValue[0] = dataInPin.Read();
            csPin.Write(true);
            for (int index = 0; index < 8; ++index)
            {
                if (tmpValue[index])
                    num |= (byte) (1 << index);
                else
                    num &= (byte) ~(1 << index);
            }
            if (num == 0)
                return num;
            return num/(double) byte.MaxValue;
        }
    }
}