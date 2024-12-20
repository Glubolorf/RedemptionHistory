using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Debuffs;
using Redemption.Items.Placeable.Banners;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Wasteland
{
	public class RadiumRampager : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Radium Rampager");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			this.dash = 0;
			this.dashSp = 6f;
			this.jump = 0;
			base.npc.width = 120;
			base.npc.height = 52;
			base.npc.damage = 90;
			base.npc.friendly = false;
			base.npc.defense = 30;
			base.npc.lifeMax = 1450;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.value = 200f;
			base.npc.knockBackResist = 0.1f;
			base.npc.aiStyle = 103;
			base.npc.noTileCollide = true;
			base.npc.noGravity = true;
			this.banner = base.npc.type;
			this.bannerItem = ModContent.ItemType<RadiumRampagerBanner>();
		}

		public override void AI()
		{
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 5.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 74;
				if (base.npc.frame.Y > 222)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			Vector2 delta = Main.player[base.npc.target].Center - base.npc.Center;
			if (delta.X > 0f)
			{
				base.npc.spriteDirection = 1;
			}
			else
			{
				base.npc.spriteDirection = -1;
			}
			if (Math.Abs(delta.X) > 300f)
			{
				this.dash = 1;
			}
			if (Math.Abs(delta.X) < 40f)
			{
				this.dash = 0;
			}
			if (delta.Y < -500f)
			{
				this.jump = 1;
			}
			if (delta.Y > 0f)
			{
				this.jump = 0;
			}
			if (this.dash == 1)
			{
				if (delta.X > 2f)
				{
					base.npc.velocity.X = this.dashSp;
					if (this.jump == 1)
					{
						base.npc.velocity.Y = -8f;
					}
				}
				if (delta.X < -2f)
				{
					base.npc.velocity.X = -this.dashSp;
					if (this.jump == 1)
					{
						base.npc.velocity.Y = -8f;
						return;
					}
				}
			}
			else if (this.jump == 1)
			{
				if (delta.X > 2f)
				{
					if (base.npc.velocity.X < 2f)
					{
						base.npc.velocity.X = 2f;
					}
					base.npc.velocity.Y = -8f;
				}
				if (delta.X < -2f)
				{
					if (base.npc.velocity.X > -2f)
					{
						base.npc.velocity.X = -2f;
					}
					base.npc.velocity.Y = -8f;
				}
			}
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/RadiumRampagerGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/RadiumRampagerGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/RadiumRampagerGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/RadiumRampagerGore4"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/XenomiteGore"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/XenomiteGore"), 1f);
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

		public int dash;

		public float dashSp = 6f;

		public int jump;
	}
}
