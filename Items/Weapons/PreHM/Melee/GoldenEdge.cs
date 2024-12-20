using System;
using Redemption.Buffs.Debuffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Melee
{
	public class GoldenEdge : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Golden Edge");
			base.Tooltip.SetDefault("'This sharp axe is made of solid gold and ruby...'\n[c/1c4dff:Rare]");
		}

		public override void SetDefaults()
		{
			base.item.damage = 33;
			base.item.melee = true;
			base.item.width = 52;
			base.item.height = 46;
			base.item.useTime = 8;
			base.item.axe = 20;
			base.item.useAnimation = 24;
			base.item.useStyle = 1;
			base.item.knockBack = 4f;
			base.item.value = Item.buyPrice(0, 10, 0, 0);
			base.item.rare = 9;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 5;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(ModContent.BuffType<LaceratedDebuff>(), 600, false);
		}
	}
}
