using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class OldRapier : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Old Rapier");
			base.Tooltip.SetDefault("'An old rapier that has nearly become blunt'");
		}

		public override void SetDefaults()
		{
			base.item.damage = 10;
			base.item.melee = true;
			base.item.width = 38;
			base.item.height = 40;
			base.item.useTime = 12;
			base.item.useAnimation = 12;
			base.item.useStyle = 3;
			base.item.knockBack = 3f;
			base.item.value = 750;
			base.item.rare = -1;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
		}
	}
}
