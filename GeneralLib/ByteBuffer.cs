﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralLib
{
    public class ByteBuffer : IDisposable
    {
        private List<byte> Buff;
        private byte[] readBuff;
        public int readPos { get; private set; }
        private bool buffUpdated = false;

        public int Count { get { return Buff.Count; } }
        public int Length { get { return Count - readPos; } }

        public ByteBuffer()
        {
            Buff = new List<byte>();
            readPos = 0;
        }

        public byte[] ToArray()
        {
            return Buff.ToArray();
        }

        public void Clear()
        {
            Buff.Clear();
            readPos = 0;
        }

        #region WriteToStream

        public void WriteByte(byte input)
        {
            Buff.Add(input);
            buffUpdated = true;
        }

        public void WriteBytes(byte[] input)
        {
            Buff.AddRange(input);
            buffUpdated = true;
        }

        public void WriteShort(short input)
        {
            Buff.AddRange(BitConverter.GetBytes(input));
            buffUpdated = true;
        }

        public void WriteInteger(int input)
        {
            Buff.AddRange(BitConverter.GetBytes(input));
            buffUpdated = true;
        }

        public void WriteLong(long input)
        {
            Buff.AddRange(BitConverter.GetBytes(input));
            buffUpdated = true;
        }

        public void WriteFloat(float input)
        {
            Buff.AddRange(BitConverter.GetBytes(input));
            buffUpdated = true;
        }

        public void WriteBool(bool input)
        {
            Buff.AddRange(BitConverter.GetBytes(input));
            buffUpdated = true;
        }

        public void WriteString(string input)
        {
            Buff.AddRange(BitConverter.GetBytes(input.Length));
            Buff.AddRange(Encoding.ASCII.GetBytes(input));
            buffUpdated = true;
        }

        #endregion

        public byte ReadByte(bool Peek = true)
        {
            if (Buff.Count > readPos)
            {
                if (buffUpdated)
                {
                    readBuff = Buff.ToArray();
                    buffUpdated = false;
                }

                byte value = readBuff[readPos];
                if (Peek & Buff.Count > readPos)
                {
                    readPos += 1;
                }

                return value;
            }
            else
            {
                throw new Exception("You are not trying to read out a 'BYTE'");
            }
        }

        public byte[] ReadBytes(int length, bool Peek = true)
        {
            if (Buff.Count > readPos)
            {
                if (buffUpdated)
                {
                    readBuff = Buff.ToArray();
                    buffUpdated = false;
                }

                byte[] value = Buff.GetRange(readPos, length).ToArray();
                if (Peek)
                {
                    readPos += length;
                }

                return value;
            }
            else
            {
                throw new Exception("You are not trying to read out a 'BYTE[]'");
            }
        }

        public short ReadShort(bool Peek = true)
        {
            if (Buff.Count > readPos)
            {
                if (buffUpdated)
                {
                    readBuff = Buff.ToArray();
                    buffUpdated = false;
                }

                short value = BitConverter.ToInt16(readBuff, readPos);
                if (Peek & Buff.Count > readPos)
                {
                    readPos += 2;
                }

                return value;
            }
            else
            {
                throw new Exception("You are not trying to read out a 'SHORT'");
            }
        }

        public int ReadInteger(bool Peek = true)
        {
            if (Buff.Count > readPos)
            {
                if (buffUpdated)
                {
                    readBuff = Buff.ToArray();
                    buffUpdated = false;
                }

                int value = BitConverter.ToInt32(readBuff, readPos);
                if (Peek & Buff.Count > readPos)
                {
                    readPos += 4;
                }

                return value;
            }
            else
            {
                throw new Exception("You are not trying to read out a 'INT'");
            }
        }

        public long ReadLong(bool Peek = true)
        {
            if (Buff.Count > readPos)
            {
                if (buffUpdated)
                {
                    readBuff = Buff.ToArray();
                    buffUpdated = false;
                }

                long value = BitConverter.ToInt64(readBuff, readPos);
                if (Peek & Buff.Count > readPos)
                {
                    readPos += 8;
                }

                return value;
            }
            else
            {
                throw new Exception("You are not trying to read out a 'LONG'");
            }
        }

        public float ReadFloat(bool Peek = true)
        {
            if (Buff.Count > readPos)
            {
                if (buffUpdated)
                {
                    readBuff = Buff.ToArray();
                    buffUpdated = false;
                }

                float value = BitConverter.ToSingle(readBuff, readPos);
                if (Peek & Buff.Count > readPos)
                {
                    readPos += 4;
                }

                return value;
            }
            else
            {
                throw new Exception("You are not trying to read out a 'FLOAT'");
            }
        }

        public bool ReadBool(bool Peek = true)
        {
            if (Buff.Count > readPos)
            {
                if (buffUpdated)
                {
                    readBuff = Buff.ToArray();
                    buffUpdated = false;
                }

                bool value = BitConverter.ToBoolean(readBuff, readPos);
                if (Peek & Buff.Count > readPos)
                {
                    readPos += 1;
                }

                return value;
            }
            else
            {
                throw new Exception("You are not trying to read out a 'BOOL'");
            }
        }

        public string ReadString(bool Peek = true)
        {

            try
            {
                int length = ReadInteger(true);

                if (buffUpdated)
                {
                    readBuff = Buff.ToArray();
                    buffUpdated = false;
                }

                string value = Encoding.ASCII.GetString(readBuff, readPos, length);
                if (Peek & Buff.Count > readPos)
                {
                    if (value.Length > 0)
                        readPos += length;
                }
                return value;
            }
            catch (Exception)
            {
                throw new Exception("You are not trying to read out a 'BOOL'");
            }
        }

        private bool disposedValue = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                if (disposing)
                {
                    Buff.Clear();
                    readPos = 0;
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
