using System;
using Redemption.Projectiles.Hostile;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Melee
{
	public class GhostCutlass : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ghost Cutlass");
			base.Tooltip.SetDefault("'Yaarrrr! I be a dead man!'\nHitting an enemy has a chance to summon a friendly Sunken Parrot to fight for you");
		}

		public override void SetDefaults()
		{
			base.item.damage = 21;
			base.item.melee = true;
			base.item.width = 52;
			base.item.height = 70;
			base.item.useTime = 29;
			base.item.useAnimation = 29;
			base.item.useStyle = 1;
			base.item.knockBack = 5f;
			base.item.value = Item.buyPrice(0, 5, 50, 50);
			base.item.rare = 2;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.alpha = 100;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			if (Main.rand.Next(3) == 0)
			{
				Projectile.NewProjectile(player.position.X + Utils.NextFloat(Main.rand, (float)player.width), player.position.Y + Utils.NextFloat(Main.rand, (float)player.height), (float)(-4 + Main.rand.Next(0, 8)), (float)(-4 + Main.rand.Next(0, 8)), ModContent.ProjectileType<FSunkenParrot>(), base.item.damage, base.item.knockBack, player.whoAmI, 0f, 1f);
			}
		}
	}
}
