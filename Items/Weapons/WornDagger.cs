using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class WornDagger : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Worn Dagger");
			base.Tooltip.SetDefault("'An old dagger that is on the edge of falling apart'");
		}

		public override void SetDefaults()
		{
			base.item.damage = 4;
			base.item.melee = true;
			base.item.width = 30;
			base.item.height = 30;
			base.item.useTime = 10;
			base.item.useAnimation = 10;
			base.item.useStyle = 3;
			base.item.knockBack = 3f;
			base.item.value = 100;
			base.item.rare = -1;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = false;
		}
	}
}
