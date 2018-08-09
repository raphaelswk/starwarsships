using Newtonsoft.Json;
using SWSC.Entities;
using SWSC.Persistence.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SWSC.Persistence.Impl
{
    public class StarshipDAO : IStarshipDAO
    {
        private enum HttpMethod
        {
            GET,
            POST
        }

        private string apiUrl = "http://swapi.co/api";
        private string _proxyName = null;

        /// <summary>
        /// Get all the starship resources
        /// </summary>
        public async Task<List<Starship>> GetAllStarshipsAsync(string pageNumber = "1")
        {
            List<Starship> ships = new List<Starship>();
            bool isLastPage = false;

            try
            {
                do
                {
                    StarshipResults result = await GetAllPaginatedAsync<Starship>("/starships/", pageNumber);
                    ships.AddRange(result.results);

                    if (string.IsNullOrEmpty(result.nextPageNo))
                    {
                        isLastPage = true;
                    }
                    else
                    {
                        pageNumber = result.nextPageNo;
                    }

                } while (isLastPage == false);

                return ships;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get all starship resources by page
        /// </summary>
        private async Task<StarshipResults> GetAllPaginatedAsync<T>(string entityName, string pageNumber = "1")
        {
            try
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("page", pageNumber);

                StarshipResults result = await GetMultipleAsync<T>(entityName, parameters);

                result.nextPageNo = String.IsNullOrEmpty(result.next) ? null : GetQueryParameters(result.next)["page"];
                result.previousPageNo = String.IsNullOrEmpty(result.previous) ? null : GetQueryParameters(result.previous)["page"];

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private NameValueCollection GetQueryParameters(string dataWithQuery)
        {
            try
            {
                NameValueCollection result = new NameValueCollection();
                string[] parts = dataWithQuery.Split('?');
                if (parts.Length > 0)
                {
                    string QueryParameter = parts.Length > 1 ? parts[1] : parts[0];
                    if (!string.IsNullOrEmpty(QueryParameter))
                    {
                        string[] p = QueryParameter.Split('&');
                        foreach (string s in p)
                        {
                            if (s.IndexOf('=') > -1)
                            {
                                string[] temp = s.Split('=');
                                result.Add(temp[0], temp[1]);
                            }
                            else
                            {
                                result.Add(s, string.Empty);
                            }
                        }
                    }
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }            
        }

        private async Task<StarshipResults> GetMultipleAsync<T>(string endpoint, Dictionary<string, string> parameters)
        {
            try
            {
                string serializedParameters = "";
                if (parameters != null)
                {
                    serializedParameters = "?" + SerializeDictionary(parameters);
                }

                string json = await RequestAsync(
                    string.Format("{0}{1}{2}", apiUrl, endpoint, serializedParameters),
                    HttpMethod.GET,
                    data: null,
                    isProxyEnabled: false);
                StarshipResults swapiResponse = JsonConvert.DeserializeObject<StarshipResults>(json);
                return swapiResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        private string SerializeDictionary(Dictionary<string, string> dictionary)
        {
            try
            {
                StringBuilder parameters = new StringBuilder();
                foreach (KeyValuePair<string, string> keyValuePair in dictionary)
                {
                    parameters.Append(keyValuePair.Key + "=" + keyValuePair.Value + "&");
                }
                return parameters.Remove(parameters.Length - 1, 1).ToString();
            }
            catch (Exception)
            {
                throw;
            }            
        }

        private async Task<string> RequestAsync(string url, HttpMethod httpMethod, string data, bool isProxyEnabled)
        {
            try
            {
                string result = string.Empty;
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Method = httpMethod.ToString();

                if (!String.IsNullOrEmpty(_proxyName))
                {
                    httpWebRequest.Proxy = new WebProxy(_proxyName, 80);
                    httpWebRequest.Proxy.Credentials = CredentialCache.DefaultCredentials;
                }

                if (data != null)
                {
                    byte[] bytes = UTF8Encoding.UTF8.GetBytes(data.ToString());
                    httpWebRequest.ContentLength = bytes.Length;
                    Stream stream = await httpWebRequest.GetRequestStreamAsync();
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Dispose();
                }

                HttpWebResponse httpWebResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
                StreamReader reader = new StreamReader(httpWebResponse.GetResponseStream());
                result = await reader.ReadToEndAsync();
                reader.Dispose();

                return result;
            }
            catch (Exception)
            {
                throw;
            }            
        }
    }
}
