using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class DruidProjectile : GlobalProjectile
	{
		public override bool InstancePerEntity
		{
			get
			{
				return true;
			}
		}

		public override bool CloneNewInstances
		{
			get
			{
				return true;
			}
		}

		public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			Player player = Main.player[projectile.owner];
			int critChance = player.HeldItem.crit;
			ItemLoader.GetWeaponCrit(player.HeldItem, player, ref critChance);
			PlayerHooks.GetWeaponCrit(player, player.HeldItem, ref critChance);
			if (critChance >= 100 || Main.rand.Next(1, 101) <= critChance)
			{
				crit = true;
			}
		}

		public override void AI(Projectile projectile)
		{
			Player player = Main.player[projectile.owner];
			if (this.fromStave && player.GetModPlayer<RedePlayer>().burnStaves && projectile.friendly && Main.rand.Next(10) == 0)
			{
				Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0f, 0f, 0, default(Color), 1f);
			}
			if (this.fromStave && player.GetModPlayer<RedePlayer>().moonStaves && projectile.friendly && Main.rand.Next(10) == 0)
			{
				Dust.NewDust(projectile.position, projectile.width, projectile.height, 229, 0f, 0f, 0, default(Color), 1f);
			}
			if (this.fromSeedbag && player.GetModPlayer<RedePlayer>().frostburnSeedbag && projectile.friendly && Main.rand.Next(10) == 0)
			{
				Dust.NewDust(projectile.position, projectile.width, projectile.height, 135, 0f, 0f, 0, default(Color), 1f);
			}
		}

		public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
		{
			Player player = Main.player[projectile.owner];
			if (this.fromStave && player.GetModPlayer<RedePlayer>().burnStaves)
			{
				target.AddBuff(24, 180, false);
			}
			if (this.fromStave && player.GetModPlayer<RedePlayer>().moonStaves)
			{
				target.AddBuff(ModContent.BuffType<MoonFireDebuff>(), 360, false);
			}
			if (this.fromSeedbag && player.GetModPlayer<RedePlayer>().frostburnSeedbag)
			{
				target.AddBuff(44, 180, false);
			}
		}

		public bool druidic;

		public bool fromStave;

		public bool fromSeedbag;

		public List<int> NativeTerrainIDs = new List<int>();

		public float seedLifetimeModifier = 1f;
	}
}
