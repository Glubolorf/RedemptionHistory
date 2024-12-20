using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class Midnight : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Midnight, Defiler of the Prince");
			base.Tooltip.SetDefault("'This axe is said to be forged out of stardust...'\nOnly usable after any Mech Boss has been defeated\n[c/aa00ff:Epic]");
		}

		public override void SetDefaults()
		{
			base.item.damage = 60;
			base.item.melee = true;
			base.item.width = 62;
			base.item.height = 62;
			base.item.useTime = 15;
			base.item.useAnimation = 15;
			base.item.useStyle = 1;
			base.item.knockBack = 5f;
			base.item.value = Item.buyPrice(0, 20, 0, 0);
			base.item.rare = 11;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<MidnightNebula>();
			base.item.shootSpeed = 10f;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 6;
		}

		public override bool CanUseItem(Player player)
		{
			return NPC.downedMechBossAny;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 perturbedSpeed = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(10f));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			return true;
		}
	}
}
