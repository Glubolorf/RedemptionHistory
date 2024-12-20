using System;
using SubworldLibrary;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class EnterSoulless : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shadesoul Gateway");
			base.Tooltip.SetDefault("Sends all players to the Soulless Caverns");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 24;
			base.item.maxStack = 30;
			base.item.noUseGraphic = true;
			base.item.useAnimation = 45;
			base.item.useTime = 45;
			base.item.UseSound = SoundID.NPCDeath52;
			base.item.useStyle = 4;
			base.item.consumable = false;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override bool UseItem(Player player)
		{
			if (Redemption.soullessBiomeActive)
			{
				base.item.consumable = true;
				SLWorld.ExitSubworld();
			}
			else
			{
				SLWorld.EnterSubworld("Redemption_SoullessSub");
			}
			return true;
		}
	}
}
