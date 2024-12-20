using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.v08
{
	public class NuclearSlime : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nuclear Slime");
			Main.npcFrameCount[base.npc.type] = 2;
		}

		public override void SetDefaults()
		{
			base.npc.width = 46;
			base.npc.height = 30;
			base.npc.friendly = false;
			base.npc.damage = 60;
			base.npc.defense = 0;
			base.npc.lifeMax = 600;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.value = 400f;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = 1;
			base.npc.netAlways = true;
			this.aiType = 138;
			this.animationType = 302;
			this.banner = base.npc.type;
			this.bannerItem = base.mod.ItemType("RadioactiveSlimeBanner");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				if (Config.NoCombatText)
				{
					CombatText.NewText(base.npc.getRect(), Color.Orange, "BOOM!", true, false);
				}
				for (int i = 0; i < 15; i++)
				{
					int num = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, base.mod.DustType("SludgeSpoonDust"), 0f, 0f, 100, default(Color), 3f);
					Main.dust[num].velocity *= 4.6f;
				}
				Main.PlaySound(SoundID.Item14, base.npc.position);
				if (Main.netMode != 1)
				{
					for (int j = 0; j < 15; j++)
					{
						Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)(-8 + Main.rand.Next(0, 17)), (float)(-3 + Main.rand.Next(-11, 0)), 686, 70, 3f, 255, 0f, 0f);
					}
				}
				for (int k = 0; k < 30; k++)
				{
					int num2 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 5f);
					Main.dust[num2].velocity *= 1.4f;
				}
				for (int l = 0; l < 40; l++)
				{
					int num3 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 6, 0f, 0f, 100, default(Color), 3f);
					Main.dust[num3].noGravity = true;
					Main.dust[num3].velocity *= 5f;
					num3 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 6, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num3].velocity *= 3f;
				}
				for (int m = 0; m < 2; m++)
				{
					int num4 = Gore.NewGore(new Vector2(base.npc.position.X + (float)(base.npc.width / 2) - 24f, base.npc.position.Y + (float)(base.npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[num4].scale = 1.5f;
					Main.gore[num4].velocity.X = Main.gore[num4].velocity.X + 1.5f;
					Main.gore[num4].velocity.Y = Main.gore[num4].velocity.Y + 1.5f;
					num4 = Gore.NewGore(new Vector2(base.npc.position.X + (float)(base.npc.width / 2) - 24f, base.npc.position.Y + (float)(base.npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[num4].scale = 1.5f;
					Main.gore[num4].velocity.X = Main.gore[num4].velocity.X - 1.5f;
					Main.gore[num4].velocity.Y = Main.gore[num4].velocity.Y + 1.5f;
					num4 = Gore.NewGore(new Vector2(base.npc.position.X + (float)(base.npc.width / 2) - 24f, base.npc.position.Y + (float)(base.npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[num4].scale = 1.5f;
					Main.gore[num4].velocity.X = Main.gore[num4].velocity.X + 1.5f;
					Main.gore[num4].velocity.Y = Main.gore[num4].velocity.Y - 1.5f;
					num4 = Gore.NewGore(new Vector2(base.npc.position.X + (float)(base.npc.width / 2) - 24f, base.npc.position.Y + (float)(base.npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[num4].scale = 1.5f;
					Main.gore[num4].velocity.X = Main.gore[num4].velocity.X - 1.5f;
					Main.gore[num4].velocity.Y = Main.gore[num4].velocity.Y - 1.5f;
				}
			}
			int num5 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, base.mod.DustType("SludgeSpoonDust"), 0f, 0f, 100, default(Color), 2f);
			Main.dust[num5].velocity *= 1.6f;
		}
	}
}
