using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Dusts
{
	public class XenoWaterDust : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.noGravity = true;
			dust.noLight = true;
			dust.scale = 0.01f;
			dust.frame = new Rectangle(0, 0, 24, 24);
			dust.velocity = new Vector2(Utils.NextFloat(Main.rand, -0.5f, 0.5f), 0f);
		}

		public override bool Update(Dust dust)
		{
			object customData;
			XenoWaterDust.Data data;
			if ((customData = dust.customData) is XenoWaterDust.Data)
			{
				data = (XenoWaterDust.Data)customData;
			}
			else
			{
				data = default(XenoWaterDust.Data);
			}
			data.time += 0.0125f;
			dust.alpha = (int)MathHelper.Lerp(160f, 255f, Math.Abs(data.time - 1f));
			dust.scale += 0.008333334f;
			dust.velocity.Y = dust.velocity.Y - 0.004166667f;
			dust.rotation += 0.008333334f * ((dust.velocity.X > 0f) ? 1f : -1f);
			dust.position += dust.velocity;
			if (dust.alpha >= 254)
			{
				dust.active = false;
			}
			dust.customData = data;
			return false;
		}

		public struct Data
		{
			public float time;
		}
	}
}
