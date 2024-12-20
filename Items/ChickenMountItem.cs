using System;
using Redemption.Projectiles.Mounts;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class ChickenMountItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Spiked Chickman Helm");
			base.Tooltip.SetDefault("Summons a rideable Chicken Cavalry");
		}

		public override void SetDefaults()
		{
			base.item.width = 14;
			base.item.height = 16;
			base.item.value = 900;
			base.item.rare = 1;
			base.item.useStyle = 4;
			base.item.useTime = 20;
			base.item.useAnimation = 20;
			base.item.noMelee = true;
			base.item.mountType = ModContent.MountType<ChickenMount>();
			base.item.UseSound = SoundID.Item25;
		}
	}
}
