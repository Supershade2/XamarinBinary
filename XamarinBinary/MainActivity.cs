using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Telephony;
namespace XamarinBinary
{
    [Activity(Label = "XamarinBinary", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        //int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            string sms_message;
            //QuickContactBadge test;
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            SmsManager sms = SmsManager.Default;
            // Get our button from the layout resource,
            // and attach an event to it
            //string.Format("{0} clicks!", count++);
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            EditText phone_number = FindViewById<EditText>(Resource.Id.editText2);
            phone_number.Click += delegate { button.Text = "Click to Send"; };
            EditText message = FindViewById<EditText>(Resource.Id.editText1);
            message.Click += delegate { button.Text = "Click to Send"; };
            button.Click += delegate { if (phone_number.Text != "" && message.Text != "" || message.Text.Length < 20) { button.Text = "Sending..."; sms_message = ToBinary(message.Text); sms.SendMultipartTextMessage(phone_number.Text, null,sms.DivideMessage(sms_message), null, null); button.Text = "Press to Send message"; } else { button.Text = "Enter a valid phone number and message that is not over 20 characters"; } };
        }
        static string ToBinary(string message)
        {
            char[] message_parts = message.ToCharArray();
            string binary_message = "";
            string original_spaced = "";
            int bit = 128;
            for (int index = 0; index < message_parts.Length; index++)
            {
                int counter = 0;
                bit = 64;
                original_spaced += message_parts[index];
                int temp = (int)message_parts[index];
                if (message_parts[index] == ' ')
                {
                    //binary_message += binary_message.Length == 154 ? "\n":"";
                    //original_spaced += original_spaced.Length == 154 ? "\n" : "";
                    binary_message += " ";
                    original_spaced += " ";
                }
                else
                {
                    while (counter < 7)
                    {
                        if (temp >= bit)
                        {
                            binary_message += 1;
                            temp = (temp - bit);
                            bit = bit / 2;
                            //temp = temp / 2;
                        }
                        else
                        {
                            if (counter == 7 && temp == 1)
                            {
                                /*char[] reversed = binary_message.ToCharArray();
                                Array.Reverse(reversed);
                                binary_message = "";
                                for(int alength = 0; alength<reversed.Length; alength++)
                                {
                                    binary_message += reversed[alength];
                                }*/
                                binary_message += 1;
                                temp = 0;
                            }
                            else
                            {
                                binary_message += 0;
                                bit = bit / 2;
                            }
                            //temp = (temp - (temp / 2));
                            //temp = temp / 2;

                        }
                        counter++;
                        //temp = temp == 1 && counter == 8 ? 0 : temp;
                    }
                    binary_message += " ";
                    binary_message += binary_message.Length == 154 ? "\n" : "";
                    original_spaced += original_spaced.Length == 154 ? "\n" : "";
                }

            }
            binary_message += "\n" + original_spaced;
            //Console.WriteLine(binary_message);
            //string[] true_message = binary_message.Split('\n');
            return binary_message;
        }
    }
}

