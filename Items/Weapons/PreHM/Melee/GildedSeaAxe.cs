using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Melee
{
	public class GildedSeaAxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Gilded Sea Axe");
			base.Tooltip.SetDefault("Enemies slain by this weapon drop more coins");
		}

		public override void SetDefaults()
		{
			base.item.damage = 19;
			base.item.melee = true;
			base.item.width = 52;
			base.item.height = 46;
			base.item.useTime = 9;
			base.item.axe = 7;
			base.item.useAnimation = 26;
			base.item.useStyle = 1;
			base.item.knockBack = 5f;
			base.item.value = Item.sellPrice(0, 1, 0, 0);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(72, 120, false);
		}
	}
}
