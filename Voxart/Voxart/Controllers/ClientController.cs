using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Voxart.Data;
using Voxart.Lib.Interactions.Sber;
using Voxart.Lib.LowManage;
using Voxart.Shared;
using Voxart.Shared.WebInteractions;

namespace Voxart.Controllers
{
    [Route("/client")]
    [Authorize]
    public class ClientController(ApplicationDbContext db, ISaluteSpeechClient speech) : Controller
    {
        [HttpPost("synthesize")]
        public async Task<SynthesizeResponse> Synthesize([FromBody] SynthesizeRequest request)
        {
            var cluster = await CheckCluster(User.Identity!.Name!);

            try
            {
                var result = await speech.Synthesize(request.Text, voice: request.VoiceCode);
                var file = Guid.NewGuid().ToString();
                var format = FileFormat.Wav16;

                ClustersControl.CreateFile(cluster, file, format, result);

                var clusterFile = new ClusterFile()
                {
                    ClusterId = cluster.FullName,
                    CreationTime = DateTime.Now,
                    FileFormat = format,
                    Name = file
                };

                db.ClusterFiles.Add(clusterFile);

                await db.SaveChangesAsync();

                return new SynthesizeResponse()
                {
                    IsSuccess = true,
                    ErrorCode = ErrorCode.NoError,
                    ResultLink = $"{cluster.FullName}/{file}"
                };
            }
            catch
            {
                return new SynthesizeResponse { IsSuccess = false, ErrorCode = ErrorCode.FailedGeneration };
            }
        }

        private async Task<DirectoryInfo> CheckCluster(string user)
        {
            var dbUser = db.Users.First(u => u.UserName == user);

            if (string.IsNullOrEmpty(dbUser.Cluster))
            {
                var guid = Guid.NewGuid().ToString();
                ClustersControl.CreateCluster(guid);
                dbUser.Cluster = guid;
                await db.SaveChangesAsync();
            }

            return ClustersControl.GetCluster(dbUser.Cluster)!;
        }
    }
}
