using System;

namespace Efrpg.Licensing
{
    public static class Hex
    {
        /// <summary>
        /// Converts HEX string to byte array.
        /// Opposite of ByteArrayToHex.
        /// </summary>
        public static byte[] HexToByteArray(string hexString)
        {
            if (hexString == null)
                return null;

            if ((hexString.Length % 2) != 0)
                throw new ApplicationException("Hex string must be multiple of 2 in length");

            var byteCount = hexString.Length / 2;
            var byteValues = new byte[byteCount];
            for (var i = 0; i < byteCount; i++)
            {
                byteValues[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return byteValues;
        }

        /// <summary>
        /// Convert bytes to 2 hex characters per byte, "-" separators are removed.
        /// Opposite of HexToByteArray
        /// </summary>
        public static string ByteArrayToHex(byte[] data)
        {
            if (data == null)
                return string.Empty;

            return BitConverter.ToString(data).Replace("-", "");
        }
    }
}