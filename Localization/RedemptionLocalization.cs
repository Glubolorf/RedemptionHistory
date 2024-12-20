using System;
using Terraria.ModLoader;

namespace Redemption.Localization
{
	public class RedemptionLocalization
	{
		public static void Load()
		{
			RedemptionLocalization._localizations = new string[][]
			{
				new string[]
				{
					"DruidicOre",
					"Druidic energy courses through the world's ore..."
				}
			};
		}

		public static void Unload()
		{
			RedemptionLocalization._localizations = null;
		}

		public static void AddLocalizations()
		{
			RedemptionLocalization.Load();
			foreach (string[] array in RedemptionLocalization._localizations)
			{
				ModTranslation modTranslation = ModContent.GetInstance<Redemption>().CreateTranslation(array[0]);
				modTranslation.SetDefault(array[1]);
				ModContent.GetInstance<Redemption>().AddTranslation(modTranslation);
			}
			RedemptionLocalization.Unload();
		}

		private static string[][] _localizations;
	}
}
