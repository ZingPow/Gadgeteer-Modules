using System.Threading;
using Gadgeteer.Modules.LoveElectronics;
using Microsoft.SPOT;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;

namespace TestApp
{
    public partial class Program
    {
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

            var ledArray = new LedArray(3);

            for (int j = 0; j < 10; j++)
            {
                for (int i = 1; i < 8; i++)
                {
                    ledArray.LightLeds(i);
                    Thread.Sleep(250);
                }

                for (int k = 7; k >= 0; k--)
                {
                    ledArray.LightLeds(k);
                    Thread.Sleep(250);
                }
            }

            ledArray.LightLeds(7);
            Thread.Sleep(500);
            ledArray.Clear();
            Thread.Sleep(500);
            ledArray.LightLeds(7);
        }
    }
}