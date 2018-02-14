using System;
using System.Threading;
using Memory;

namespace RiskOfRainCheat {
	public class Cheat {
		private class Offset {
			public static IntPtr Health;
			public static IntPtr MaxHealth;
			public static IntPtr AttackPower;
			public static IntPtr AttackSpeed;
			public static IntPtr Regen;
			public static IntPtr Defence;
			public static IntPtr JmpHeight;
			public static IntPtr JmpDouble;
			public static IntPtr RunSpeed;
			public static IntPtr SkillCoolDown;
			public static IntPtr ItemCoolDown;
			public static IntPtr Gold;

			public static void Init() {
				ushort[] PlayerAOB = {
					0xDE, 0xC0, 0xAD, 0xDE, 0x0B, 0xB0, 0xAD, 0xBA, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
					0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0x00,
					0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF,
					0xFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
					0xFF, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
					0x00, 0x00, 0xFF, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
					0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
					0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00,
					0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0x00, 0x00, 0x00,
					0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0x00,
					0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
					0x00, 0x00, 0x00, 0x00, 0x00, 0x00, WC.V, WC.V, WC.V, WC.V, 0x00, 0x00, 0x00, 0x00,
					0x00, 0x00, 0x00, 0x00, WC.V, WC.V, WC.V, WC.V, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
					0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
				};
				AttackPower = Mem.AOBScan(PlayerAOB) + 0xA0;
				Health = AttackPower + 0x30C; // 9999
				MaxHealth = AttackPower + 0x176C; // 9999
				AttackSpeed = AttackPower + 0xC20; // 2.499999523
				Regen = AttackPower + 0x1230;
				Defence = AttackPower + 0xC10;
				JmpHeight = AttackPower + 0xB40; // 2.375
				JmpDouble = AttackPower + 0xB30;
				RunSpeed = AttackPower + 0xAD0; // 2.125
				SkillCoolDown = AttackPower + 0x120C; // Set to 1 for unlimited
				ItemCoolDown = AttackPower + 0x1700; // Set to 1 for unlimited
				return;
			}
		}

		private class Original {
			public static double Health;
			public static double MaxHealth;
			public static float AttackPower;
			public static float AttackSpeed;
			//public static float Regen;
			//public static float Defence;
			public static float JmpHeight;
			public static float JmpDouble;
			public static float RunSpeed;
			public static double SkillCoolDown;
			public static double ItemCoolDown;
			public static byte[] Gold;
		}

		/* Prints to console with line and func for debug purposes */

		public static void Health(ref bool stop) {
			Debug.Write(Offset.Health.ToString("X"));
			Debug.Write(Offset.MaxHealth.ToString("X"));

			Original.Health = Mem.Read(Offset.Health, Mem.Type.D);
			Original.MaxHealth = Mem.Read(Offset.Health, Mem.Type.D);

			Mem.Write(Offset.MaxHealth, 9999D);

			while (true) {
				Mem.Write(Offset.Health, 9999D);
				Thread.Sleep(1000);

				if (stop == true) {
					Mem.Write(Offset.Health, Original.Health);
					Mem.Write(Offset.Health, Original.MaxHealth);
					return;
				}
			}
		}

		public static void AttackSpeed(bool isChecked) {
			Debug.Write(Offset.AttackSpeed.ToString("X"));
			if (isChecked) {
				Original.AttackSpeed = Mem.Read(Offset.AttackSpeed, Mem.Type.F);
				Mem.Write(Offset.AttackSpeed, 2.499999523F);
			}
			else if (!isChecked) {
				Mem.Write(Offset.AttackSpeed, Original.AttackSpeed);
			}
		}

		public static void AttackPower(bool isChecked) {
			Debug.Write(Offset.AttackPower.ToString("X"));
			if (isChecked) {
				Original.AttackPower = Mem.Read(Offset.AttackPower, Mem.Type.F);
				Mem.Write(Offset.AttackPower, 10F);
			} else if (!isChecked) {
				Mem.Write(Offset.AttackPower, Original.AttackPower);
			}
		}

		public static void JmpHeight(bool isChecked) {
			Debug.Write(Offset.JmpHeight.ToString("X"));
			if (isChecked) {
				Original.JmpHeight = Mem.Read(Offset.JmpHeight, Mem.Type.F);
				Mem.Write(Offset.JmpHeight, 2.375F);
			} else if (!isChecked) {
				Mem.Write(Offset.JmpHeight, Original.JmpHeight);
			}
		}
		public static void JmpDouble(bool isChecked) {
			Debug.Write(Offset.JmpDouble.ToString("X"));
			if (isChecked) {
				Original.JmpDouble = Mem.Read(Offset.JmpDouble, Mem.Type.F);
				Mem.Write(Offset.JmpDouble, 100F);
			} else if (!isChecked) {
				Mem.Write(Offset.JmpDouble, Original.JmpDouble);
			}
		}
		public static void RunSpeed(bool isChecked) {
			Debug.Write(Offset.RunSpeed.ToString("X"));
			if (isChecked) {
				Original.RunSpeed = Mem.Read(Offset.RunSpeed, Mem.Type.F);
				Mem.Write(Offset.RunSpeed, 2.125F);
			} else if (!isChecked) {
				Mem.Write(Offset.RunSpeed, Original.RunSpeed);
			}
		}
		public static void SkillCoolDown(bool isChecked) {
			Debug.Write(Offset.SkillCoolDown.ToString("X"));
			if (isChecked) {
				Original.SkillCoolDown = Mem.Read(Offset.SkillCoolDown, Mem.Type.D);
				Mem.Write(Offset.SkillCoolDown, 1D);
			} else if (!isChecked) {
				Mem.Write(Offset.SkillCoolDown, Original.SkillCoolDown);
			}
		}

		public static void ItemCoolDown(bool isChecked) {
			Debug.Write(Offset.ItemCoolDown.ToString("X"));
			if (isChecked) {
				Original.ItemCoolDown = Mem.Read(Offset.ItemCoolDown, Mem.Type.D);
				Mem.Write(Offset.ItemCoolDown, 1D);
			} else if (!isChecked) {
				Mem.Write(Offset.ItemCoolDown, Original.ItemCoolDown);
			}
		}

		public static void Gold(bool isChecked) { // Unlimited gold
			Debug.Write(Offset.Gold.ToString("X"));
			if (isChecked) {
				// AOBs
				ushort[] GoldAOB1 = {
					0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xF0, 0x3F, 0x00, 0x00, 0x00, 0x00,
					0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
					0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00,
					0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0x00,
					WC.V, WC.V, WC.V, WC.V, WC.V, WC.V, WC.V, WC.V, 0x00, 0x00, 0x00, 0x00,
					0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
					0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0x00
				};
				ushort[] GoldAOB2 = {
					0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x10, 0x40, 0x00, 0x00, 0x00, 0x00,
					0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
					0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00,
					0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0x00,
					WC.V, WC.V, WC.V, WC.V, WC.V, WC.V, WC.V, WC.V, 0x00, 0x00, 0x00, 0x00,
					0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
					0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0x00
				};
				Offset.Gold = Mem.AOBScan(GoldAOB1);
				if (Offset.Gold == IntPtr.Zero) {
					Offset.Gold = Mem.AOBScan(GoldAOB2);
					if (Offset.Gold == IntPtr.Zero)
						throw new Exception("I'm disabled");
				}
				Offset.Gold = Offset.Gold + 0x30;
				Original.Gold = Mem.WriteASM(Offset.Gold, new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xF0, 0x7F });
			} else if (!isChecked) {
				Mem.WriteASM(Offset.Gold, Original.Gold);
			}
		}

		/* Initiate the cheat by finding and hooking the process and filling variables */
		public static void Init() {
			Mem.Open("Risk of Rain");
			Offset.Init();
			return;
		}
	}
}