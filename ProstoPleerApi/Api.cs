using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace ProstoPleerApi
{
    public class Api
    {
        /// <summary>
        /// Параметры подключения
        /// </summary>
        public ConnectionSettings Settings { get; set; }

        public Api()
        {
            Settings = new ConnectionSettings();
        }

        public bool GetToken()
        {
            HttpWebResponse response = null;
            try
            {
                // Create the data to send
                var data = new StringBuilder();
                data.Append("grant_type=" + Uri.EscapeDataString(Settings.GrantType));
                data.Append("&client_id=" + Uri.EscapeDataString(Settings.ClientId));
                data.Append("&client_secret=" + Uri.EscapeDataString(Settings.ClientSecret));

                // Create a byte array of the data to be sent
                var byteArray = Encoding.ASCII.GetBytes(data.ToString());

                // Setup the Request
                var request = (HttpWebRequest)WebRequest.Create(Settings.TokenUrl);
                request.Method = WebRequestMethods.Http.Post;
                request.ProtocolVersion = HttpVersion.Version11;
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;

                // Write data
                var postStream = request.GetRequestStream();
                postStream.Write(byteArray, 0, byteArray.Length);
                postStream.Close();

                // Send Request & Get Response
                response = (HttpWebResponse)request.GetResponse();

                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    // Get the Response Stream
                    var json = reader.ReadLine();
                    Console.WriteLine(json);

                    // Retrieve and Return the Access Token
                    var ser = new JavaScriptSerializer();
                    var x = (Dictionary<string, object>)ser.DeserializeObject(json);
                    Settings.AccessToken = x["access_token"].ToString();

                    return true;
                }
            }
            catch (WebException e)
            {
                // This exception will be raised if the server didn't return 200 - OK
                // Retrieve more information about the error
                if (e.Response != null)
                {
                    using (var err = (HttpWebResponse)e.Response)
                    {
                        var error = string.Format("The server returned '{0}' with the status code '{1} ({2:d})'.",
                          err.StatusDescription, err.StatusCode, err.StatusCode);
                    }
                }
            }
            finally
            {
                if (response != null) { response.Close(); }
            }

            return false;
        }

        public bool SearchTracks()
        {
            throw new NotImplementedException();
        }

        public bool GetTrackInfo()
        {
            throw new NotImplementedException();
        }

        public bool GetTrackLyrics()
        {
            throw new NotImplementedException();
        }

        public bool DownloadTrack()
        {
            throw new NotImplementedException();
        }

        public bool GetTopList()
        {
            throw new NotImplementedException();
        }
    }
}
