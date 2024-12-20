using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class HeartOfInfection : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Heart of Infection");
			base.Tooltip.SetDefault("Summons up to 8 friendly Hive Growths\nAmount of Growths are doubled every 10 seconds");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(4, 6));
		}

		public override void SetDefaults()
		{
			base.item.CloneDefaults(669);
			base.item.width = 30;
			base.item.height = 36;
			base.item.value = Item.sellPrice(0, 8, 25, 0);
			base.item.expert = true;
			base.item.shoot = base.mod.ProjectileType("HiveGrowthFriendly");
			base.item.buffType = base.mod.BuffType("HiveGrowthBuff");
		}

		public override void UseStyle(Player player)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(base.item.buffType, 3600, true);
			}
		}
	}
}
