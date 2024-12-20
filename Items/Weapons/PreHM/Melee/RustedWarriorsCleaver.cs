using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Melee
{
	public class RustedWarriorsCleaver : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Rusted Warrior's Cleaver");
			base.Tooltip.SetDefault("'A rusted iron cleaver wielded by the Skeleton Warriors'");
		}

		public override void SetDefaults()
		{
			base.item.damage = 24;
			base.item.melee = true;
			base.item.width = 42;
			base.item.height = 46;
			base.item.useTime = 29;
			base.item.useAnimation = 29;
			base.item.useStyle = 1;
			base.item.knockBack = 5f;
			base.item.value = 1200;
			base.item.rare = -1;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = false;
		}
	}
}
