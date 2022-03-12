using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.DotNET.Extensions;
using ModLibsCore.Services.Timers;
using PKEMeter.Logic;


namespace PKEMeter.Items {
	public partial class PKEMeterItem : ModItem {
		public static int MaxScanTicks { get; private set; } = 45;



		////////////////

		public static bool CanScanAt( int screenX, int screenY, out bool foundInInventory ) {
			bool myFoundInInventory = false;

			bool canScan = PKEScannable.Scannables?.Values
				.Any( data => data.CanScan(screenX, screenY, out myFoundInInventory) )
				?? false;

			foundInInventory = canScan && myFoundInInventory;

			return canScan;
		}


		////////////////

		public static void RunScanAt( int screenX, int screenY ) {
			var mymod = PKEMeterMod.Instance;

			var scanned = new List<string>();

			//

			foreach( (string name, PKEScannable scannable) in PKEScannable.Scannables ) {
				if( !scannable.CanScan(screenX, screenY, out _) ) {
					continue;
				}

				//

				float addPerc = 1f / (float)PKEMeterItem.MaxScanTicks;

				if( (scannable.ScanPercent + addPerc) >= 1f ) {
					scanned.Add( name );
				} else {
					scannable.ScanPercent += addPerc;
				}
			}
			
			//
			
			foreach( string scanName in scanned ) {
				PKEScannable.CompleteScan( scanName );
			}

			//

			if( mymod.PKEScanLoop.State != SoundState.Playing ) {
				mymod.PKEScanLoop.Volume = 0.2f;
				mymod.PKEScanLoop.IsLooped = true;
				mymod.PKEScanLoop.Play();
			}

			Timers.SetTimer( "PKEScanSoundLoopCutoff", 2, false, () => {
				mymod.PKEScanLoop.Stop();
				return false;
			} );

			//

			if( scanned.Count > 0 ) {
				if( mymod.PKEScanDone.State != SoundState.Playing ) {
					mymod.PKEScanDone.Volume = 0.2f;
					mymod.PKEScanDone.Play();
				}
			}
		}
	}
}