using System;
using Redemption.Projectiles.DruidProjectiles.Plants;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.SeedBags
{
	public class EyeStalkBag : DruidSeedBag
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Eye Stalk Seedbag");
			base.Tooltip.SetDefault("'Eye stalk with my little eye...'\nThrows a seed that grows into a creepy eye-covered plant\nThe eyes shed poisonous tears");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 21;
			base.item.width = 22;
			base.item.height = 26;
			base.item.useTime = 42;
			base.item.useAnimation = 42;
			base.item.useStyle = 1;
			base.item.mana = 6;
			base.item.crit = 4;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 3, 0, 0);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = false;
			base.item.shoot = ModContent.ProjectileType<Seed19>();
			base.item.shootSpeed = 18f;
			this.NativeTerrainIDs = TileLists.EvilTiles;
			this.nativeText = "Corruption/Crimson";
		}
	}
}
