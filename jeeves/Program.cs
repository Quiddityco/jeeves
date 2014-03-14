using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VpNet;

namespace jeeves
{

    public static class storage
    {
        public static List<Player> stats = new List<Player>();
    }



    class Program
    {

        static void Main(string[] args)
        {

            var vp = new Instance();
            vp.Connect();
            vp.Login("trooper", "", "jeeves");
            vp.Enter("Blizzard");
            Console.WriteLine("online");
            vp.UpdateAvatar();
            vp.UseAutoWaitTimer = true;
            vp.OnAvatarEnter += vp_OnAvatarEnter;
            vp.OnAvatarChange += vp_OnAvatarChange;
            vp.OnChatMessage += vp_OnChatMessage;




            while (true) // Loop indefinitely
            {
                Console.WriteLine("Enter input:"); // Prompt

                string line = Console.ReadLine(); // Get string from user

                if (line == "exit") // Check string
                {
                    break; // exit program
                }
                else if (line == "hello") // Check string
                {
                    //test command to show status and see if broken
                    Console.WriteLine("hello trooper");
                }
                else if (line == "edit terrain") // Check string
                {
                    //change terrain height to bottom of troopers feet
                    //feather out from trooper

                }
                else if (line.StartsWith("say"))
                {
                    vp.Say(line.Remove(0, 4));
                    Console.WriteLine(line);
                }
                else if (line == "start city")
                {
                    vp.OnAvatarClick += vp_OnAvatarClick;
                    vp.Say("everyone has been given guns");
                }
                else if (line == "list players")
                {
                    Console.WriteLine("\nCapacity: {0}", storage.stats.Capacity);
                    Console.WriteLine("Count: {0}", storage.stats.Count);
                }


                else
                {
                    Console.Write("You typed "); // Report output
                    Console.Write(line.Length); // length of line
                    Console.WriteLine(" characters and i didnt understand one");
                }
            }





        }

        static void vp_OnAvatarClick(Instance sender, AvatarClickEventArgsT<Avatar<Vector3>, Vector3> args)
        {

             var distance = Vector3.Distance(args.Avatar.Position, args.ClickedAvatar.Position);
             Console.WriteLine(distance);
            
            
            //storage.stats.Find(x => x._name.Contains("seat")));
            //storage.stats.Find(x => x._name == args.ClickedAvatar.Name); 
            for (int i = 0; i < storage.stats.Count; i++) // Loop through List with for
            {
                if (args.ClickedAvatar.Name == storage.stats[i]._name)
                {
                    if (storage.stats[i]._wep == "knife" && distance <= .5)
                    {
                        Console.WriteLine("knifed");
                        sender.ConsoleMessage(args.Avatar, "jeeves", string.Format("knifed {0}", args.ClickedAvatar.Name));
                        storage.stats[i]._health = storage.stats[i]._health - 5;

                        //sender.ConsoleMessage(args.Avatar, "jeeves", string.Format("{0} was stabbed by {1}", args.ClickedAvatar.Name, args.Avatar.Name)); //saw you saw trooper

                    }

                    else if (storage.stats[i]._wep == "handgun" && distance <= 5)
                    {
                        storage.stats[i]._ammo = storage.stats[i]._ammo - 1;
                        storage.stats[i]._health = storage.stats[i]._health - 5;
                        //sender.ConsoleMessage(args.Avatar, "jeeves", string.Format("{0} was shot by {1}", args.ClickedAvatar.Name, args.Avatar.Name)); //saw you saw trooper

                    }

                    else if (storage.stats[i]._wep == "sniper" && distance <= 15)
                    {
                        storage.stats[i]._ammo = storage.stats[i]._ammo - 1;
                        storage.stats[i]._health = storage.stats[i]._health - 5;
                        //sender.ConsoleMessage(args.Avatar, "jeeves", string.Format("{0} was sniped by {1}", args.ClickedAvatar.Name, args.Avatar.Name)); //saw you saw trooper

                    }

                    else if (storage.stats[i]._wep == "rocketlauncher" && distance <= 10)
                    {

                        //need help with splash code HELP!

                        storage.stats[i]._ammo = storage.stats[i]._ammo - 1;
                        storage.stats[i]._health = storage.stats[i]._health - 5;

                        // splash code
                        //sender.ConsoleMessage(args.Avatar, "jeeves", string.Format("{0} was blownup by {1}", args.ClickedAvatar.Name, args.Avatar.Name)); //saw you saw trooper

                    }
                    else
                    {
                        Console.WriteLine("failed");
                    }
                }
                




                if (storage.stats[i]._health <= 10) //if dead clause
                {

                    args.ClickedAvatar.Position = new Vector3(0, 1, 0);
                    args.ClickedAvatar.Rotation = new Vector3(0, 0, 0);
                    sender.TeleportAvatar(args.ClickedAvatar);
                    storage.stats[i]._health = 20;
                    storage.stats[i]._armor = 0;
                    storage.stats[i]._wep = "knife";
                    storage.stats[i]._ammo = 6;

                    //sender.ConsoleMessage(args.Avatar, "jeeves", string.Format("{0} was sent to hospital by {1}", args.ClickedAvatar.Name, args.Avatar.Name)); //saw you saw trooper
                }

            }


        }

        static void vp_OnChatMessage(Instance sender, ChatMessageEventArgsT<Avatar<Vector3>, ChatMessage, Vector3, Color> args)
        {
            Console.WriteLine("{0} : {1}", args.ChatMessage.Name, args.ChatMessage.Message); //displays chat on console window
        }


        static void vp_OnAvatarChange(Instance sender, AvatarChangeEventArgsT<Avatar<Vector3>, Vector3> args)
        {
            
            if (args.Avatar.Name == "trooper")
            {
                args.Avatar.Position = Vector3.UnitY + args.Avatar.Position; // follow trooper 1 meter above head
                sender.UpdateAvatar(args.Avatar.Position); //change position to follow trooper


                //Console.WriteLine("{0},{1}", args.Avatar.Name, args.Avatar.Position); //says where the avatar is and what its possition is
            }
        }
        static void vp_OnAvatarEnter(Instance sender, AvatarEnterEventArgsT<Avatar<Vector3>, Vector3> args)
        {
            //sender.ConsoleMessage(args.Avatar, "jeeves", string.Format("wecome {0}", args.Avatar.Name)); //saw you saw trooper
            Console.WriteLine("saw {0}", args.Avatar.Name); //list online users


            Player stats1 = new Player();
            Player stats = new Player();
            stats1._name = args.Avatar.Name;
            stats1._health = 20;
            stats1._wep = "knife";
            stats1._armor = 1;
            stats1._ammo = 6;

            storage.stats.Add(new Player() { _health = stats1._health, _name = stats1._name,_wep = stats1._wep,_ammo = stats1._ammo,_armor = stats1._armor }); //makes container for each variable

            Console.WriteLine("done");


        }
    }
}
