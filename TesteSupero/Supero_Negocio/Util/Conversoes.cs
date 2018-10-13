using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Supero_Negocio.Util
{
    public static class Conversoes
    {

        public static void StreamToFile(this Stream fileStream, string parDiretorio)
        {
            try
            {
                byte[] buffer = new byte[2048];
                using (FileStream ws = new FileStream(parDiretorio, FileMode.Create))
                {
                    int bytesRead = fileStream.Read(buffer, 0, buffer.Length);
                    while (bytesRead > 0)
                    {
                        ws.Write(buffer, 0, bytesRead);
                        bytesRead = fileStream.Read(buffer, 0, buffer.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao converter stream to file " + ex.GetDetalheErro());
            }
        }

        public static String FileToBase64(String parDiretorio)
        {
            try
            {
                Byte[] bytes = File.ReadAllBytes(parDiretorio);
                return Convert.ToBase64String(bytes);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao converter arquivo para Base64 " + ex.GetDetalheErro());
            }
        }

        public static string Base64ToFile(String parBinario, String parDiretorio, String fileName)
        {
            try
            {
                Byte[] bytes = Convert.FromBase64String(parBinario);
                var arquivo = parDiretorio + "\\" + fileName;
                File.WriteAllBytes(arquivo, bytes);
                return arquivo;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao converter Base64 para arquivo " + ex.GetDetalheErro());
            }
        }

        public static string ConvertToBase64(this Stream stream)
        {
            try
            {
                byte[] bytes;
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    bytes = memoryStream.ToArray();
                }

                return Convert.ToBase64String(bytes);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao converter stream para base64 " + ex.GetDetalheErro());
            }
        }

        public static byte[] ConvertToByte(this string base64)
        {
            try
            {
                return Encoding.UTF8.GetBytes(base64);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao converter Base64 para Byte[] " + ex.GetDetalheErro());
            }
        }

        public static string FormataString(this string parConteudo)
        {
            return ((parConteudo != String.Empty && parConteudo != null) ? RemoveAspasSimples(RemoveUTF8Enconding(parConteudo)) : "");
        }

        public static string RemoveAspasSimples(this string parConteudo)
        {
            try
            {
                return parConteudo.Replace("''", "\"\"").Replace("'", "\"");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao remover aspas simples de texto " + ex.GetDetalheErro());
            }
        }

        public static string GetDetalheErro(this Exception ex)
        {
            var trace = "";

            try
            {
                trace = "StackTrace -> [ " + ex.StackTrace.ToString() + "] " + Environment.NewLine;
            }
            catch (Exception)
            {
                trace = "StackTrace -> [ NO StackTrace ] " + Environment.NewLine;
            }

            return Environment.NewLine + ex.Message.ToString() + trace;
        }

        //public static string EncodeToUTF8(this string parConteudo)
        //{
        //    Encoding encoding = Encoding.GetEncoding(1252);
        //    string textoInWin1252 = encoding.GetString(encoding.GetBytes(parConteudo));

        //    return parConteudo.Equals(textoInWin1252, StringComparison.Ordinal) ? parConteudo : Convert.ToBase64String(Encoding.UTF8.GetBytes(parConteudo));
        //}

        public static string RemoveUTF8Enconding(this string parConteudo)
        {
            try
            {
                return Regex.Replace((Encoding.GetEncoding(1252).GetString(Encoding.GetEncoding(1252).GetBytes(parConteudo))), @"[^\u0020-\u007E\u00A0-\u00FF]", string.Empty);
                //return Regex.Replace(parConteudo, @"[^\u0020-\u007E\u00A0-\u00FF]", string.Empty);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao remover caracteres UFT-8 de texto " + ex.GetDetalheErro());
            }
        }

        public static string EncodeToWin1252(this string parConteudo)
        {
            return Encoding.GetEncoding(1252).GetString(Encoding.GetEncoding(1252).GetBytes(parConteudo));
        }

        public static string DecodeUTF8(this string parConteudo)
        {
            byte[] bytes = Encoding.Default.GetBytes(parConteudo);
            return Encoding.UTF8.GetString(bytes);
        }

        public static string DecodeUTF8ToIso8859(this string parConteudo)
        {
            byte[] utfBytes = Encoding.UTF8.GetBytes(parConteudo);
            byte[] isoBytes = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding("ISO-8859-1"), utfBytes);
            return Encoding.GetEncoding("ISO-8859-1").GetString(isoBytes);
        }

        public static string ObjectToString(this object parObjeto)
        {
            try
            {
                if (!String.IsNullOrEmpty(((System.Xml.XmlText)((System.Xml.XmlNode[])parObjeto)[0]).Value))
                {
                    return ((System.Xml.XmlText)((System.Xml.XmlNode[])parObjeto)[0]).Value;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static decimal? ObjectToDecimal(this object parObjeto)
        {
            try
            {
                return decimal.Parse(((System.Xml.XmlText)((System.Xml.XmlNode[])parObjeto)[0]).Value);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static double? ObjectToDouble(this object parObjeto)
        {
            try
            {
                return double.Parse(((System.Xml.XmlText)((System.Xml.XmlNode[])parObjeto)[0]).Value.Replace(".", ","));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static long? ObjectToLong(this object parObjeto)
        {
            try
            {
                return long.Parse(((System.Xml.XmlText)((System.Xml.XmlNode[])parObjeto)[0]).Value);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static int? ObjectToInt(this object parObjeto)
        {
            try
            {
                return Int32.Parse(((System.Xml.XmlText)((System.Xml.XmlNode[])parObjeto)[0]).Value);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static short? ObjectToShort(this object parObjeto)
        {
            try
            {
                return short.Parse(((System.Xml.XmlText)((System.Xml.XmlNode[])parObjeto)[0]).Value);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DateTime? ObjectToDateTime(this object parObjeto)
        {
            try
            {
                return DateTime.Parse(((System.Xml.XmlText)((System.Xml.XmlNode[])parObjeto)[0]).Value);
            }
            catch (Exception)
            {
                try
                {
                    return DateTime.Parse(parObjeto.ToString());
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public static string RemoverAcentucao(this string parConteudo)
        {
            try
            {
                StringBuilder sbReturn = new StringBuilder();
                var arrayText = parConteudo.Normalize(NormalizationForm.FormD).ToCharArray();
                foreach (char letter in arrayText)
                {
                    if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                        sbReturn.Append(letter);
                }
                return sbReturn.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao remover acentuacao de string (" + parConteudo + ") -> " + ex.GetDetalheErro());
            }
        }

        public static string GetHoraByDateTime(this DateTime? parData)
        {
            var data = parData ?? DateTime.Now;
            return data.ToString("HH:mm");
        }

        public static string GetHoraByDateTime(this DateTime parData)
        {
            return parData.ToString("HH:mm");
        }

        public static string FormatDiaMesAno(this DateTime parData)
        {
            return parData.ToString("yyyy-MM-dd");
        }

        public static string FormatDiaMesAnoHora(this DateTime parData)
        {
            return parData.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static int ToInt(this int? parValor)
        {
            return int.Parse(parValor.ToString());
        }

        public static DateTime ToDateTime(this string parValue)
        {
            DateTime dateTime;
            return DateTime.TryParse(parValue, out dateTime) ? dateTime : dateTime;
        }


        public static string GetInitials(this string parValue, int parTamanho)
        {
            if (string.IsNullOrEmpty(parValue))
            {
                return "";
            }
            else
            {
                return parValue.Substring(0, (parValue.Length < parTamanho ? parValue.Length : parTamanho));
            }
        }

        public static decimal GetPorcentagemDesconto(this decimal? parPrecoOrinal, decimal? parPrecoFinal)
        {
            return (decimal)(100 - Math.Round((double)((parPrecoFinal / parPrecoOrinal) * 100), 2));
        }


    }
}
