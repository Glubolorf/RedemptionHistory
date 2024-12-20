using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.DruidProjectiles.Stave;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class UkkosStave : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ukko's Stave");
			base.Tooltip.SetDefault("'Finnish him!'\nOnly usable after Moonlord has been defeated\nSummons a giant Lightning Bolt at cursor point\nRight-clicking will shoot out a Lightning Blast\n[c/ffc300:Legendary]");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 600;
			base.item.width = 76;
			base.item.height = 80;
			base.item.crit = 40;
			base.item.useTime = 25;
			base.item.useAnimation = 25;
			base.item.knockBack = 5f;
			base.item.value = Item.buyPrice(0, 20, 0, 0);
			base.item.rare = 8;
			base.item.UseSound = SoundID.Item43;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<UkkosLightning2>();
			base.item.shootSpeed = 0f;
			this.defaultShoot = ModContent.ProjectileType<UkkosLightning2>();
			this.singleShotStave = true;
			this.rightClickStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 76.2f;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 7;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse != 2)
			{
				base.item.damage = 600;
				base.item.useTime = 25;
				base.item.useAnimation = 25;
				base.item.UseSound = base.mod.GetLegacySoundSlot(2, "Sounds/Item/Zap2");
				base.item.shoot = ModContent.ProjectileType<UkkosLightning2>();
				base.item.shootSpeed = 10f;
			}
			else
			{
				base.item.damage = 2000;
				base.item.useTime = 40;
				base.item.useAnimation = 40;
				base.item.UseSound = SoundID.Item1;
				base.item.shoot = ModContent.ProjectileType<UkkosLightning>();
				base.item.shootSpeed = 0f;
			}
			return NPC.downedMoonlord;
		}

		protected override bool SpecialShootPattern(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.altFunctionUse == 2)
			{
				position = Main.MouseWorld;
			}
			return true;
		}
	}
}
