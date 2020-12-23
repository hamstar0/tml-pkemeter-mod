using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using HamstarHelpers.Classes.Loadable;


namespace PKEMeter.Logic {
	public delegate (float bluePercent, float greenPercent, float yellowPercent, float redPercent)
		PKEGauge( Player player, Vector2 position );




	partial class PKEMeterLogic : ILoadable {
		private void InitializeDefaultGauge() {
			if( this.CurrentGauge != null ) { return; }

			float b = 0, g = 0, y = 0, r = 0;
			int proxCheckTimer = 0;

			this.CurrentGauge = ( _, __ ) => {
				/*if( Main.rand.NextFloat() < (1f / 60f) ) {
					b = Main.rand.NextFloat();
					g = Main.rand.NextFloat();
					y = Main.rand.NextFloat();
					r = Main.rand.NextFloat();
				}*/

				// Report proximity to first found boss
				if( proxCheckTimer++ > 5 ) {
					proxCheckTimer = 0;
					NPC npc = Main.npc.FirstOrDefault( n =>
						n?.active == true
						&& (n.boss || n.type == NPCID.EaterofWorldsHead)
					);

					if( npc != null ) {
						r = (npc.Center - Main.LocalPlayer.Center).Length() / (160f * 16f);
						r = 1f - r;
						r = Math.Max( r, 0f );
					} else {
						r = 0f;
					}
				}

				return (b, g, y, r);
			};
		}


		////

		public (float b, float g, float y, float r) GetGauges( Player player, Vector2 position ) {
			this.GaugeSnapshot = this.CurrentGauge?.Invoke( player, position )
				?? (0f, 0f, 0f, 0f);
			return this.GaugeSnapshot;
		}
	}
}
