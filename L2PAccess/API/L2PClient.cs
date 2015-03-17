using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using L2PAccess.API.Model;
using L2PAccess.Authentication;
using L2PAccess.Authentication.Config;
using Refit;

namespace L2PAccess.API
{
    public class L2PClient : IL2PApi
    {
        private const string DefaultL2PUrl = "https://www3.elearning.rwth-aachen.de/_vti_bin/l2pservices/api.svc/v1";
        private readonly IL2PApi client;

        public L2PClient(string l2PServiceUrl = DefaultL2PUrl)
        {
            client = RestService.For<IL2PApi>(l2PServiceUrl);
        }

        public L2PClient(OAuthConfig config, string l2PServiceUrl = DefaultL2PUrl)
        {
            var httpClient = new HttpClient(new OAuthHttpClientHandler(config))
            {
                BaseAddress = new Uri(l2PServiceUrl)
            };
            client = RestService.For<IL2PApi>(httpClient);
        }

        public async Task<L2PResponse<Course>> ViewAllCourseInfo()
        {
            return await client.ViewAllCourseInfo();
        }

        public async Task<L2PResponse<Course>> ViewCourseInfo(string courseId)
        {
            var l2PResponse = await client.ViewCourseInfo(courseId);
            return l2PResponse;
        }
    }
}
