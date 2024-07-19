using TikTokTtsAPI;

namespace HttpClientExample
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            if(args.Length != 3)
            {
                Console.WriteLine("Usage: TikTokTtsAPI *text* *voice* *output_path*");
                return - 1;
            }

            string text = args[0];
            string voice = args[1];
            string path = args[2];
            
            var parts = Utils.SplitText(text);

            var datas = new List<byte[]>();

            using (var tikTokApiClient = new TikTokApiClient())
            {
                foreach (var part in parts)
                {
                    var data = await tikTokApiClient.SendRequestAndGetData(part, voice);
                    datas.Add(data);
                }
            }
            
            if(datas.Count == parts.Count) 
            {
                Utils.CombineFilesAndSaveMp3File(datas, path);
            }
            else
            {
                throw new Exception("Datas and parts don't match!");
            }

            Console.WriteLine("Done.");
            return 0;
        }
    }
}
