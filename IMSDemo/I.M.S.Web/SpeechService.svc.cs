using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Speech.Synthesis;
using System.IO;
using System.ServiceModel.Activation;

namespace I.M.S.Web
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SpeechService" in code, svc and config file together.
    public class SpeechService : ISpeechService
    {
        public void DoWork()
        {
        }

        public byte[] Speak(string text)
        {
            SpeechSynthesizer ss = new SpeechSynthesizer();
            MemoryStream ms = new MemoryStream();
            ss.SetOutputToWaveStream(ms);
            ss.Speak(text);
            return ms.ToArray();
        }
    }
}
