using Gadgeteer.Interfaces;

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

        private readonly InterruptInput m_ButtonInput;
        private readonly DigitalOutput m_Led1Port;
        private readonly DigitalOutput m_Led2Port;
        private bool m_Led1Status;
        private bool m_Led2Status;
        private ButtonEventHandler m_OnButton;

        /// <summary />
        /// <param name="socketNumber">The socket that this module is plugged in to.</param>
        public Button(int socketNumber)
        {
            Socket socket = Socket.GetSocket(socketNumber, true, this, null);
            m_ButtonInput = new InterruptInput(socket, Socket.Pin.Three, GlitchFilterMode.Off, ResistorMode.Disabled,
                InterruptMode.RisingAndFallingEdge, this);
            m_ButtonInput.Interrupt += ButtonInput_Interrupt;
            m_Led1Port = new DigitalOutput(socket, Socket.Pin.Four, false, this);
            m_Led2Port = new DigitalOutput(socket, Socket.Pin.Five, false, this);
            m_Led1Status = false;
            m_Led2Status = false;
        }

        /// <summary>
        ///     Gets a value that indicates whether the button of the Button is pressed.
        /// </summary>
        public bool IsPressed
        {
            get { return m_ButtonInput.Read(); }
        }

        public bool Led1Status
        {
            get { return m_Led1Status; }
            set
            {
                if (m_Led1Status == value)
                    return;
                m_Led1Status = value;
                m_Led1Port.Write(m_Led1Status);
            }
        }

        public bool Led2Status
        {
            get { return m_Led2Status; }
            set
            {
                if (m_Led2Status == value)
                    return;
                m_Led2Status = value;
                m_Led2Port.Write(m_Led2Status);
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
            if (m_OnButton == null)
                m_OnButton = OnButtonEvent;
            if (buttonState == ButtonState.Pressed)
            {
                if (!Program.CheckAndInvoke(ButtonPressed, m_OnButton, (object) sender, (object) buttonState))
                    return;
                ButtonPressed(sender, buttonState);
            }
            else
            {
                if (!Program.CheckAndInvoke(ButtonReleased, m_OnButton, (object) sender, (object) buttonState))
                    return;
                ButtonReleased(sender, buttonState);
            }
        }
    }
}