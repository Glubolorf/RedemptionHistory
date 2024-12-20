using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Items;
using Redemption.Items.Placeable.Banners.v08;
using Redemption.NPCs.Bosses.InfectedEye;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.v08
{
	public class Injector : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Injector");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.width = 50;
			base.npc.height = 86;
			base.npc.damage = 75;
			base.npc.defense = 38;
			base.npc.lifeMax = 2300;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.value = 2000f;
			base.npc.knockBackResist = 0.35f;
			base.npc.aiStyle = 5;
			this.aiType = 6;
			base.npc.noGravity = true;
			this.banner = base.npc.type;
			this.bannerItem = ModContent.ItemType<InjectorBanner>();
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/InjectorGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/InjectorGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/XenomiteGore"), 1f);
				for (int i = 0; i < 5; i++)
				{
					int dustIndex2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex2].velocity *= 2.6f;
				}
			}
		}

		public override void NPCLoot()
		{
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<XenomiteShard>(), Main.rand.Next(2, 5), false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<Starlite>(), Main.rand.Next(1, 3), false, 0, false, false);
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<Bioweapon>(), 1, false, 0, false, false);
			}
			if (Main.rand.Next(100) == 0 || (Main.expertMode && Main.rand.Next(50) == 0))
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 892, 1, false, 0, false, false);
			}
		}

		public override void AI()
		{
			Player player = Main.player[base.npc.target];
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 10.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 52;
				if (base.npc.frame.Y > 156)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			BaseAI.LookAt((player != null && player.active && !player.dead) ? player.Center : (base.npc.Center + base.npc.velocity), base.npc, 0, 0f, 0.1f, false);
			if (base.npc.Distance(Main.player[base.npc.target].Center) <= 800f && Main.rand.Next(100) == 0)
			{
				float Speed = 10f;
				Vector2 vector8 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int damage = 36;
				int type = ModContent.ProjectileType<InfectedSpray>();
				float rotation = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
				int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0), type, damage, 0f, 0, 0f, 0f);
				Main.projectile[num54].netUpdate = true;
			}
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			if (Main.rand.Next(2) == 0 || (Main.expertMode && Main.rand.Next(0) == 0))
			{
				target.AddBuff(ModContent.BuffType<XenomiteDebuff>(), Main.rand.Next(500, 1000), true);
			}
			if (Main.rand.Next(9) == 0 || (Main.expertMode && Main.rand.Next(7) == 0))
			{
				target.AddBuff(ModContent.BuffType<XenomiteDebuff2>(), Main.rand.Next(250, 500), true);
			}
		}
	}
}
