using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.DataProtection;
using Windows.Storage;
using Windows.Storage.Streams;
using L2PAccess.Authentication.Model;
using L2PAccess.Authentication.Model.Response;

namespace L2PAccess.Authentication.Storage
{
    public class SecureTokenStorage : ITokenStorage
    {
        private const BinaryStringEncoding ENCODING = BinaryStringEncoding.Utf8;
        private readonly DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Token));
        private const string TokenFileName = "rwth_access_token";
        private const string FileExtension = ".txt";

        public async Task Save(string accessToken, string refreshToken, string accessTokenExpiration)
        {
            await Save(new Token()
            {
                access_token = accessToken,
                refresh_token = refreshToken,
                accessTokenExpirationDate = DateTime.Now.AddSeconds(Double.Parse(accessTokenExpiration))
            });
        }

        public async Task Save(Token token)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, token);
                var bytes = ms.ToArray();
                await SaveAccessToken(Encoding.UTF8.GetString(bytes, 0, bytes.Length));
            }
        }

        public async Task<Token> Read()
        {
            string json = await ReadAccessToken();

            if (string.IsNullOrEmpty(json))
                throw new IOException();

            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                return (Token)serializer.ReadObject(ms);
            }
        }

        public async Task<string> ReadAccessToken()
        {
            StorageFolder local = ApplicationData.Current.LocalFolder;
            var file = await local.GetFileAsync(TokenFileName + FileExtension);

            var text = await FileIO.ReadBufferAsync(file);

            if (text.Length == 0)
                return string.Empty;


            DataProtectionProvider Provider = new DataProtectionProvider();
            IBuffer buffUnprotected = await Provider.UnprotectAsync(text);

            return CryptographicBuffer.ConvertBinaryToString(ENCODING, buffUnprotected);
        }

        public async Task<IStorageFile> SaveAccessToken(string token)
        {
            DataProtectionProvider Provider = new DataProtectionProvider("LOCAL=user");
            IBuffer buffMsg = CryptographicBuffer.ConvertStringToBinary(token, ENCODING);
            IBuffer buffProtected = await Provider.ProtectAsync(buffMsg);

            StorageFolder local = ApplicationData.Current.LocalFolder;
            var file = await local.CreateFileAsync(TokenFileName + FileExtension, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteBufferAsync(file, buffProtected);
            return file;
        }
    }
}
