using Gadgeteer.SocketInterfaces;

namespace Gadgeteer.Modules.SolderMonkey
{
    /// <summary>
    ///     A AddaSlide module for Microsoft .NET Gadgeteer
    /// </summary>
    public class AddaSlide : Module
    {
        private readonly DigitalOutput _csPin;
        private readonly DigitalOutput _dataClockPin;
        private readonly DigitalInput _dataInPin;
        private readonly bool[] _tmpValue = new bool[8];

        /// <summary />
        /// <param name="socketNumber">The socket that this module is plugged in to.</param>
        public AddaSlide(int socketNumber)
        {
            Socket socket = Socket.GetSocket(socketNumber, true, this, null);
            _dataInPin = DigitalInputFactory.Create(socket, Socket.Pin.Four, GlitchFilterMode.Off, ResistorMode.Disabled,
                null);
            _csPin = DigitalOutputFactory.Create(socket, Socket.Pin.Three, true, null);
            _dataClockPin = DigitalOutputFactory.Create(socket, Socket.Pin.Five, true, null);
        }

        /// <summary />
        /// <returns />
        public double GetSliderPosition()
        {
            byte num = 0;
            _csPin.Write(false);
            _dataClockPin.Write(false);
            _dataClockPin.Write(true);
            _dataInPin.Read();
            _dataClockPin.Write(false);
            _dataClockPin.Write(true);
            _dataInPin.Read();
            _dataClockPin.Write(false);
            _dataClockPin.Write(true);
            _dataInPin.Read();
            _dataClockPin.Write(false);
            _dataClockPin.Write(true);
            _tmpValue[7] = _dataInPin.Read();
            _dataClockPin.Write(false);
            _dataClockPin.Write(true);
            _tmpValue[6] = _dataInPin.Read();
            _dataClockPin.Write(false);
            _dataClockPin.Write(true);
            _tmpValue[5] = _dataInPin.Read();
            _dataClockPin.Write(false);
            _dataClockPin.Write(true);
            _tmpValue[4] = _dataInPin.Read();
            _dataClockPin.Write(false);
            _dataClockPin.Write(true);
            _tmpValue[3] = _dataInPin.Read();
            _dataClockPin.Write(false);
            _dataClockPin.Write(true);
            _tmpValue[2] = _dataInPin.Read();
            _dataClockPin.Write(false);
            _dataClockPin.Write(true);
            _tmpValue[1] = _dataInPin.Read();
            _dataClockPin.Write(false);
            _dataClockPin.Write(true);
            _tmpValue[0] = _dataInPin.Read();
            _csPin.Write(true);
            for (int index = 0; index < 8; ++index)
            {
                if (_tmpValue[index])
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