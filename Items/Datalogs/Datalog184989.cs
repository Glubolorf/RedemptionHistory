using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Datalogs
{
	public class Datalog184989 : ModItem
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Items/Datalogs/Datalog1";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Data Log #184989");
			base.Tooltip.SetDefault("It reads - [c/aee6f3:'On my journey to the nearest solar system, I decided to dabble with AI.]\n[c/aee6f3:I have set up blueprints for a simple android with the purpose of]\n[c/aee6f3:maintaining the SoS while I'm away. It's something for me to do so why not.]\n[c/aee6f3:It's estimated to take 770 years to reach the next solar system,]\n[c/aee6f3:and I haven't encountered another moving thing for 506 years.]\n[c/aee6f3:Having robots going about the SoS would be nice, and I'll be less lonely.']");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(5, 2));
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 30;
			base.item.maxStack = 1;
			base.item.value = 0;
			base.item.rare = 9;
		}
	}
}
