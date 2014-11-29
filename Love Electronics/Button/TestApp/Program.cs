using Gadgeteer.Modules.LoveElectronics;
using Microsoft.SPOT;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;

namespace TestApp
{
    public partial class Program
    {
        private readonly Button button = new Button(4);

        // This method is run when the mainboard is powered up or reset.   
        private void ProgramStarted()
        {
            /*******************************************************************************************
            Modules added in the Program.gadgeteer designer view are used by typing 
            their name followed by a period, e.g.  button.  or  camera.
            
            Many modules generate useful events. Type +=<tab><tab> to add a handler to an event, e.g.:
                button.ButtonPressed +=<tab><tab>
            
            If you want to do something periodically, use a GT.Timer and handle its Tick event, e.g.:
                GT.Timer timer = new GT.Timer(1000); // every second (1000ms)
                timer.Tick +=<tab><tab>
                timer.Start();
            *******************************************************************************************/


            // Use Debug.Print to show messages in Visual Studio's "Output" window during debugging.
            Debug.Print("Program Started");

            button.ButtonPressed += button_ButtonPressed;
            button.ButtonReleased += button_ButtonReleased;
        }

        private void button_ButtonReleased(Button sender, Button.ButtonState state)
        {
            button.Led2Status = !button.Led2Status;
        }

        private void button_ButtonPressed(Button sender, Button.ButtonState state)
        {
            button.Led1Status = !button.Led1Status;
        }
    }
}