using Gadgeteer.SocketInterfaces;

namespace Gadgeteer.Modules.SolderMonkey
{
    /// <summary>
    ///     A TripleSlide module for Microsoft .NET Gadgeteer
    /// </summary>
    public class TripleSlide : Module
    {
        /// <summary />
        public enum sliderID
        {
            Slider1 = 1,
            Slider2 = 2,
            Slider3 = 3,
        }

        private readonly AnalogInput _slide1;
        private readonly AnalogInput _slide2;
        private readonly AnalogInput _slide3;

        /// <summary />
        /// <param name="socketNumber">The socket that this module is plugged in to.</param>
        public TripleSlide(int socketNumber)
        {
            Socket socket = Socket.GetSocket(socketNumber, true, this, null);
            _slide1 = AnalogInputFactory.Create(socket, Socket.Pin.Three, this);
            _slide2 = AnalogInputFactory.Create(socket, Socket.Pin.Four, this);
            _slide3 = AnalogInputFactory.Create(socket, Socket.Pin.Five, this);
        }

        /// <summary />
        /// <param name="ID" />
        /// <returns>
        ///     Returns the current position of the slide from 0.0 to 1.0
        /// </returns>
        public double ReadSliderPosition(sliderID ID)
        {
            switch (ID)
            {
                case sliderID.Slider1:
                    return _slide1.ReadProportion();
                case sliderID.Slider2:
                    return _slide2.ReadProportion();
                case sliderID.Slider3:
                    return _slide3.ReadProportion();
                default:
                    return 0.0;
            }
        }
    }
}