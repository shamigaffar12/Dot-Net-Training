using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Assignment4
{
    //1. create a custom Event Arguments
    public class ButtonClickEventArgs : EventArgs
    {
        public string ClickBy { get; } //read only property

        public ButtonClickEventArgs(string ClickBy)
        {
            this.ClickBy = ClickBy;
        }
    }

    // create a class with an event

    public class Button
    {
        //create an event 
        public event EventHandler<ButtonClickEventArgs> ButtonClicked;

        //Method to stimulate the event
        public void Click(string user)
        {
            Console.WriteLine("Event Raised");
            OnButtonClicked(new ButtonClickEventArgs(user));
        }

        //Raise the event

        protected virtual void OnButtonClicked(ButtonClickEventArgs e)
        {
            ButtonClicked.Invoke(this, e);

        }
    }

    class Assignment4Q2
    {
        //event handler

        public static void Button_ButtonClicked(object sender, ButtonClickEventArgs e)
        {
            Console.WriteLine($"Button was clicked by : {e.ClickBy}");
        }
        public static void Main()
        {
            Button button = new Button();

            //subscribe to the event
            button.ButtonClicked += Button_ButtonClicked;

            button.Click("Shami");
            button.Click("Gaffar");
            Console.Read();

        }
    }
}

