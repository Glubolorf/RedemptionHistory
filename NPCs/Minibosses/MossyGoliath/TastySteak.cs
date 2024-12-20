using System;
using Redemption.Items.DruidDamageClass.SeedBags;
using Terraria;
using Terraria.ID;

namespace Redemption.NPCs.Minibosses.MossyGoliath
{
	public class TastySteak : DruidSeedBag
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Tasty Steak");
			base.Tooltip.SetDefault("'Give your meat a good ol' rub'\nThrows a piece of meat on the ground to summon a Mossy Goliath at its location\nDeals more damage on mud or jungle grass");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 99;
			base.item.width = 24;
			base.item.height = 20;
			base.item.useTime = 30;
			base.item.useAnimation = 30;
			base.item.useStyle = 1;
			base.item.crit = 4;
			base.item.knockBack = 0f;
			base.item.value = Item.buyPrice(0, 1, 50, 0);
			base.item.rare = 3;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = false;
			base.item.shoot = base.mod.ProjectileType("TastySteakPro");
			base.item.shootSpeed = 4f;
			this.NativeTerrainIDs = TileLists.JungleTiles;
			this.nativeText = "Jungle";
		}

		public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[base.item.shoot] < 1;
		}
	}
}
