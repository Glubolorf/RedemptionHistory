using System;
using Redemption.Buffs.Wasteland;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Usable.Potions
{
	public class Panacea : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Panacea Pill");
			base.Tooltip.SetDefault("Cures radiation poisoning instantly and grants complete immunity to it for 10 minutes\n[Cheat Item]");
		}

		public override void SetDefaults()
		{
			base.item.UseSound = SoundID.Item1;
			base.item.useStyle = 2;
			base.item.useTurn = true;
			base.item.useAnimation = 17;
			base.item.useTime = 17;
			base.item.consumable = true;
			base.item.width = 20;
			base.item.height = 26;
			base.item.maxStack = 30;
			base.item.value = Item.buyPrice(0, 15, 0, 0);
			base.item.rare = 11;
		}

		public override bool UseItem(Player player)
		{
			player.GetModPlayer<RedePlayer>().irradiatedEffect = 0;
			player.GetModPlayer<RedePlayer>().irradiatedLevel = 0;
			player.GetModPlayer<RedePlayer>().irradiatedTimer = 0;
			player.AddBuff(ModContent.BuffType<PanaceaBuff>(), 36000, true);
			return true;
		}
	}
}
