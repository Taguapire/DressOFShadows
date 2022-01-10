using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Text;

namespace DressOfShadows
{
    /// <summary>
    /// Class that provides blowfish encryption.
    /// </summary>
    /// 

    public class DressOfShadowsCripto
    {
        private static readonly Encoding Encoding = Encoding.UTF8;

        public DressOfShadowsCripto()
        {
        }

        public string Encriptar(string pClave, string pTextoNormal)
        {
            return BlowfishEncrypt(pTextoNormal, pClave);
        }

        public string DesEncriptar(string pClave, string pTextoEncriptado)
        {
            return BlowfishDecrypt(pTextoEncriptado, pClave);
        }

        private string BlowfishEncrypt(string strValue, string key)
        {
            try
            {
                BlowfishEngine engine = new();

                PaddedBufferedBlockCipher cipher = new(engine);

                KeyParameter keyBytes = new(Encoding.GetBytes(key));

                cipher.Init(true, keyBytes);

                byte[] inB = Encoding.GetBytes(strValue);

                byte[] outB = new byte[cipher.GetOutputSize(inB.Length)];

                int len1 = cipher.ProcessBytes(inB, 0, inB.Length, outB, 0);

                cipher.DoFinal(outB, len1);

                return BitConverter.ToString(outB).Replace("-", "");
            }
            catch (Exception)
            {
                return "";
            }
        }

        private string BlowfishDecrypt(string name, string keyString)
        {
            BlowfishEngine engine = new();
            PaddedBufferedBlockCipher cipher = new(engine);

            StringBuilder result = new();

            cipher.Init(false, new KeyParameter(Encoding.GetBytes(keyString)));

            byte[] out1 = Hex.Decode(name);
            byte[] out2 = new byte[cipher.GetOutputSize(out1.Length)];

            int len2 = cipher.ProcessBytes(out1, 0, out1.Length, out2, 0);

            cipher.DoFinal(out2, len2); //Pad block corrupted error happens here

            string s2 = BitConverter.ToString(out2);

            for (int i = 0; i < s2.Length; i++)
            {
                char c = s2[i];
                if (c != 0)
                {
                    result.Append(c.ToString());
                }
            }

            return FromHexString(result.ToString().Replace("-", ""));
        }

        private string FromHexString(string hexString)
        {
            byte[] bytes = new byte[hexString.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return Encoding.GetString(bytes);
        }
    }
}
