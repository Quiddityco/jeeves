using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VpNet;


namespace jeeves
{

    public static class storage //storage for all variable and list
    {
        public static List<Player> stats = new List<Player>();
        public static string desc;
        public static string clicker;
        public static int p;
        public static int i;

    }



    class Program
    {

        static void Main(string[] args) // main sequence
        {

            var vp = new Instance();
            vp.Connect(); //connect to vp
            vp.Login("trooper", "wiggle11", "jeeves");
            vp.Enter("Blizzard");
            Console.WriteLine("CONNECTED");
            vp.UpdateAvatar();
            vp.UseAutoWaitTimer = true;
            vp.OnAvatarEnter += vp_OnAvatarEnter;
            vp.OnAvatarChange += vp_OnAvatarChange;
            vp.OnChatMessage += vp_OnChatMessage;
            vp.OnObjectClick += vp_OnObjectClick;
            vp.OnObjectGetCallback += vp_OnObjectGetCallback;
            vp.OnObjectChangeCallback += vp_OnObjectChangeCallback;




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
                else if (line.StartsWith("say")) // bot chat command
                {
                    vp.Say(line.Remove(0, 4));
                    Console.WriteLine(line);
                }
                else if (line == "start city") // starts the vp:gta code
                {
                    vp.OnAvatarClick += vp_OnAvatarClick;
                    vp.Say("everyone has been given guns");
                }
                else if (line == "list players")
                {
                    Console.WriteLine("\nCapacity: {0}", storage.stats.Capacity);
                    Console.WriteLine("Count: {0}", storage.stats.Count);
                }
                else if (line == "stats list")
                {
                    for (int c = 0; c < storage.stats.Count; c++) // Loop through List with for id of person
                    {
                        Console.WriteLine(storage.stats[c]._name);
                        Console.WriteLine(storage.stats[c]._wep);
                        Console.WriteLine(storage.stats[c]._health);
                        Console.WriteLine(storage.stats[c]._armor);
                        Console.WriteLine(storage.stats[c]._ammo);
                        Console.WriteLine(storage.stats[c]._grenadeammo);
                    }

                }

                else
                {
                    Console.Write("You typed "); // Report output
                    Console.Write(line.Length); // length of line
                    Console.WriteLine(" characters and i didnt understand one");
                }

                //vp.GetObject(1528956); 
 
            }
        }

        static void vp_OnObjectChangeCallback(Instance sender, ObjectChangeCallbackArgsT<RcDefault, VpObject<Vector3>, Vector3> args)
        {
            args.VpObject.Description = "boob";
        }


        static void vp_OnObjectGetCallback(Instance sender, ObjectGetCallbackArgsT<RcDefault, VpObject<Vector3>, Vector3> args)
        {
            Console.WriteLine(args.VpObject.Description);  // write description for object after the callback calls for the details
            storage.desc = args.VpObject.Description; //store description in desc
            //Console.WriteLine(args.VpObject.Action);



            for (int b = 0; b < storage.stats.Count; b++) // Loop through List with for id of person
            {
                if (storage.clicker == storage.stats[b]._name)
                {
                    if (storage.desc == "knife")
                    {
                        storage.stats[b]._wep = "knife";
                        Console.WriteLine("{0} was given {1}", storage.clicker, storage.stats[b]._wep);
                    }
                    else if (storage.desc == "rocket launcher")
                    {
                        storage.stats[b]._wep = "rocketlauncher";
                        Console.WriteLine("{0} was given {1}", storage.clicker, storage.stats[b]._wep);
                    }
                    else if (storage.desc == "sniper")
                    {
                        storage.stats[b]._wep = "sniper";
                        Console.WriteLine("{0} was given {1}", storage.clicker, storage.stats[b]._wep);
                    }
                    else if (storage.desc == "handgun")
                    {
                        storage.stats[b]._wep = "handgun";
                        Console.WriteLine("{0} was given {1}", storage.clicker, storage.stats[b]._wep);
                    }
                    else if (storage.desc == "grenade")
                    {
                        storage.stats[b]._wep = "grenade";
                        Console.WriteLine("{0} was given {1}", storage.clicker, storage.stats[b]._wep);
                    }
                    else if (storage.desc == "ammo")
                    {
                        storage.stats[b]._ammo = 6;
                        Console.WriteLine("{0} was given {1} ammo", storage.clicker, storage.stats[b]._ammo);
                    }
                    else if (storage.desc == "armor")
                    {
                        storage.stats[b]._armor = 5;
                        Console.WriteLine("{0} was given armor at level {1}", storage.clicker, storage.stats[b]._ammo);

                    }
                }
            }
        }
        static void vp_OnObjectClick(Instance sender, ObjectClickArgsT<Avatar<Vector3>, VpObject<Vector3>, Vector3> args)
        {

            Console.WriteLine("{0},{1}", args.Avatar.Name, args.VpObject.Id);
            sender.GetObject(args.VpObject.Id);
            storage.clicker = args.Avatar.Name;



        }
        
        static void vp_OnAvatarClick(Instance sender, AvatarClickEventArgsT<Avatar<Vector3>, Vector3> args)
        {
            if (args.ClickedAvatar.Name == null)
            {
                Console.WriteLine("TEESSTTT"); // fix this so that avatarclick no longer causes exception
            }
            var distance = Vector3.Distance(args.Avatar.Position, args.ClickedAvatar.Position); //calculate distnace between clicked and clickee

            // creates a grenade which you can throw 20 meters, and has a damage radius of 5 meters. with a falloff of 100% in the radius and a max damage of 100 points.
            var grenade = new DamageCalculation.Weapon("Grenade", 0.5f, 20, 10, 2f); //grenade,damage radious,0,dammagem,distance


            Console.WriteLine(distance);


            //storage.stats.Find(x => x._name.Contains("seat")));
            //storage.stats.Find(x => x._name == args.ClickedAvatar.Name); 
            for (storage.i = 0; storage.i < storage.stats.Count; storage.i++) // Loop through List with for the clicking avatar name
            {
                if (args.Avatar.Name == storage.stats[storage.i]._name)
                {
                    for (storage.p = 0; storage.p < storage.stats.Count; storage.p++) // Loop through List with for clicked avatar name
                    {
                        if (args.ClickedAvatar.Name == storage.stats[storage.p]._name)
                        {
                            if (storage.stats[storage.i]._wep == "knife" && distance <= .5)
                            {
                                storage.stats[storage.p]._health = storage.stats[storage.p]._health - 5;
                            }
                            else if (storage.stats[storage.i]._wep == "handgun" && distance <= 5)
                            {
                                storage.stats[storage.i]._ammo = storage.stats[storage.i]._ammo - 1;
                                storage.stats[storage.p]._health = storage.stats[storage.p]._health - 5;
                            }
                            else if (storage.stats[storage.i]._wep == "sniper" && distance <= 30)
                            {
                                storage.stats[storage.i]._ammo = storage.stats[storage.i]._ammo - 1;
                                storage.stats[storage.p]._health = storage.stats[storage.p]._health - 10;
                            }
                            else if (storage.stats[storage.i]._wep == "rocketlauncher" && distance <= 10)
                            {
                                //need help with splash code HELP!
                                storage.stats[storage.i]._ammo = storage.stats[storage.i]._ammo - 1;
                                storage.stats[storage.p]._health = storage.stats[storage.p]._health - 5;
                                // splash code
                            }
                            else if (storage.stats[storage.i]._wep == "grenade") // needs work HELP!
                            {
                                // = Throw(grenade, avatars[0], avatars[3], 80, avatars);
                                //  var damage2 = DamageCalculation.Throw(grenade, args.Avatar.Name  , args.ClickedAvatar.Name, 80, storage.stats);
                                storage.stats[storage.i]._ammo = storage.stats[storage.i]._grenadeammo - 1;
                                storage.stats[storage.p]._health = storage.stats[storage.p]._health - 5;
                                // splash code
                            }
                            else
                            {
                                Console.WriteLine("failed");
                            }


                            // code for what happend displaying who clicked who and what clicked what
                            sender.ConsoleMessage(args.ClickedAvatar, "jeeves", string.Format("you were attacked with a {0} by {1} you have {2} life left ", storage.stats[storage.i]._wep, args.Avatar.Name, storage.stats[storage.i]._health - 10));//say you were stabbed directly to victim
                            sender.ConsoleMessage(args.Avatar, "jeeves", string.Format("you attacked with a {0} and hit {1} he has {2} life left",storage.stats[storage.i]._wep, args.ClickedAvatar.Name, storage.stats[storage.p]._health - 10));//say you stabed viciton
                            Console.WriteLine("{0} stabed {1} health left {2}", args.Avatar.Name, args.ClickedAvatar.Name, storage.stats[storage.p]._health - 10);

                            if (storage.stats[storage.p]._health <= 10) //if dead clause
                            {

                                args.ClickedAvatar.Position = new Vector3(0, 1, 0);
                                args.ClickedAvatar.Rotation = new Vector3(0, 0, 0);
                                sender.TeleportAvatar(args.ClickedAvatar);

                                sender.ConsoleMessage(args.ClickedAvatar, "jeeves", string.Format("you were attacked with a {0} by {1} you have {2} life left ", storage.stats[storage.i]._wep, args.Avatar.Name, storage.stats[storage.i]._health - 10));//say you were stabbed directly to victim
                                Console.WriteLine("{0} died with health of {1}", args.ClickedAvatar.Name, storage.stats[storage.p]._health - 10);

                                storage.stats[storage.p]._health = 20;
                                storage.stats[storage.p]._armor = 0;
                                storage.stats[storage.p]._wep = "knife";
                                storage.stats[storage.p]._ammo = 0;
                                // code for when you die
                            }
                        }
                    }
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
            stats1._armor = 0;
            stats1._ammo = 0;
            stats1._grenadeammo = 0;

            storage.stats.Add(new Player() { _health = stats1._health, _name = stats1._name, _wep = stats1._wep, _ammo = stats1._ammo, _armor = stats1._armor, _grenadeammo = stats1._grenadeammo }); //makes container for each variable

            Console.WriteLine("done");


        }
    }
}