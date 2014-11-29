using Gadgeteer.Modules.SolderMonkey;
using Microsoft.SPOT;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;

namespace TestApp
{
    public partial class Program
    {
        private readonly GT.Timer _timer = new GT.Timer(500);
        private readonly AddaSlide addaSlide = new AddaSlide(4);

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

            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        private void _timer_Tick(GT.Timer timer)
        {
            Debug.Print("Slider Position: " + addaSlide.GetSliderPosition());
        }
    }
}