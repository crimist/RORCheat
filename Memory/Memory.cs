using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Memory {
	// Use like WC.V for a wildcard in ushort[]
	public class WC { // WildCard
		public static ushort V = ushort.MaxValue; // Value
	}
	public class Mem { // TODO: make it use IntPtrs as they can be 64/32bit based on process
		public enum Type { // Mem types
			U8,
			U16,
			U32,
			U64,
			S8,
			S16,
			S32,
			S64,
			F,
			D
			//AOB?
		}

		private class Win32 {
			// DLLImport our memory manipulation and process functions from the kernel32.dll file
			[DllImport("kernel32.dll")]
			public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);
			[DllImport("kernel32.dll")]
			public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, ref int lpNumberOfBytesRead);
			[DllImport("kernel32.dll")]
			public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, ref int lpNumberOfBytesWritten);
		}

		public const ushort Wild = ushort.MaxValue; // Wildcard for AOB Scans

		// Global process and processhandle
		private static Process process;
		private static IntPtr processHandle;

		// Opens the process in this class with all permissions
		public static void Open(string processName) {
			Process[] proclist = Process.GetProcessesByName(processName);
			if (proclist.Length == 0) {
				throw new Exception("Couldn't find process");
			}

			process = proclist[0];
			processHandle = Win32.OpenProcess((0x000F0000 | 0x00100000 | 0xFFF), false, process.Id);
			if (processHandle == null) {
				throw new Exception("Coudln't Win32.OpenProcess()");
			}
			Debug.Write("Handle: " + processHandle.ToString("X"));
			return;
		}

		// Reads buffer into memory address
		private static void _Write(IntPtr address, byte[] buffer) {
			int bytesRead = 0;
			if (Win32.WriteProcessMemory(processHandle, address, buffer, buffer.Length, ref bytesRead) == false) {
				string error = new Win32Exception(Marshal.GetLastWin32Error()).Message;
				Debug.Write("Error (" + address.ToString("X") + "): " + error);
				throw new Exception("Error (" + address.ToString("X") + "): " + error);
			}
			return;
		}

		// Reads memory address into buffer
		private static void _Read(IntPtr address, byte[] buffer) {
			int bytesRead = 0;
			if (Win32.ReadProcessMemory(processHandle, address, buffer, buffer.Length, ref bytesRead) == false) {
				string error = new Win32Exception(Marshal.GetLastWin32Error()).Message;
				Debug.Write("Error (" + address.ToString("X") + "): " + error);
				throw new Exception("Error (" + address.ToString("X") + "): " + error);
			}
			return;
		}

		// Follows a cheatengine pointer until it gets the address
		// Call like Pointer32("Risk of Rain.exe+0xDEADC0DE", new int { 0xFF, 0xAA, 0x77 });
		public static IntPtr Pointer32(string Address, int[] Offsets) {
			IntPtr address = IntPtr.Zero;

			string[] tmp = Convert.ToString(Address).Split('+');
			foreach (ProcessModule M in process.Modules) {
				if (M.ModuleName.ToLower() == tmp[0].ToLower()) {
					Debug.Write("Base module " + M.ModuleName + ": " + M.BaseAddress.ToString("X"));
					if (IntPtr.Size == 8) { // 64
						address = (IntPtr)(M.BaseAddress.ToInt64() + long.Parse(tmp[1], NumberStyles.HexNumber));
					} else if (IntPtr.Size == 4) { // 32
						address = (IntPtr)(M.BaseAddress.ToInt32() + int.Parse(tmp[1], NumberStyles.HexNumber));
					}
				}
			}

			byte[] buff = new byte[4]; // 64 = new byte[8]

			_Read(address, buff);
			if (IntPtr.Size == 8) { // 64
				address = (IntPtr)BitConverter.ToInt64(buff, 0);
			} else if (IntPtr.Size == 4) { // 32
				address = (IntPtr)BitConverter.ToInt32(buff, 0);
			}

			for (int i = 0; i < Offsets.Length - 1; i++) {
				_Read(address + Offsets[i], buff);
				if (IntPtr.Size == 8) { // 64
					address = (IntPtr)BitConverter.ToInt64(buff, 0);
				} else if (IntPtr.Size == 4) { // 32
					address = (IntPtr)BitConverter.ToInt32(buff, 0);
				}
			}

			address = address + Offsets[Offsets.Length - 1];
			return address;
		}

		// Writes ASM to offset given where Offset is a module + offset and ASM is byte array
		// Call like this WriteASM("Risk of Rain.exe+109EB1", new byte[] { 0x90, 0x90 });
		public static byte[] WriteASM(object offset, byte[] asm) {
			IntPtr address = IntPtr.Zero;

			if (offset.GetType() == typeof(string)) {
				string[] tmp = Convert.ToString(offset).Split('+');

				foreach (ProcessModule M in process.Modules) {
					if (M.ModuleName.ToLower() == tmp[0].ToLower()) {
						if (IntPtr.Size == 8) { // 64
							address = (IntPtr)(M.BaseAddress.ToInt64() + long.Parse(tmp[1], NumberStyles.HexNumber));
						} else if (IntPtr.Size == 4) { // 32
							address = (IntPtr)(M.BaseAddress.ToInt32() + int.Parse(tmp[1], NumberStyles.HexNumber));
						}
					}
				}
			} else if (offset.GetType() == typeof(IntPtr)) {
				address = (IntPtr)offset;
			}

			if (address == IntPtr.Zero) {
				return null;
			}

			byte[] buffer = new byte[asm.Length]; // Make a buffer the size of what we are going to write
			_Read(address, buffer); // Save the old byte[]
			_Write(address, asm); // Write the new ASM
			return buffer;
		}

		// AOBScans for an array of bytes and returns the found address
		// BUG: AOBScan wont work if the process using it is 64 bit as it uses IntPtr
		public static IntPtr AOBScan(ushort[] pattern) {
			_AOBScan scan = new _AOBScan(process.Id);
			IntPtr address = scan.AobScan(pattern);
			return address;
		}

		// Not all my code so I just wrote a func to access it easily
		private class _AOBScan {
			protected int ProcessID;
			public _AOBScan(int ProcessID) {
				this.ProcessID = ProcessID;
			}

			[DllImport("kernel32.dll")]
			protected static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] buffer, uint size, int lpNumberOfBytesRead);
			[DllImport("kernel32.dll")]
			protected static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, int dwLength);

			[StructLayout(LayoutKind.Sequential)]
			protected struct MEMORY_BASIC_INFORMATION {
				public IntPtr BaseAddress;
				public IntPtr AllocationBase;
				public uint AllocationProtect;
				public uint RegionSize;
				public uint State;
				public uint Protect;
				public uint Type;
			}

			protected List<MEMORY_BASIC_INFORMATION> MemoryRegion { get; set; }

			protected void MemInfo(IntPtr pHandle) {
				IntPtr Addy = new IntPtr();
				while (true) {
					MEMORY_BASIC_INFORMATION MemInfo = new MEMORY_BASIC_INFORMATION();
					int MemDump = VirtualQueryEx(pHandle, Addy, out MemInfo, Marshal.SizeOf(MemInfo));
					if (MemDump == 0)
						break;
					if ((MemInfo.State & 0x1000) != 0 && (MemInfo.Protect & 0x100) == 0)
						MemoryRegion.Add(MemInfo);
					Addy = new IntPtr(MemInfo.BaseAddress.ToInt32() + (int)MemInfo.RegionSize);
				}
			}

			// V2
			protected IntPtr Scan(byte[] bytes, ushort[] pattern) {
				int end = bytes.Length;
				int match = 0;
				int matchEnd = pattern.Length;

				for (int i = 0; i < end; i++) {
					if (pattern[match] == Wild) {
						if (++match == matchEnd)
							return (IntPtr)i - matchEnd + 1;
						continue;
					}
					if (pattern[match] == bytes[i]) {
						if (++match == matchEnd)
							return (IntPtr)i - matchEnd + 1;
					} else {
						if (match == 0) {
							continue;
						} else {
							if (pattern[0] == bytes[i - match + 1]) {
								i = i - match + 1;
								match = 1;
							} else
								match = 0;
						}
					}
				}
				return IntPtr.Zero;
			}

			public IntPtr AobScan(ushort[] Pattern) {
				MemoryRegion = new List<MEMORY_BASIC_INFORMATION>();
				MemInfo(processHandle);
				for (int i = 0; i < MemoryRegion.Count; i++) {
					byte[] buff = new byte[MemoryRegion[i].RegionSize];
					ReadProcessMemory(processHandle, MemoryRegion[i].BaseAddress, buff, MemoryRegion[i].RegionSize, 0);

					IntPtr Result = Scan(buff, Pattern);
					if (Result != IntPtr.Zero)
						return new IntPtr(MemoryRegion[i].BaseAddress.ToInt32() + Result.ToInt32());
				}
				return IntPtr.Zero;
			}
		}

		// https://stackoverflow.com/questions/34798681/method-with-multiple-return-types

		public static void Write(IntPtr address, object value) { // Writes the value doesn't care about type
			if (value == null)
				throw new Exception("value == null");
			byte[] aob;
			//byte[] buffer = new byte[Marshal.SizeOf(value.GetType())]; // This may not work

			System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			using (System.IO.MemoryStream ms = new System.IO.MemoryStream()) {
				bf.Serialize(ms, value);
				aob = ms.ToArray();
			}

			_Write(address, aob);

			return;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static byte GetType(Type type) {
			switch (type) {
				case Type.U8:
					return sizeof(byte);
				case Type.U16:
					return sizeof(ushort);
				case Type.U32:
					return sizeof(uint);
				case Type.U64:
					return sizeof(ulong);
				case Type.S8:
					return sizeof(sbyte);
				case Type.S16:
					return sizeof(short);
				case Type.S32:
					return sizeof(int);
				case Type.S64:
					return sizeof(long);
				case Type.F:
					return sizeof(float);
				case Type.D:
					return sizeof(double);
				default:
					throw new Exception("What the fuck");
			}
		}

		/*public static void Read(IntPtr address, ref object x) { // Could try this...
			...
			if (x.TypeOf() == typeof(byte)
				return array[0];
			else if (x.TypeOf() == typeof(ushort)
				return BitConverter.ToUInt16(array, 0);
		}*/

		public static dynamic Read(IntPtr address, Type type) { // Read the type no matter what
			byte size = GetType(type);
			byte[] array = new byte[size];

			_Read(address, array);

			switch (type) {
				case Type.U8:
					return array[0];
				case Type.U16:
					return BitConverter.ToUInt16(array, 0);
				case Type.U32:
					return BitConverter.ToUInt32(array, 0);
				case Type.U64:
					return BitConverter.ToUInt64(array, 0);
				case Type.S8:
					return (sbyte)BitConverter.ToChar(array, 0);
				case Type.S16:
					return BitConverter.ToInt16(array, 0);
				case Type.S32:
					return BitConverter.ToInt32(array, 0);
				case Type.S64:
					return BitConverter.ToInt64(array, 0);
				case Type.F:
					return BitConverter.ToSingle(array, 0);
				case Type.D:
					return BitConverter.ToDouble(array, 0);
				default:
					throw new Exception("What the fuck");
			}
		}
	}
}
