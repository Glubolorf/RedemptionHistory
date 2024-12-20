using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Melee
{
	public class OldGathicWaraxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Old Gathic Waraxe");
			base.Tooltip.SetDefault("'An ancient waraxe wielded by the Iron Realm's Warriors of the Old Era'");
		}

		public override void SetDefaults()
		{
			base.item.damage = 32;
			base.item.melee = true;
			base.item.width = 60;
			base.item.height = 60;
			base.item.useTime = 33;
			base.item.useAnimation = 33;
			base.item.useStyle = 1;
			base.item.knockBack = 6f;
			base.item.value = 3200;
			base.item.rare = 3;
			base.item.UseSound = SoundID.Item7;
			base.item.autoReuse = true;
		}
	}
}
