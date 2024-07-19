using NAudio.Wave;

namespace TikTokTtsAPI
{
    public class Utils
    {
        public static void CombineFilesAndSaveMp3File(List<byte[]> mp3Files, string outputPath)
        {
            using (var writer = File.Create(outputPath + ".mp3"))
            {
                foreach (var mp3File in mp3Files)
                {
                    using (var mp3Reader = new Mp3FileReader(new MemoryStream(mp3File)))
                    {
                        Mp3Frame mp3Frame;
                        while ((mp3Frame = mp3Reader.ReadNextFrame()) != null)
                        {
                            writer.Write(mp3Frame.RawData, 0, mp3Frame.RawData.Length);
                        }
                    }
                }
            }
        }

        public static List<string> SplitText(string text)
        {
            if (text.Length <= 300)
            {
                return new List<string> { text };
            }
            var output = new List<string>();

            var splitedText = text.Split('.').ToList();

            if (splitedText[splitedText.Count - 1] == "") splitedText.Remove(splitedText[splitedText.Count - 1]);

            for (int i = 0; i < splitedText.Count; i++)
            {
                splitedText[i] += '.';
            }

            foreach (var x in splitedText)
            {
                if (x.Length <= 300)
                {
                    output.Add(x);
                }
                else
                {
                    if (x.Length > 600)
                    {
                        throw new Exception("Unable to split text. Messages are too large!");
                    }
                    var words = x.Split(' ');
                    string part1 = "", part2 = "";

                    foreach (var word in words)
                    {
                        if (part1.Length + word.Length <= 300)
                        {
                            part1 += " " + word;
                        }
                        else
                        {
                            part2 += " " + word;
                        }
                    }

                    output.Add(part1);
                    output.Add(part2);
                }
            }

            return output;
        }
    }
}
