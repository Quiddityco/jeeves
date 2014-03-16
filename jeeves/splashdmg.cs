using System.Collections.Generic;
using VpNet;

namespace jeeves
{
    // [TestFixture]
    public class DamageCalculation
    {
        /// <summary>
        /// Detonates the specified weapon. Such as a remotely controlled weapons device.
        /// </summary>
        /// <param name="weapon">The weapon.</param>
        /// <param name="weaponPosition">The weapon position.</param>
        /// <param name="potentialVictims">The potential victims.</param>
        /// <returns></returns>
        public System.Collections.Generic.Dictionary<int, float> Detonate(Weapon weapon, Vector3 weaponPosition, IEnumerable<Avatar<Vector3>> potentialVictims)
        {
            var damageResults = new System.Collections.Generic.Dictionary<int, float>();
            foreach (var potentialVictim in potentialVictims)
            {
                var distance = Vector3.Distance(weaponPosition, potentialVictim.Position);
                if (distance <= weapon.Radius)
                {
                    damageResults.Add(
                        potentialVictim.Session,
                        weapon.MaxDamage - (weapon.MaxDamage * (distance - ((distance / weapon.Radius) * weapon.FalloffPercentage / 100))));

                }
            }
            return damageResults;
        }

        /// <summary>
        /// Throws the specified weapon, it is not wise to throw a weapon with a large damage radius if you have low skils you will be part of the damage results.
        /// </summary>
        /// <param name="weapon">The weapon.</param>
        /// <param name="attackerPosition">The attacker position.</param>
        /// <param name="targettedVictim">The targetted victim.</param>
        /// <param name="attackerSkillPercentage">The attacker skill percentage.</param>
        /// <param name="potentialVictims">The potential victims.</param>
        /// <returns></returns>
        public System.Collections.Generic.Dictionary<int, float> Throw(Weapon weapon, Avatar<Vector3> attackerPosition, Avatar<Vector3> targettedVictim, float attackerSkillPercentage, IEnumerable<Avatar<Vector3>> potentialVictims)
        {
            var damageResults = new System.Collections.Generic.Dictionary<int, float>();
            var throwingDistance = Vector3.Distance(targettedVictim.Position, attackerPosition.Position) * (attackerSkillPercentage / 100);
            var impact = FindPointAlongLine(attackerPosition.Position, targettedVictim.Position, throwingDistance);


            foreach (var potentialVictim in potentialVictims)
            {
                var distance = Vector3.Distance(impact, potentialVictim.Position);
                if (distance <= weapon.Radius)
                {
                    damageResults.Add(potentialVictim.Session, weapon.MaxDamage * (weapon.Radius - distance) / weapon.Radius);
                }
            }
            return damageResults;
        }

        /// <summary>
        /// Finds the point along line.
        /// </summary>
        /// <param name="v1">The v1.</param>
        /// <param name="v2">The v2.</param>
        /// <param name="d">The distance</param>
        /// <returns></returns>
        private Vector3 FindPointAlongLine(Vector3 v1, Vector3 v2, float d)
        {
            return new Vector3(v1.X + (v2.X - v1.X) * d, v1.Y + (v2.Y - v1.Y) * d, v1.Z + (v2.Z - v1.Z) * d);
        }

        /*  [Test]
        public void TestSplashDamage()
        {
            var avatars = new[]
                                  {
                                      new Avatar<Vector3>() {Position = new Vector3(0, 0, 0), Session = 0},
                                      new Avatar<Vector3>() {Position = new Vector3(0.5f, 0, 0.5f), Session = 1},
                                      new Avatar<Vector3>() {Position = new Vector3(1.0f, 0, 1.0f), Session = 2},
                                      new Avatar<Vector3>() {Position = new Vector3(1.5f, 0, 0), Session = 3}
                                  };

            // creates a grenade which you can throw 20 meters, and has a damage radius of 5 meters. with a falloff of 100% in the radius and a max damage of 100 points.
            var grenade = new Weapon("Grenade", 0.5f, 100, 100, 2f);
            // creates a remote controlled c4 bomb which you can't throw but place (0 meters), and has a damage radius of 20 meters. with a falloff of 100% in the radius and a max damage of 1000 points.
            var remoteControlledC4 = new Weapon("Remotely controlled C4", 2f, 1000 , 100, 0);
            // walk away 25 meters from GZ and detonate the c4 at GZ.
            avatars[0].Position = new Vector3(2.5f, 0, 0);
            var damage1 = Detonate(remoteControlledC4, new Vector3(0, 0, 0), avatars);
            // now 25 meters from GZ, throw a grenade using skillset 80% to avatar[3] who
            // 15 meters away from GZ, or 10 meters from your current location.
            
            // again, but at a skillset of 50%,  var damage2 = Throw(grenade, avatars[0], avatars[3], 80, avatars);will both damage you and the victim which is now 2 meters closer to you
            avatars[3].Position = new Vector3(1.7f, 0, 0);
            var damage3 = Throw(grenade, avatars[0], avatars[3], 50, avatars);

        }
         */

        public class Weapon
        {
            public string Name { get; set; }
            public float Radius { get; set; }
            public float MaxDamage { get; set; }
            public float FalloffPercentage { get; set; }
            public float Range { get; set; }

            public Weapon(string name, float radius, float maxDamage, float falloffPercentage, float range)
            {
                Name = name;
                Radius = radius;
                MaxDamage = maxDamage;
                FalloffPercentage = falloffPercentage;
                Range = range;
            }
        }
         
    }
        
}

