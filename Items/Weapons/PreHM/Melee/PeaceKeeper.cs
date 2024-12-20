using System;
using Redemption.Projectiles.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Melee
{
	public class PeaceKeeper : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Peacekeeper");
			base.Tooltip.SetDefault("'A spear used by the Generals of Anglon'\n[c/1c4dff:Rare]");
		}

		public override void SetDefaults()
		{
			base.item.damage = 19;
			base.item.useStyle = 5;
			base.item.useAnimation = 14;
			base.item.useTime = 18;
			base.item.shootSpeed = 3.7f;
			base.item.knockBack = 5.5f;
			base.item.width = 64;
			base.item.height = 64;
			base.item.scale = 1f;
			base.item.rare = 9;
			base.item.UseSound = SoundID.Item1;
			base.item.shoot = ModContent.ProjectileType<PeaceKeeperPro>();
			base.item.value = Item.buyPrice(0, 10, 0, 0);
			base.item.noMelee = true;
			base.item.noUseGraphic = true;
			base.item.melee = true;
			base.item.autoReuse = true;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 5;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(30, 300, false);
		}

		public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[base.item.shoot] < 1;
		}
	}
}
