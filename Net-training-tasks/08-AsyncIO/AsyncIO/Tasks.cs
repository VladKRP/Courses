﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AsyncIO
{
    public static class Tasks
    {


        /// <summary>
        /// Returns the content of required uris.
        /// Method has to use the synchronous way and can be used to compare the performace of sync \ async approaches. 
        /// </summary>
        /// <param name="uris">Sequence of required uri</param>
        /// <returns>The sequence of downloaded url content</returns>
        public static IEnumerable<string> GetUrlContent(this IEnumerable<Uri> uris) 
        {
            List<string> urisContent = new List<string>();
            foreach(var uri in uris)
            {
                using (WebClient webClient = new WebClient())
                    urisContent.Add(webClient.DownloadString(uri));
            }
            return urisContent;
        }



        /// <summary>
        /// Returns the content of required uris.
        /// Method has to use the asynchronous way and can be used to compare the performace of sync \ async approaches. 
        /// 
        /// maxConcurrentStreams parameter should control the maximum of concurrent streams that are running at the same time (throttling). 
        /// </summary>
        /// <param name="uris">Sequence of required uri</param>
        /// <param name="maxConcurrentStreams">Max count of concurrent request streams</param>
        /// <returns>The sequence of downloaded url content</returns>
        public static async Task<IEnumerable<string>> GetUrlContentAsync(this IEnumerable<Uri> uris, int maxConcurrentStreams)
        {
            List<string> urisContent = new List<string>();
            foreach(var uri in uris)
            {
                using (WebClient webClient = new WebClient())
                    await urisContent.Add(webClient.DownloadStringAsync(uri));
            }
            return urisContent;
        }


        /// <summary>
        /// Calculates MD5 hash of required resource.
        /// 
        /// Method has to run asynchronous. 
        /// Resource can be any of type: http page, ftp file or local file.
        /// </summary>
        /// <param name="resource">Uri of resource</param>
        /// <returns>MD5 hash</returns>
        public static Task<string> GetMD5Async(this Uri resource)
        {
            // TODO : Implement GetMD5Async
            throw new NotImplementedException();
        }

    }



}
