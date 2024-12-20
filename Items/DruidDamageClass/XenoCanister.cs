using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Redemption.Items.DruidDamageClass
{
	public class XenoCanister : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenomite Canister");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nThrow a seed that grows a Xenomite Shard");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 6;
			base.item.width = 22;
			base.item.height = 26;
			base.item.useTime = 25;
			base.item.useAnimation = 25;
			base.item.useStyle = 1;
			base.item.mana = 6;
			base.item.crit = 4;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 1, 0, 0);
			base.item.rare = 2;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("Seed12");
			base.item.shootSpeed = 12f;
		}

		public override bool CanUseItem(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).fasterSeedbags)
			{
				base.item.useTime = 21;
				base.item.useAnimation = 21;
			}
			else
			{
				base.item.useTime = 25;
				base.item.useAnimation = 25;
			}
			return true;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).moreSeeds)
			{
				int num = 2 + Main.rand.Next(2);
				for (int i = 0; i < num; i++)
				{
					Vector2 vector = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(25f));
					float num2 = 1f - Utils.NextFloat(Main.rand) * 0.3f;
					vector *= num2;
					Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
				}
				return false;
			}
			return true;
		}
	}
}
