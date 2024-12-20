using System;
using Microsoft.Xna.Framework;
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
		}

		public bool enjoyment;

		public bool ultraFlames;

		public bool druidBane;
	}
}
