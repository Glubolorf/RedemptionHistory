using System;
using Terraria.ModLoader;

namespace Redemption.Items.Lore
{
	public class SeaNote : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Sunken Message");
			base.Tooltip.SetDefault("It reads - [c/d8f5ea:'Waiting by the sea,]\n[c/d8f5ea:When the moon is at its brightest,]\n[c/d8f5ea:The reflection of the waves,]\n[c/d8f5ea:Distorted my surroundings.]\n[c/d8f5ea:Fog started to form, thick and blinding,]\n[c/d8f5ea:Beware the one they call, the Sunken Captain']");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 22;
			base.item.maxStack = 999;
			base.item.value = 0;
			base.item.rare = -1;
		}
	}
}
