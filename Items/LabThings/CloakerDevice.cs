using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.LabThings
{
	public class CloakerDevice : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cloaker Device");
			base.Tooltip.SetDefault("'Cloaks you from Girus's sight'\nStops Girus's minions and aircraft from finding you\nMight make Girus angry once she finds out");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(5, 2));
		}

		public override void SetDefaults()
		{
			base.item.UseSound = SoundID.Item1;
			base.item.useStyle = 2;
			base.item.useTurn = true;
			base.item.noUseGraphic = true;
			base.item.useAnimation = 17;
			base.item.useTime = 17;
			base.item.consumable = true;
			base.item.width = 24;
			base.item.height = 38;
			base.item.maxStack = 1;
			base.item.value = Item.buyPrice(0, 10, 0, 0);
			base.item.rare = 6;
			base.item.buffType = base.mod.BuffType("GirusCloakBuff");
			base.item.buffTime = 72000;
		}

		public override bool UseItem(Player player)
		{
			RedeWorld.girusCloaked = true;
			return true;
		}
	}
}
