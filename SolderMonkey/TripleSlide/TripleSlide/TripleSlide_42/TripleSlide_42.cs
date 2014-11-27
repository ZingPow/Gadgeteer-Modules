using Gadgeteer.Interfaces;

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
            slider1 = 1,
            slider2 = 2,
            slider3 = 3,
        }

        private readonly AnalogInput Slide1;
        private readonly AnalogInput Slide2;
        private readonly AnalogInput Slide3;

        /// <summary />
        /// <param name="socketNumber">The socket that this module is plugged in to.</param>
        public TripleSlide(int socketNumber)
        {
            Socket socket = Socket.GetSocket(socketNumber, true, this, null);
            Slide1 = new AnalogInput(socket, Socket.Pin.Three, this);
            Slide2 = new AnalogInput(socket, Socket.Pin.Four, this);
            Slide3 = new AnalogInput(socket, Socket.Pin.Five, this);
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
                case sliderID.slider1:
                    return Slide1.ReadProportion();
                case sliderID.slider2:
                    return Slide2.ReadProportion();
                case sliderID.slider3:
                    return Slide3.ReadProportion();
                default:
                    return 0.0;
            }
        }
    }
}