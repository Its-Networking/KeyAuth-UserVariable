using KeyAuth;
using Newtonsoft.Json;
using System;
using System.Threading;

namespace AutoVariable
{
    class Program
    {
        public static api KeyAuthApp = new api(                                                                                                                // This information is needed in order to connect your application to KeyAuth - can all be found on the dashboard of KeyAuth on Manage Applications Page
            name: "Your App name",
            ownerid: "Your ownerid",
            secret: "Your app secret",
            version: "Your app version"
        );

        public class UserData                                                                                                                                  // This class will be used to get the response/message from KeyAuth while making request(s)
        {
            public string response { get; set; } 
            public string message { get; set; }
        }

        static void Main(string[] args)
        {
            Console.Title = "KeyAuth Auto Create Variable";                                                                                                     // The title of the console

            Console.WriteLine("\n Attemtping To Initialize");                                                                                                   // Prints the string out
            KeyAuthApp.init();                                                                                                                                  // attempts to initialize KeyAuth

            if (!KeyAuthApp.response.success)                                                                                                                   // If KeyAuth failed to initialize then:
            {
                Console.WriteLine($"\n Error: {KeyAuthApp.response.message}");                                                                                  // Print the error(s)
                Thread.Sleep(1500);                                                                                                                             // Wait a given time
                Environment.Exit(0);                                                                                                                            // Quit the application
            }


                                                                                                                                                                /// <summary>
                                                                                                                                                                /// In this example we will get/set the variable while unauthenticated (not logged in)
                                                                                                                                                                /// If you would like to use this while authenticated please view https://github.com/keyauth for the examples


            var myVariable = KeyAuthApp.webhook("webhookIDForGetVar", "user=userName&var=VariableNameToRetrieve");                 // the string after var can be anything you'd like, 
                                                                                                                                                                // to get the webhook ID you need to create a new webhook, place "https://keyauth.win/api/seller/?sellerkey=sellerkeyhere&type=getvar&" in a new webhook, check if you'd like it to be authenticated or not, 
                                                                                                                                                                // for this example we are not authenticating so if you are following this then uncheck "authenticated" or else it won't work...
                                                                                                                                                                // then for VariableNameYouWantToRetrieve place the name of the variable you would like to retrieve. If you do not have a variable created yet that's fine! Just put a variable name you would like to create instead. 

            UserData userData = JsonConvert.DeserializeObject<UserData>(myVariable);                                                                            // We will reference the UserData Class we created on line 20, this way we can get the response/message from the request(s) we make
                                                                                                                                                                // We will be using Json to deserialize the response/message, in this case we want to deserialize "myVariable" on line 46
                                                                                                                                                              
            if (userData.message == "Variable not found for user")                                                                                              // We want to check if the variable is created already or not... the message inside the if statement is the response you will receive if the variable is not created
            {                                                                                                                                                   // 
                KeyAuthApp.webhook("webhookIDForSetVar", "user=userName&var=VariableNameToSet&data=VariableDataYouWantToSet");     // If the variable does not exist then we will attemp to create a new one... note this webhook is different than the one on line 46. This one creates a new var, the other one gathers var data
                                                                                                                                                                // for the webhook ID you'll want to add this link in a new webhook, "https://keyauth.win/api/seller/?sellerkey=sellerkeyhere&type=setvar&", make sure you uncheck authenticated if you are just testing/following along or don't want a login system
                                                                                                                                                                // for the VarDataYouWantToSet, just enter whatever data you want to set for that variable

                var newlyCreatedVar = KeyAuthApp.webhook("webhookIDForGetVar", "user=userName&var=VariableNameToRetrieve");        // we will then create another var to get the new response from the new request to create a variable, for this you want to put the webhookID for the RETRIEVEVAR (on line 46)
                                                                                                                                                                // then you want to enter the variable name that you want to retrieve... also on line 46

                UserData userData1 = JsonConvert.DeserializeObject<UserData>(newlyCreatedVar);                                                                  // we then reference the UserData class again because we need to get the response/message again for the new request
                                                                                                                                                                // just like with the other data, we will need to use Json to deserialize the response/message

                Console.WriteLine($"Newly Created Variable {userData1.response}");                                                                              // once we have the response deserialized we will print it out. This will display only the variable data.
                Thread.Sleep(5000);                                                                                                                             // wait a given period of time     (can remove)
                Environment.Exit(0);                                                                                                                            // close the application           (can remove)
            }
            else                                                                                                                                                // HOWEVER, if the variable already exists THEN....
            {
                Console.WriteLine($"Exisitng Variable {userData.response}");                                                                                    // We just want to print the response and don't need to worry about creating a var. 
                Thread.Sleep(5000);                                                                                                                             // wait a given period of time      (can remove)
                Environment.Exit(0);                                                                                                                            // close the application            (can remove)
            }
             
        }
    }                                                                                                                                                           //**NOTE** You do not need the spaces inbetween the code... I only did it to explain to you how to set it up and how it works...
                                                                                                                                                                // If this method is too much and you find another way let me know. I like to see what the community can do, plus it never hurts to learn more. 
                                                                                                                                                                // If you need any assistance with this or have any questions just let me know. Discord: IT'S Netᴡᴏʀᴋɪɴɢ#6912
}                                                                                                                                                               /// </summary>
