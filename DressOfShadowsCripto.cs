using System;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities.Encoders;

namespace DressOfShadows
{
    /// <summary>
    /// Class that provides blowfish encryption.
    /// </summary>
    /// 

    // Engines de BounceCastle
    // AESFastEngine
    // 

    public class DressOfShadowsCripto
    {
        const int KEY128BIT = 16;
        const int KEY192BIT = 24;
        const int KEY256BIT = 32;
        const int KEY448BIT = 56;

        private static readonly Encoding Encoding = Encoding.UTF8;
        public string ErrorDeRetorno { get; set; }

        public DressOfShadowsCripto()
        {
        }

        // ************************************************************
        // Procedimientos AES Encriptar
        // ************************************************************
        public string AESEncrypt(string strValue, string key)
        {
            ErrorDeRetorno = "";

            if (strValue == "" || key == "")
            {
                ErrorDeRetorno = "Los datos ingresados estan incompletos";
                return "";
            }

            if (key.Length > KEY256BIT)
            {
                ErrorDeRetorno = "La clave no puede ser mayor a 32 Caracteres";
                return "";
            }

            if (key.Length <= KEY128BIT)
            {
                for (int i = key.Length; i < KEY128BIT; i++)
                {
                    key += "*";
                }
            }
            else if (key.Length <= KEY192BIT)
            {
                for (int i = key.Length; i < KEY192BIT; i++)
                {
                    key += "*";
                }
            }
            else if (key.Length <= KEY256BIT)
            {
                for (int i = key.Length; i < KEY256BIT; i++)
                {
                    key += "*";
                }
            }

            try
            {
                AesEngine engine = new();

                PaddedBufferedBlockCipher cipher = new(engine);

                KeyParameter keyBytes = new(Encoding.GetBytes(key));

                cipher.Init(true, keyBytes);


                byte[] inB = Encoding.GetBytes(strValue);

                byte[] outB = new byte[cipher.GetOutputSize(inB.Length)];

                int len1 = cipher.ProcessBytes(inB, 0, inB.Length, outB, 0);

                _ = cipher.DoFinal(outB, len1);

                return BitConverter.ToString(outB).Replace("-", "");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ErrorDeRetorno = "Los datos ingresados estan incorrectos";
                return "";
            }
        }

        // ************************************************************
        // Procedimientos AES Desencriptar
        // ************************************************************
        public string AESDecrypt(string name, string keyString)
        {
            ErrorDeRetorno = "";

            if (name == "" || keyString == "")
            {
                ErrorDeRetorno = "Los datos ingresados estan incompletos";
                return "";
            }

            if (keyString.Length > KEY256BIT)
            {
                ErrorDeRetorno = "La clave no puede ser mayor a 32 Caracteres";
                return "";
            }

            if (keyString.Length <= KEY128BIT)
            {
                for (int i = keyString.Length; i < KEY128BIT; i++)
                {
                    keyString += "*";
                }
            }
            else if (keyString.Length <= KEY192BIT)
            {
                for (int i = keyString.Length; i < KEY192BIT; i++)
                {
                    keyString += "*";
                }
            }
            else if (keyString.Length <= KEY256BIT)
            {
                for (int i = keyString.Length; i < KEY256BIT; i++)
                {
                    keyString += "*";
                }
            }

            AesEngine engine = new();

            PaddedBufferedBlockCipher cipher = new(engine);

            StringBuilder result = new();

            try
            {
                cipher.Init(false, new KeyParameter(Encoding.GetBytes(keyString)));
            }
            catch (IndexOutOfRangeException)
            {
                ErrorDeRetorno = "Ingrese los datos a decodificar correctamente";
                return "";
            }

            byte[] out1;

            try
            {
                out1 = Hex.Decode(name);
            }
            catch (System.IO.IOException)
            {
                ErrorDeRetorno = "Los datos ingresados a decodificar no tienen el formato correcto.";
                return "";
            }

            byte[] out2 = new byte[cipher.GetOutputSize(out1.Length)];

            int len2 = cipher.ProcessBytes(out1, 0, out1.Length, out2, 0);

            try
            {
                _ = cipher.DoFinal(out2, len2); //Pad block corrupted error happens here
            }
            catch (DataLengthException)
            {
                ErrorDeRetorno = "Los datos ingresados a decodificar no concuerdan con la clave";
                return "";
            }
            catch (InvalidCipherTextException)
            {
                ErrorDeRetorno = "Los datos ingresados a decodificar no concuerdan con la clave";
                return "";
            }

            string s2 = BitConverter.ToString(out2);

            for (int i = 0; i < s2.Length; i++)
            {
                char c = s2[i];
                if (c != 0)
                {
                    _ = result.Append(c.ToString());
                }
            }

            ErrorDeRetorno = "";

            return FromHexString(result.ToString().Replace("-", ""));
        }

        // ************************************************************
        // Procedimientos BlowFish Encriptar
        // ************************************************************
        public string BlowfishEncrypt(string strValue, string key)
        {
            ErrorDeRetorno = "";

            if (strValue == "" || key == "")
            {
                ErrorDeRetorno = "Los datos ingresados estan incompletos";
                return "";
            }

            if (key.Length > KEY448BIT)
            {
                ErrorDeRetorno = "La clave no puede ser mayor a 56 Caracteres";
                return "";
            }

            try
            {
                BlowfishEngine engine = new();

                PaddedBufferedBlockCipher cipher = new(engine);

                KeyParameter keyBytes = new(Encoding.GetBytes(key));

                cipher.Init(true, keyBytes);

                byte[] inB = Encoding.GetBytes(strValue);

                byte[] outB = new byte[cipher.GetOutputSize(inB.Length)];

                int len1 = cipher.ProcessBytes(inB, 0, inB.Length, outB, 0);

                _ = cipher.DoFinal(outB, len1);

                return BitConverter.ToString(outB).Replace("-", "");
            }
            catch (Exception)
            {
                ErrorDeRetorno = "Los datos ingresados estan incorrectos";
                return "";
            }
        }

        // ************************************************************
        // Procedimientos BlowFish DesEncriptar
        // ************************************************************
        public string BlowfishDecrypt(string name, string keyString)
        {
            ErrorDeRetorno = "";

            if (name == "" || keyString == "")
            {
                ErrorDeRetorno = "Los datos ingresados estan incompletos";
                return "";
            }

            if (keyString.Length > KEY448BIT)
            {
                ErrorDeRetorno = "La clave no puede ser mayor a 56 Caracteres";
                return "";
            }

            BlowfishEngine engine = new();
            PaddedBufferedBlockCipher cipher = new(engine);

            StringBuilder result = new();

            try
            {
                cipher.Init(false, new KeyParameter(Encoding.GetBytes(keyString)));
            }
            catch (IndexOutOfRangeException)
            {
                ErrorDeRetorno = "Ingrese los datos a decodificar correctamente";
                return "";
            }

            byte[] out1;

            try
            {
                out1 = Hex.Decode(name);
            }
            catch (System.IO.IOException)
            {
                ErrorDeRetorno = "Los datos ingresados a decodificar no tienen el formato correcto.";
                return "";
            }

            byte[] out2 = new byte[cipher.GetOutputSize(out1.Length)];

            int len2 = cipher.ProcessBytes(out1, 0, out1.Length, out2, 0);

            try
            {
                _ = cipher.DoFinal(out2, len2); //Pad block corrupted error happens here
            }
            catch (DataLengthException)
            {
                ErrorDeRetorno = "Los datos ingresados a decodificar no concuerdan con la clave";
                return "";
            }
            catch (InvalidCipherTextException)
            {
                ErrorDeRetorno = "Los datos ingresados a decodificar no concuerdan con la clave";
                return "";
            }

            string s2 = BitConverter.ToString(out2);

            for (int i = 0; i < s2.Length; i++)
            {
                char c = s2[i];
                if (c != 0)
                {
                    _ = result.Append(c.ToString());
                }
            }

            ErrorDeRetorno = "";

            return FromHexString(result.ToString().Replace("-", ""));
        }

        // ************************************************************
        // Procedimientos ElGamal Encriptar
        // ************************************************************
        public string ElGamalEncrypt(string strValue, string key)
        {
            ErrorDeRetorno = "";

            if (strValue == "" || key == "")
            {
                ErrorDeRetorno = "Los datos ingresados estan incompletos";
                return "";
            }

            try
            {
                ElGamalEngine engine = new();

                BufferedAsymmetricBlockCipher cipher = new(engine);

                KeyParameter keyBytes = new(Encoding.GetBytes(key));

                cipher.Init(true, keyBytes);


                byte[] inB = Encoding.GetBytes(strValue);

                byte[] outB = new byte[cipher.GetOutputSize(inB.Length)];

                int len1 = cipher.ProcessBytes(inB, 0, inB.Length, outB, 0);

                _ = cipher.DoFinal(outB, len1);

                return BitConverter.ToString(outB).Replace("-", "");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ErrorDeRetorno = "Los datos ingresados estan incorrectos";
                return "";
            }
        }

        // ************************************************************
        // Procedimientos ElGamal DesEncriptar
        // ************************************************************
        public string ElGamalDecrypt(string name, string keyString)
        {
            ErrorDeRetorno = "";

            if (name == "" || keyString == "")
            {
                ErrorDeRetorno = "Los datos ingresados estan incompletos";
                return "";
            }

            ElGamalEngine engine = new();
            BufferedAsymmetricBlockCipher cipher = new(engine);

            StringBuilder result = new();

            try
            {
                cipher.Init(false, new KeyParameter(Encoding.GetBytes(keyString)));
            }
            catch (IndexOutOfRangeException)
            {
                ErrorDeRetorno = "Ingrese los datos a decodificar correctamente";
                return "";
            }

            byte[] out1;

            try
            {
                out1 = Hex.Decode(name);
            }
            catch (System.IO.IOException)
            {
                ErrorDeRetorno = "Los datos ingresados a decodificar no tienen el formato correcto.";
                return "";
            }

            byte[] out2 = new byte[cipher.GetOutputSize(out1.Length)];

            int len2 = cipher.ProcessBytes(out1, 0, out1.Length, out2, 0);

            try
            {
                _ = cipher.DoFinal(out2, len2); //Pad block corrupted error happens here
            }
            catch (DataLengthException)
            {
                ErrorDeRetorno = "Los datos ingresados a decodificar no concuerdan con la clave";
                return "";
            }
            catch (InvalidCipherTextException)
            {
                ErrorDeRetorno = "Los datos ingresados a decodificar no concuerdan con la clave";
                return "";
            }

            string s2 = BitConverter.ToString(out2);

            for (int i = 0; i < s2.Length; i++)
            {
                char c = s2[i];
                if (c != 0)
                {
                    _ = result.Append(c.ToString());
                }
            }

            ErrorDeRetorno = "";

            return FromHexString(result.ToString().Replace("-", ""));
        }

        // ************************************************************
        // Procedimientos TwoFish Encriptar
        // ************************************************************
        public string TwofishEncrypt(string strValue, string key)
        {
            ErrorDeRetorno = "";

            if (strValue == "" || key == "")
            {
                ErrorDeRetorno = "Los datos ingresados estan incompletos";
                return "";
            }

            try
            {
                TwofishEngine engine = new();

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
                ErrorDeRetorno = "Los datos ingresados estan incorrectos";
                return "";
            }
        }

        // ************************************************************
        // Procedimientos TwoFish DesEncriptar
        // ************************************************************
        public string TwofishDecrypt(string name, string keyString)
        {
            ErrorDeRetorno = "";

            if (name == "" || keyString == "")
            {
                ErrorDeRetorno = "Los datos ingresados estan incompletos";
                return "";
            }

            TwofishEngine engine = new();

            PaddedBufferedBlockCipher cipher = new(engine);

            StringBuilder result = new();

            try
            {
                cipher.Init(false, new KeyParameter(Encoding.GetBytes(keyString)));
            }
            catch (IndexOutOfRangeException)
            {
                ErrorDeRetorno = "Ingrese los datos a decodificar correctamente";
                return "";
            }

            byte[] out1;

            try
            {
                out1 = Hex.Decode(name);
            }
            catch (System.IO.IOException)
            {
                ErrorDeRetorno = "Los datos ingresados a decodificar no tienen el formato correcto.";
                return "";
            }

            byte[] out2 = new byte[cipher.GetOutputSize(out1.Length)];

            int len2 = cipher.ProcessBytes(out1, 0, out1.Length, out2, 0);

            try
            {
                cipher.DoFinal(out2, len2); //Pad block corrupted error happens here
            }
            catch (DataLengthException)
            {
                ErrorDeRetorno = "Los datos ingresados a decodificar no concuerdan con la clave";
                return "";
            }
            catch (InvalidCipherTextException)
            {
                ErrorDeRetorno = "Los datos ingresados a decodificar no concuerdan con la clave";
                return "";
            }

            string s2 = BitConverter.ToString(out2);

            for (int i = 0; i < s2.Length; i++)
            {
                char c = s2[i];
                if (c != 0)
                {
                    _ = result.Append(c.ToString());
                }
            }

            ErrorDeRetorno = "";

            return FromHexString(result.ToString().Replace("-", ""));
        }

        // ************************************************************
        // Procedimientos Utilitarios
        // ************************************************************
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
