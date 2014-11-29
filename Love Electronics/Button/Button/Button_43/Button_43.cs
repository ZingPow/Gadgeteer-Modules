using Gadgeteer.SocketInterfaces;

namespace Gadgeteer.Modules.LoveElectronics
{
    /// <summary>
    ///     A Button module for Microsoft .NET Gadgeteer
    /// </summary>
    public class Button : Module
    {
        /// <summary>
        ///     Represents the delegate that is used to handle the
        ///     <see cref="E:Gadgeteer.Modules.LoveElectronics.Button.ButtonPressed" />
        ///     and <see cref="E:Gadgeteer.Modules.LoveElectronics.Button.ButtonReleased" /> events.
        /// </summary>
        /// <param name="sender">The <see cref="T:Gadgeteer.Modules.LoveElectronics.Button" /> object that raised the event.</param>
        /// <param name="state">The state of the button of the <see cref="T:Gadgeteer.Modules.LoveElectronics.Button" /></param>
        public delegate void ButtonEventHandler(Button sender, ButtonState state);

        /// <summary>
        ///     Represents the state of button of the <see cref="T:Gadgeteer.Modules.LoveElectronics.Button" />.
        /// </summary>
        public enum ButtonState
        {
            Released,
            Pressed,
        }

        private readonly InterruptInput _buttonInput;
        private readonly DigitalOutput _led1Port;
        private readonly DigitalOutput _led2Port;
        private bool _led1Status;
        private bool _led2Status;
        private ButtonEventHandler _onButton;

        /// <summary />
        /// <param name="socketNumber">The socket that this module is plugged in to.</param>
        public Button(int socketNumber)
        {
            Socket socket = Socket.GetSocket(socketNumber, true, this, null);
            _buttonInput = InterruptInputFactory.Create(socket, Socket.Pin.Three, GlitchFilterMode.Off,
                ResistorMode.Disabled, InterruptMode.RisingAndFallingEdge, this);
            _buttonInput.Interrupt += ButtonInput_Interrupt;
            _led1Port = DigitalOutputFactory.Create(socket, Socket.Pin.Four, false, this);
            _led2Port = DigitalOutputFactory.Create(socket, Socket.Pin.Five, false, this);
            _led1Status = false;
            _led2Status = false;
        }

        /// <summary>
        ///     Gets a value that indicates whether the button of the Button is pressed.
        /// </summary>
        public bool IsPressed
        {
            get { return _buttonInput.Read(); }
        }

        public bool Led1Status
        {
            get { return _led1Status; }
            set
            {
                if (_led1Status == value)
                    return;
                _led1Status = value;
                _led1Port.Write(_led1Status);
            }
        }

        public bool Led2Status
        {
            get { return _led2Status; }
            set
            {
                if (_led2Status == value)
                    return;
                _led2Status = value;
                _led2Port.Write(_led2Status);
            }
        }

        /// <summary>
        ///     Raised when the button of the <see cref="T:Gadgeteer.Modules.LoveElectronics.Button" /> is pressed.
        /// </summary>
        /// <remarks>
        ///     Implement this event handler and/or the <see cref="E:Gadgeteer.Modules.LoveElectronics.Button.ButtonReleased" />
        ///     event handler
        ///     when you want to provide an action associated with button events.
        ///     Since the state of the button is passed to the
        ///     <see cref="T:Gadgeteer.Modules.LoveElectronics.Button.ButtonEventHandler" /> delegate,
        ///     so you can use the same event handler for both button states.
        /// </remarks>
        public event ButtonEventHandler ButtonPressed;

        /// <summary>
        ///     Raised when the button of the <see cref="T:Gadgeteer.Modules.LoveElectronics.Button" /> is released.
        /// </summary>
        /// <remarks>
        ///     Implement this event handler and/or the <see cref="E:Gadgeteer.Modules.LoveElectronics.Button.ButtonPressed" />
        ///     event handler
        ///     when you want to provide an action associated with button events.
        ///     Since the state of the button is passed to the
        ///     <see cref="T:Gadgeteer.Modules.LoveElectronics.Button.ButtonEventHandler" /> delegate,
        ///     you can use the same event handler for both button states.
        /// </remarks>
        public event ButtonEventHandler ButtonReleased;

        private void ButtonInput_Interrupt(InterruptInput input, bool value)
        {
            OnButtonEvent(this, value ? ButtonState.Released : ButtonState.Pressed);
        }

        /// <summary>
        ///     Raises the <see cref="E:Gadgeteer.Modules.LoveElectronics.Button.ButtonPressed" /> or
        ///     <see cref="E:Gadgeteer.Modules.LoveElectronics.Button.ButtonReleased" /> event.
        /// </summary>
        /// <param name="sender">The <see cref="T:Gadgeteer.Modules.LoveElectronics.Button" /> that raised the event.</param>
        /// <param name="buttonState">The state of the button.</param>
        protected virtual void OnButtonEvent(Button sender, ButtonState buttonState)
        {
            if (_onButton == null)
                _onButton = OnButtonEvent;
            if (buttonState == ButtonState.Pressed)
            {
                if (!Program.CheckAndInvoke(ButtonPressed, _onButton, (object) sender, (object) buttonState))
                    return;
                ButtonPressed(sender, buttonState);
            }
            else
            {
                if (!Program.CheckAndInvoke(ButtonReleased, _onButton, (object) sender, (object) buttonState))
                    return;
                ButtonReleased(sender, buttonState);
            }
        }
    }
}