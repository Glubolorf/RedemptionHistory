﻿using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class RedeGlobalNPC : GlobalNPC
	{
		public override bool InstancePerEntity
		{
			get
			{
				return true;
			}
		}

		public override void ResetEffects(NPC npc)
		{
			this.enjoyment = false;
			this.ultraFlames = false;
			this.druidBane = false;
			this.holyFire = false;
			this.bInfection = false;
			this.needleStab = false;
			this.sleepPowder = false;
			this.vendetta = false;
		}

		public override void UpdateLifeRegen(NPC npc, ref int damage)
		{
			if (this.enjoyment)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 15;
				if (damage < 2)
				{
					damage = 2;
				}
			}
			if (this.ultraFlames)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 40;
				if (damage < 10)
				{
					damage = 10;
				}
			}
			if (this.druidBane)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 200;
				if (damage < 10)
				{
					damage = 10;
				}
				npc.defense -= 10;
			}
			if (this.holyFire)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 500;
				if (damage < 10)
				{
					damage = 10;
				}
			}
			if (this.bInfection)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 15;
				if (damage < 2)
				{
					damage = 2;
				}
			}
			if (this.needleStab)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				int num = 0;
				for (int i = 0; i < 1000; i++)
				{
					Projectile projectile = Main.projectile[i];
					if (projectile.active && projectile.type == base.mod.ProjectileType<NeedlePro>() && projectile.ai[0] == 1f && projectile.ai[1] == (float)npc.whoAmI)
					{
						num++;
					}
				}
				npc.lifeRegen -= num * 10 * 20;
				if (damage < num * 10)
				{
					damage = num * 10;
				}
			}
			if (this.sleepPowder)
			{
				if (!npc.boss)
				{
					npc.velocity.X = npc.velocity.X * 0.4f;
					npc.velocity.Y = npc.velocity.Y * 0.4f;
				}
				npc.defense -= 25;
			}
		}

		public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
		{
			if (this.vendetta)
			{
				npc.AddBuff(20, 200, false);
			}
		}

		public override bool CanHitPlayer(NPC npc, Player target, ref int cooldownSlot)
		{
			return (!Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).skeletonFriendly || (npc.type != 77 && npc.type != -49 && npc.type != -51 && npc.type != -53 && npc.type != -47 && npc.type != 449 && npc.type != 450 && npc.type != 451 && npc.type != 452 && npc.type != 566 && npc.type != 567 && npc.type != 481 && npc.type != 201 && npc.type != -15 && npc.type != 202 && npc.type != 203 && npc.type != 21 && npc.type != 324 && npc.type != 110 && npc.type != 323 && npc.type != 293 && npc.type != 291 && npc.type != 322 && npc.type != -48 && npc.type != -50 && npc.type != -52 && npc.type != -46 && npc.type != 292)) && base.CanHitPlayer(npc, target, ref cooldownSlot);
		}

		public override void DrawEffects(NPC npc, ref Color drawColor)
		{
			if (this.enjoyment && Main.rand.Next(4) < 3)
			{
				int num = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 243, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1.5f);
				Main.dust[num].noGravity = true;
				Main.dust[num].velocity *= 1.8f;
				Dust dust = Main.dust[num];
				dust.velocity.Y = dust.velocity.Y - 0.5f;
				if (Main.rand.Next(4) == 0)
				{
					Main.dust[num].noGravity = false;
					Main.dust[num].scale *= 0.5f;
				}
			}
			if (this.ultraFlames && Main.rand.Next(3) < 3)
			{
				int num2 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 92, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1.5f);
				Main.dust[num2].noGravity = true;
				Main.dust[num2].velocity *= 1.8f;
				Dust dust2 = Main.dust[num2];
				dust2.velocity.Y = dust2.velocity.Y - 0.5f;
				if (Main.rand.Next(4) == 0)
				{
					Main.dust[num2].noGravity = false;
					Main.dust[num2].scale *= 0.5f;
				}
			}
			if (this.druidBane && Main.rand.Next(3) < 3)
			{
				int num3 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 163, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1.5f);
				Main.dust[num3].noGravity = true;
				Main.dust[num3].velocity *= 1.8f;
				Dust dust3 = Main.dust[num3];
				dust3.velocity.Y = dust3.velocity.Y - 0.5f;
				if (Main.rand.Next(4) == 0)
				{
					Main.dust[num3].noGravity = false;
					Main.dust[num3].scale *= 0.5f;
				}
			}
			if (this.holyFire && Main.rand.Next(3) < 3)
			{
				int num4 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 64, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 2f);
				Main.dust[num4].noGravity = true;
				Main.dust[num4].velocity *= 1.8f;
				Dust dust4 = Main.dust[num4];
				dust4.velocity.Y = dust4.velocity.Y - 0.5f;
				if (Main.rand.Next(4) == 0)
				{
					Main.dust[num4].noGravity = false;
					Main.dust[num4].scale *= 0.5f;
				}
			}
		}

		public bool enjoyment;

		public bool ultraFlames;

		public bool druidBane;

		public bool holyFire;

		public bool bInfection;

		public bool needleStab;

		public bool sleepPowder;

		public bool vendetta;
	}
}
