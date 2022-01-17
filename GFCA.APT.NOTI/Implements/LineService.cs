using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.NOTI.Implements
{
    public class LineService : IDisposable
    {
        private const string _endPoint = "https://notify-api.line.me/api/notify";
        private const string _token = "";

        private HttpWebRequest _request;
        public LineService()
        {
            _request = (HttpWebRequest)WebRequest.Create(_endPoint);
        }

        public void SetHeader(string token, int contentLength)
        {
            _request.Method = "POST";
            _request.ContentType = "application/x-www-from-urlencoded";
            _request.ContentLength = contentLength;
            _request.Headers.Add("Authorization", $"Bearer {token}");
        }
        public void SetContext()
        {

        }

        public async Task<string> Send(string context)
        {

            try
            {

                var data = prepareData(context);
                SetHeader(_token, data.Length);

                using (var stream = _request.GetRequestStream())
                    stream.Write(data, 0, data.Length);

                var response = (HttpWebResponse)_request.GetResponse();
                //var responseStr = (new StreamReader(response.GetResponseStream()).ReadToEnd());
                //return await Task.FromResult(responseStr);

                var responseStr = await (new StreamReader(response.GetResponseStream()).ReadToEndAsync());
                return responseStr;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }
        
        private string attechSticker(int stickerPacketId, int stickerId)
        {
            var dataStickerPacketId = $"stickerPacketId={stickerPacketId}";
            var dataStickerId = $"stickerId={stickerId}";
            return $"{dataStickerPacketId}&{dataStickerId}";
        }
        private string attechFile(string filePath)
        {
            string msgEncoded = System.Web.HttpUtility.UrlEncode(filePath, Encoding.UTF8);
            var dataFile = $"imageFile={msgEncoded}";
            return dataFile;
        }
        private byte[] prepareData(string message)
        {

            string msgEncoded = System.Web.HttpUtility.UrlEncode(message, Encoding.UTF8);
            var dataPackage = $"message={msgEncoded}";
            var data = Encoding.UTF8.GetBytes(dataPackage);
            return data;

        }
        public void Dispose()
        {
            if (_request != null)
                _request = null;
        }

    }

}
