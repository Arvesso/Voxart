using Microsoft.AspNetCore.Mvc;
using Voxart.Lib.Interactions.D_ID;
using Voxart.Lib.Interactions.Sber;
using Voxart.Lib.LowManage;
using Voxart.Server.Low.Database;
using Voxart.Server.Low.Processing;
using Voxart.Shared;
using Voxart.Shared.WebInteractions;

namespace Voxart.Server.Low.Controllers
{
    [Route("/client")]
    public class ClientController(ApplicationDbContext db, ISaluteSpeechClient speech, IDidClient did) : Controller
    {
        [HttpGet("create")]
        public ActionResult CreateNewClient()
        {
            var cluster = Guid.NewGuid().ToString();

            //ClustersControl.CreateCluster(cluster);

            return Json("access_denied");
        }

        [HttpPost("synthesize")]
        public async Task<SynthesizeResponse> Synthesize([FromBody] SynthesizeRequest request)
        {
            var cluster = ClustersControl.GetCluster(request.ClientId);

            if (cluster is null)
                return new() { IsSuccess = false, ErrorCode = ErrorCode.Unknown };

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

                return new()
                {
                    IsSuccess = true,
                    ErrorCode = ErrorCode.NoError,
                    ResultLink = $"clusters/audio/{cluster.Name}/{file}"
                };
            }
            catch
            {
                return new() { IsSuccess = false, ErrorCode = ErrorCode.FailedGeneration };
            }
        }

        [HttpPost("recognize")]
        public async Task<RecognizeResponse> Recognize([FromBody] RecognizeRequest request)
        {
            var cluster = ClustersControl.GetCluster(request.ClientId);

            if (cluster is null)
                return new() { IsSuccess = false, ErrorCode = ErrorCode.Unknown };

            var audio = request.AudioFormat is AudioFormat.mpeg3 ? request.Audio : AudioConverter.ConvertWavToMp3(request.Audio);

            try
            {
                var result = await speech.Recognize(audio);

                var file = Guid.NewGuid().ToString();
                var format = FileFormat.TXT;

                ClustersControl.CreateFile(cluster, file, format, result[0]);

                var clusterFile = new ClusterFile()
                {
                    ClusterId = cluster.FullName,
                    CreationTime = DateTime.Now,
                    FileFormat = format,
                    Name = file
                };

                db.ClusterFiles.Add(clusterFile);

                await db.SaveChangesAsync();

                return new()
                {
                    IsSuccess = true,
                    ErrorCode = ErrorCode.NoError,
                    ResultLink = $"clusters/text/{cluster.Name}/{file}"
                };
            }
            catch
            {
                return new() { IsSuccess = false, ErrorCode = ErrorCode.FailedGeneration };
            }
        }

        [HttpPost("avatar")]
        public async Task<AvatarResponse> Avatar([FromBody] AvatarRequest request)
        {
            var cluster = ClustersControl.GetCluster(request.ClientId);

            if (cluster is null)
                return new() { IsSuccess = false, ErrorCode = ErrorCode.Unknown };

            try
            {
                var audio = await speech.Synthesize(request.Text, voice: request.VoiceCode);

                var audioFile = Guid.NewGuid().ToString();
                ClustersControl.CreateFile(cluster, audioFile, FileFormat.Wav16, audio);
                var audioLink = Program.WorkDomain + $"AppData/Clusters/{cluster.Name}/{audioFile}{Help.GetTextFormat(FileFormat.Wav16)}";

                var imageFIle = Guid.NewGuid().ToString();
                ClustersControl.CreateFile(cluster, imageFIle, FileFormat.JPG, request.Image);
                var imageLink = Program.WorkDomain + $"AppData/Clusters/{cluster.Name}/{imageFIle}{Help.GetTextFormat(FileFormat.JPG)}";

                var result = await did.CreateTalk(audioLink, imageLink);

                var file = Guid.NewGuid().ToString();
                var format = FileFormat.MP4;

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

                return new()
                {
                    IsSuccess = true,
                    ErrorCode = ErrorCode.NoError,
                    ResultLink = $"clusters/video/{cluster.Name}/{file}"                  
                };
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return new() { IsSuccess = false, ErrorCode = ErrorCode.FailedGeneration };
            }
        }

        [HttpPost("audios")]
        public ClusterFilesResponse ClusterFilesAudios([FromBody] ClusterFilesRequest request)
        {
            var dbFiles = db.ClusterFiles.Where(f => f.ClusterId.Contains(request.ClientId) && f.FileFormat == FileFormat.Wav16);
            var cluster = ClustersControl.GetCluster(request.ClientId);

            if (cluster is null)
                return new() { IsSuccess = false, ErrorCode = ErrorCode.Unknown, ClusterFiles = null! };

            var files = new List<SharedClusterFile>();

            foreach (var file in dbFiles)
            {
                files.Add(new()
                {
                    Name = file.Name,
                    Template = "Аудио",
                    CreationTime = file.CreationTime,
                    Link = $"clusters/audio/{cluster.Name}/{file.Name}"
                });
            }

            return new()
            {
                IsSuccess = true,
                ErrorCode = ErrorCode.NoError,
                ClusterFiles = files
            };
        }

        [HttpPost("videos")]
        public ClusterFilesResponse ClusterFilesVideos([FromBody] ClusterFilesRequest request)
        {
            var dbFiles = db.ClusterFiles.Where(f => f.ClusterId.Contains(request.ClientId) && f.FileFormat == FileFormat.MP4);
            var cluster = ClustersControl.GetCluster(request.ClientId);

            if (cluster is null)
                return new() { IsSuccess = false, ErrorCode = ErrorCode.Unknown, ClusterFiles = null! };

            var files = new List<SharedClusterFile>();

            foreach (var file in dbFiles)
            {
                files.Add(new()
                {
                    Name = file.Name,
                    Template = "Видео",
                    CreationTime = file.CreationTime,
                    Link = $"clusters/video/{cluster.Name}/{file.Name}"
                });
            }

            return new()
            {
                IsSuccess = true,
                ErrorCode = ErrorCode.NoError,
                ClusterFiles = files
            };
        }
    }
}
