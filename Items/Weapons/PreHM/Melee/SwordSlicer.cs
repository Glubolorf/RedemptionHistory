using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Melee
{
	public class SwordSlicer : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Zephos' Sword Slicer");
			base.Tooltip.SetDefault("'A silver blade used to break opponent's weapons'\n[c/1c4dff:Rare]");
		}

		public override void SetDefaults()
		{
			base.item.damage = 26;
			base.item.melee = true;
			base.item.width = 46;
			base.item.height = 46;
			base.item.useTime = 19;
			base.item.useAnimation = 19;
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
			target.AddBuff(69, 1800, false);
		}
	}
}
