using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using SystemTrayModule.Properties;

namespace SystemTrayModule.Base
{
    class SystemTrayMainElement : IDisposable
    {
        private readonly NotifyIcon _notifyIcon;

        private readonly ContextMenu _contextMenu;

        public SystemTrayMainElement()
        {
            _contextMenu = new ContextMenu();
            var menuItem = new MenuItem("Exit", OnExitClick);
            _contextMenu.MenuItems.Add(menuItem);
            
            _notifyIcon = new NotifyIcon
            {
                Icon = Resources.system_network_icon,
                Text = @"Tray plugins container",
                Visible = true
            };

            _notifyIcon.MouseClick += OnMouseClick;
            _notifyIcon.MouseDoubleClick += OnMouseDoubleClick;
            _notifyIcon.ContextMenu = _contextMenu;
        }

        private void OnExitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OnMouseDoubleClick(object sender, MouseEventArgs mouseEventArgs)
        {
            //var connected = Pinger.Ping(new Uri("http://www.google.com/"));
            //MessageBox.Show("Ping result = " + connected, "Results", MessageBoxButtons.OK);
        }

        private void OnMouseClick(object sender, MouseEventArgs mouseEventArgs)
        {
            TryToConnect();
        }

        public void Dispose()
        {
            _notifyIcon.Visible = false;
            _notifyIcon.Dispose();
        }

        public void TryToConnect()
        {
            // Setup the variables necessary to make the Request 
            string grantType = "client_credentials";
            string username = "339052";
            string password = "100500";
            string url = "http://api.pleer.com/token.php";
            HttpWebResponse response = null;

            try
            {
                // Create the data to send
                var data = new StringBuilder();
                data.Append("grant_type=" + Uri.EscapeDataString(grantType));
                data.Append("&client_id=" + Uri.EscapeDataString(username));
                data.Append("&client_secret=" + Uri.EscapeDataString(password));

                // Create a byte array of the data to be sent
                var byteArray = Encoding.ASCII.GetBytes(data.ToString());

                // Setup the Request
                var request = (HttpWebRequest)WebRequest.Create(url);
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
                    string accessToken = x["access_token"].ToString();
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
                        MessageBox.Show(string.Format("The server returned '{0}' with the status code '{1} ({2:d})'.",
                          err.StatusDescription, err.StatusCode, err.StatusCode));
                    }
                }
            }
            finally
            {
                if (response != null) { response.Close(); }
            }
        }
    }
}
