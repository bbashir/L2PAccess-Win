using System.Threading.Tasks;
using L2PAccess.API.Model;
using Refit;

namespace L2PAccess.API
{
    /// <summary>
    /// Methods on the L2P
    /// </summary>
    interface IL2PApi
    {
        [Get("/viewAllCourseInfo")]
        Task<L2PResponse<Course>> ViewAllCourseInfo();

        [Get("/viewCourseInfo")]
        Task<L2PResponse<Course>> ViewCourseInfo([AliasAs("cid")] string courseId);
    }
}
