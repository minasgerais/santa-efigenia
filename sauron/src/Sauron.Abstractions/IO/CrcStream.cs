using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Sauron.Abstractions.IO
{
    public class CrcStream : Stream
    {
        private readonly Stream _stream;

        private static readonly uint[] _table = GenerateTable();

        private uint _readCrc;

        private uint _writeCrc;

        public override bool CanRead { get => _stream.CanRead; }

        public override bool CanSeek { get => _stream.CanSeek; }

        public override bool CanWrite { get => _stream.CanWrite; }

        public override long Length { get => _stream.Length; }

        public override long Position { get => _stream.Position; set => _stream.Position = value; }

        public uint ReadCrc { get => unchecked(_readCrc ^ 0xFFFFFFFF); }

        public uint WriteCrc { get => unchecked(_writeCrc ^ 0xFFFFFFFF); }

        public CrcStream(Stream stream)
        {
            _stream = stream;
            Reset();
        }

        public override void Flush()
        {
            _stream.Flush();
        }

        public override Task FlushAsync(CancellationToken cancellationToken)
        {
            return _stream.FlushAsync(cancellationToken);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            count = _stream.Read(buffer, offset, count);
            _readCrc = CalculateCrc(_readCrc, buffer, offset, count);

            return count;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _stream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _stream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _stream.Write(buffer, offset, count);
            _writeCrc = CalculateCrc(_writeCrc, buffer, offset, count);
        }

        public void Reset()
        {
            _readCrc = unchecked(0xFFFFFFFF);
            _writeCrc = unchecked(0xFFFFFFFF);
        }

        private uint CalculateCrc(uint crc, byte[] buffer, int offset, int count)
        {
            unchecked
            {
                for (int i = offset, end = offset + count; i < end; i++)
                    crc = (crc >> 8) ^ _table[(crc ^ buffer[i]) & 0xFF];
            }
            return crc;
        }

        private static uint[] GenerateTable()
        {
            unchecked
            {
                uint[] table = new uint[256];

                uint crc;
                const uint poly = 0xEDB88320;
                for (uint i = 0; i < table.Length; i++)
                {
                    crc = i;
                    for (int j = 8; j > 0; j--)
                    {
                        if ((crc & 1) == 1)
                            crc = (crc >> 1) ^ poly;
                        else
                            crc >>= 1;
                    }
                    table[i] = crc;
                }
                return table;
            }
        }
    }
}
