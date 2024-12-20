using System;
using Redemption.Buffs;
using Redemption.Buffs.Debuffs;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.PreHM
{
	public class BlackenedHeart : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Blackened Heart");
			base.Tooltip.SetDefault("'May cause instant death'");
			ItemID.Sets.ItemIconPulse[base.item.type] = true;
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(5, 5));
		}

		public override void SetDefaults()
		{
			base.item.width = 20;
			base.item.height = 18;
			base.item.maxStack = 99;
			base.item.value = 3000;
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item3;
			base.item.useStyle = 2;
			base.item.useTurn = true;
			base.item.useAnimation = 17;
			base.item.useTime = 17;
			base.item.consumable = true;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override bool UseItem(Player player)
		{
			int num = Main.rand.Next(2);
			if (num == 0)
			{
				base.item.buffType = ModContent.BuffType<BlackenedHeartBuff>();
				base.item.buffTime = 18000;
			}
			if (num == 1)
			{
				base.item.buffType = ModContent.BuffType<BlackenedHeartDebuff>();
				base.item.buffTime = 18000;
			}
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			return !player.HasBuff(ModContent.BuffType<BlackenedHeartBuff>()) && !player.HasBuff(ModContent.BuffType<BlackenedHeartDebuff>());
		}
	}
}
