using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;
using System;
using System.Diagnostics;
    
namespace AbilityFactoryStatic
{  
    public abstract class Ability
    {
        public abstract string Name { get; }

        public abstract void Process(PlayerScript player);
    }

    public class DoubleShootAbility : Ability
    {
        public override string Name => "Double Shoot";

        public override void Process(PlayerScript player)
        {
            player.shootAmount += 1;
        }

    }

    public class SpawnSpeedAbility : Ability
    {
        public override string Name => "Spawn Speed";

        public override void Process(PlayerScript player)
        {
            player.spawnRate += player.spawnRate;
        }

    }

    public class FasterBulletsAbility : Ability
    {
        public override string Name => "Faster Bullets";

        public override void Process(PlayerScript player)
        {
            player.fireSpeed += player.fireSpeed/2;
        }

    }

    public class TripleShootAbility : Ability
    {
        public override string Name => "Triple Shoot";

        public override void Process(PlayerScript player)
        {
            player.isTripleShootActivated = true;
                  
        }

    }

    public class SpawnCopyAbility : Ability
    {
        public override string Name => "Spawn Copy";

        public override void Process(PlayerScript player)
        {
            player.SpawnCopy(player.isTripleShootActivated,
                             player.shootAmount, 
                             player.fireSpeed, 
                             player.spawnRate);
        }

    }

    public static class AbilityFactory
    {
        private static Dictionary<string, Type> abilitiesByName;
        private static bool isInitialized => abilitiesByName != null;

        private static void InitializeFactory()
        {
            if (isInitialized)
                return;

            var abilityTypes = Assembly.GetAssembly(typeof(Ability)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Ability)));

            abilitiesByName = new Dictionary<string, Type>();

            foreach(var type in abilityTypes)
            {
                var tempEffect = Activator.CreateInstance(type) as Ability;
                abilitiesByName.Add(tempEffect.Name, type);
            }
            
        }

        public static Ability GetAbility(string abilityType)
        {
            InitializeFactory();

            if(abilitiesByName.ContainsKey(abilityType))
            {
                Type type = abilitiesByName[abilityType];
                var ability = Activator.CreateInstance(type) as Ability;
                return ability;
            }

            return null;
        }

        internal static IEnumerable<string> GetAbilityNames()
        {
            UnityEngine.Debug.Log("Test");
            InitializeFactory();
            return abilitiesByName.Keys;
        }
    }

}
