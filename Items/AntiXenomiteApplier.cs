using System;
using Redemption.Buffs;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class AntiXenomiteApplier : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Anti-Crystallizer Serum");
			base.Tooltip.SetDefault("Makes you immune to Xenomite for a while\n'Label says 'Do not swallow.' Why would you do that?'");
		}

		public override void SetDefaults()
		{
			base.item.UseSound = SoundID.Item3;
			base.item.useStyle = 2;
			base.item.useTurn = true;
			base.item.useAnimation = 17;
			base.item.useTime = 17;
			base.item.maxStack = 30;
			base.item.consumable = true;
			base.item.width = 12;
			base.item.height = 38;
			base.item.value = 100;
			base.item.rare = 7;
			base.item.buffType = ModContent.BuffType<AntiXenomiteBuff>();
			base.item.buffTime = 500;
		}
	}
}
