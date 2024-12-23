﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Dusts
{
	public class VlitchFlame : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.noGravity = true;
			dust.noLight = true;
			dust.scale = 1.5f;
		}

		public override bool Update(Dust dust)
		{
			dust.position += dust.velocity;
			dust.rotation += dust.velocity.X;
			dust.scale -= 0.1f;
			if (dust.scale < 0.5f)
			{
				dust.active = false;
			}
			else
			{
				float strength = dust.scale / 2f;
				Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), (float)dust.color.R / 255f * 0.5f * strength, (float)dust.color.G / 255f * 0.5f * strength, (float)dust.color.B / 255f * 0.5f * strength);
			}
			return false;
		}
	}
}
