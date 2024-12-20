using System;
using System.IO;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;

namespace Redemption
{
	public static class Config
	{
		static Config()
		{
			Config.Load();
		}

		public static void Load()
		{
			if (!Config.ReadConfig())
			{
				ErrorLogger.Log("Failed to read the config file for Redemption! Recreating config...");
				Config.CreateConfig();
			}
		}

		private static bool ReadConfig()
		{
			if (Config.Configuration.Load())
			{
				Config.Configuration.Get<bool>("Classic Vlitch Cleaver sprite", ref Config.classicRedeVC);
				Config.Configuration.Get<bool>("Previous Infected Eye sprite", ref Config.classicRedeIE);
				Config.Configuration.Get<bool>("No combat text above npcs (e.g. enemies saying 'slash!' when they slash)", ref Config.NoCombatText);
				return true;
			}
			return false;
		}

		private static void CreateConfig()
		{
			Config.Configuration.Clear();
			Config.Configuration.Put("Classic Vlitch Cleaver sprite", Config.classicRedeVC);
			Config.Configuration.Put("Previous Infected Eye sprite", Config.classicRedeIE);
			Config.Configuration.Put("No combat text above npcs (e.g. enemies saying 'slash!' when they slash)", Config.NoCombatText);
			Config.Configuration.Save(true);
		}

		private static string filename = "RedemptionConfig.json";

		public static bool classicRedeVC = false;

		public static bool classicRedeIE = false;

		public static bool NoCombatText = false;

		private static string ConfigPath = Path.Combine(Main.SavePath, "Mod Configs", Config.filename);

		private static Preferences Configuration = new Preferences(Config.ConfigPath, false, false);
	}
}
