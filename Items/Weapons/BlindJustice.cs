using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class BlindJustice : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Blind Justice, Demon's Terror");
			base.Tooltip.SetDefault("'Demons fear this weapon, for a good reason too...'\nDeal 2x more damage to Demons\nOnly usable after all Mech Bosses have been defeated\n[c/aa00ff:Epic]");
		}

		public override void SetDefaults()
		{
			base.item.damage = 95;
			base.item.melee = true;
			base.item.width = 82;
			base.item.height = 82;
			base.item.useTime = 22;
			base.item.useAnimation = 22;
			base.item.useStyle = 1;
			base.item.knockBack = 5f;
			base.item.value = Item.buyPrice(0, 20, 0, 0);
			base.item.rare = 11;
			base.item.UseSound = SoundID.Item71;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shootSpeed = 14f;
			base.item.shoot = base.mod.ProjectileType("SpectreScythe");
			base.item.GetGlobalItem<RedeItem>().redeRarity = 6;
		}

		public override bool CanUseItem(Player player)
		{
			return NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float numberProjectiles = 5f;
			float rotation = MathHelper.ToRadians(40f);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
			int i = 0;
			while ((float)i < numberProjectiles)
			{
				Vector2 perturbedSpeed = Utils.RotatedBy(new Vector2(speedX, speedY), (double)MathHelper.Lerp(-rotation, rotation, (float)i / (numberProjectiles - 1f)), default(Vector2)) * 0.2f;
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
				i++;
			}
			return false;
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(2) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 56, 0f, 0f, 0, default(Color), 1f);
			}
		}

		public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
		{
			if (target.type == 62)
			{
				damage *= 2;
			}
			if (target.type == 66)
			{
				damage *= 2;
			}
			if (target.type == 24)
			{
				damage *= 2;
			}
			if (target.type == 156)
			{
				damage *= 2;
			}
		}
	}
}
