using System;
using System.Collections.Generic;

namespace PmxLib;

public class VmdCamera : VmdFrameBase, IBytesConvert, ICloneable
{
	public float Distance;

	public Vector3 Position;

	public Vector3 Rotate;

	public VmdCameraIPL IPL = new VmdCameraIPL();

	public float Angle;

	public byte Pers;

	public int ByteCount => 32 + IPL.ByteCount + 1 + 4;

	public VmdCamera()
	{
	}

	public VmdCamera(VmdCamera camera)
	{
		FrameIndex = camera.FrameIndex;
		Distance = camera.Distance;
		Position = camera.Position;
		Rotate = camera.Rotate;
		IPL = (VmdCameraIPL)camera.IPL.Clone();
		Angle = camera.Angle;
		Pers = camera.Pers;
	}

	public byte[] ToBytes()
	{
		List<byte> list = new List<byte>();
		list.AddRange(BitConverter.GetBytes(FrameIndex));
		list.AddRange(BitConverter.GetBytes(Distance));
		list.AddRange(BitConverter.GetBytes(Position.x));
		list.AddRange(BitConverter.GetBytes(Position.y));
		list.AddRange(BitConverter.GetBytes(Position.z));
		list.AddRange(BitConverter.GetBytes(Rotate.x));
		list.AddRange(BitConverter.GetBytes(Rotate.y));
		list.AddRange(BitConverter.GetBytes(Rotate.z));
		list.AddRange(IPL.ToBytes());
		list.AddRange(BitConverter.GetBytes((int)Angle));
		list.Add(Pers);
		return list.ToArray();
	}

	public void FromBytes(byte[] bytes, int startIndex)
	{
		FrameIndex = BitConverter.ToInt32(bytes, startIndex);
		int num = startIndex + 4;
		Distance = BitConverter.ToSingle(bytes, num);
		int num2 = num + 4;
		Position.x = BitConverter.ToSingle(bytes, num2);
		int num3 = num2 + 4;
		Position.y = BitConverter.ToSingle(bytes, num3);
		int num4 = num3 + 4;
		Position.z = BitConverter.ToSingle(bytes, num4);
		int num5 = num4 + 4;
		Rotate.x = BitConverter.ToSingle(bytes, num5);
		int num6 = num5 + 4;
		Rotate.y = BitConverter.ToSingle(bytes, num6);
		int num7 = num6 + 4;
		Rotate.z = BitConverter.ToSingle(bytes, num7);
		int num8 = num7 + 4;
		IPL.FromBytes(bytes, num8);
		int num9 = num8 + IPL.ByteCount;
		Angle = BitConverter.ToInt32(bytes, num9);
		int num10 = num9 + 4;
		Pers = bytes[num10];
	}

	public object Clone()
	{
		return new VmdCamera(this);
	}
}